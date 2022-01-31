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
    public class ShuttleCocksPageModel : FreshBasePageModel
    {
        public ShuttleCocksPageModel(IShuttleCocksService shuttleCocksService, IVibrationService vibrationService)
        {
            _shuttleCocksService = shuttleCocksService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IShuttleCocksService _shuttleCocksService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private IEnumerable<ShuttleCockModel> _shuttleCocks;

        public IEnumerable<ShuttleCockModel> ShuttleCocks
        {
            get => _shuttleCocks;
            set
            {
                _shuttleCocks = value;
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
            await CoreMethods.PushPageModel<ShuttleCockDetailPageModel>(selectedModel);
        });

        public Command OnAddShuttleCock => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await CoreMethods.PushPageModel<AddShuttleCockPageModel>();
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
                var apiResponse = await _shuttleCocksService.GetAllShuttleCocksAsync();
                var shuttleCocks = apiResponse.Results;
                ShuttleCocks = shuttleCocks;
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