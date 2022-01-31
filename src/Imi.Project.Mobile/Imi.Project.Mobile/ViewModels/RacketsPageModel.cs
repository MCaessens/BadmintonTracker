using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FreshMvvm;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class RacketsPageModel : FreshBasePageModel
    {
        public RacketsPageModel(IRacketsService racketsService, IVibrationService vibrationService)
        {
            _racketsService = racketsService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IRacketsService _racketsService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private ObservableCollection<RacketModel> _rackets;

        public ObservableCollection<RacketModel> Rackets
        {
            get => _rackets;
            set
            {
                _rackets = value;
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
            await RequestRackets();
        });

        public Command OnViewDetails => new Command(async (selectedModel) =>
        {
            await _vibrationService.Vibrate();
            await CoreMethods.PushPageModel<RacketDetailPageModel>(selectedModel);
        });

        public Command OnAddRacket => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await CoreMethods.PushPageModel<AddRacketPageModel>();
        });

        #endregion

        #region Methods

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RequestRackets();
        }

        private async Task RequestRackets()
        {
            try
            {
                var apiResponse = await _racketsService.GetAllRacketsAsync();
                var rackets = apiResponse.Results;
                Rackets = null;
                Rackets = new ObservableCollection<RacketModel>(rackets);
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