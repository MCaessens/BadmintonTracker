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
    public class GamesPageModel : FreshBasePageModel
    {
        public GamesPageModel(IGamesService gamesService, IVibrationService vibrationService)
        {
            _gamesService = gamesService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IGamesService _gamesService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private IEnumerable<GameModel> _games;

        public IEnumerable<GameModel> Games
        {
            get => _games;
            set
            {
                _games = value;
                RaisePropertyChanged();
            }
        }

        private bool _isRefreshing;

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

        public Command OnAddGame => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await CoreMethods.PushPageModel<AddGamePageModel>();
        });

        public Command OnRefresh => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await RequestGames();
        });

        public Command OnViewDetails => new Command(async (model) =>
        {
            await _vibrationService.Vibrate();
            await CoreMethods.PushPageModel<GameDetailPageModel>(model);
        });

        #endregion

        #region Methods

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await RequestGames();
        }

        private async Task RequestGames()
        {
            try
            {
                var apiResponse = await _gamesService.GetAllGamesAsync();
                var games = apiResponse.Results;
                Games = games;
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