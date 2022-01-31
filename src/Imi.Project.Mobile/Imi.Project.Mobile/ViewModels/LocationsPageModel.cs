using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FreshMvvm;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class LocationsPageModel : FreshBasePageModel
    {
        public LocationsPageModel(ILocationsService locationsService, IVibrationService vibrationService)
        {
            _locationsService = locationsService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly ILocationsService _locationsService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private IEnumerable<LocationModel> _locations;

        public IEnumerable<LocationModel> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                RaisePropertyChanged();
            }
        }

        private bool _isRefreshing = false;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public Command OnRefresh => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await RequestLocations();
        });

        public Command OnViewDetails => new Command(async (model) =>
        {
            await _vibrationService.Vibrate();
            await CoreMethods.PushPageModel<LocationDetailPageModel>(model);
        });

        public Command OnAddLocation => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await CoreMethods.PushPageModel<AddLocationPageModel>();
        });

        #endregion

        #region Methods

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RequestLocations();
        }

        private async Task RequestLocations()
        {
            try
            {
                var apiResponse = await _locationsService.GetAllLocationsAsync();
                var apiLocations = apiResponse.Results;
                Locations = apiLocations;
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Oops", ex.Message, "Ok");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        #endregion
    }
}