using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FreshMvvm;
using Imi.Project.Mobile.Core.Constants;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Models.ErrorModels;
using Imi.Project.Mobile.Core.Validators;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class AddGamePageModel : FreshBasePageModel
    {
        public AddGamePageModel(IGamesService gamesService, IRacketsService racketsService,
            IShuttleCocksService shuttleCocksService, ILocationsService locationsService, IVibrationService vibrationService)
        {
            _gamesService = gamesService;
            _racketsService = racketsService;
            _shuttleCocksService = shuttleCocksService;
            _locationsService = locationsService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IGamesService _gamesService;
        private readonly IRacketsService _racketsService;
        private readonly IShuttleCocksService _shuttleCocksService;
        private readonly ILocationsService _locationsService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private GameModel _newGameModel;

        public GameModel NewGameModel
        {
            get { return _newGameModel; }
            set
            {
                _newGameModel = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<LocationModel> _locations;

        public ObservableCollection<LocationModel> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RacketModel> _rackets;

        public ObservableCollection<RacketModel> Rackets
        {
            get { return _rackets; }
            set
            {
                _rackets = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ShuttleCockModel> _shuttleCocks;

        public ObservableCollection<ShuttleCockModel> ShuttleCocks
        {
            get { return _shuttleCocks; }
            set
            {
                _shuttleCocks = value;
                RaisePropertyChanged();
            }
        }

        private LocationModel _selectedLocation;

        public LocationModel SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                RaisePropertyChanged();
            }
        }

        private RacketModel _selectedRacket;

        public RacketModel SelectedRacket
        {
            get { return _selectedRacket; }
            set
            {
                _selectedRacket = value;
                RaisePropertyChanged();
            }
        }

        private ShuttleCockModel _selectedShuttleCock;

        public ShuttleCockModel SelectedShuttleCock
        {
            get { return _selectedShuttleCock; }
            set
            {
                _selectedShuttleCock = value;
                RaisePropertyChanged();
            }
        }

        private GameErrorModel _errorModel;

        public GameErrorModel ErrorModel
        {
            get { return _errorModel; }
            set
            {
                _errorModel = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public Command OnSave => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await SaveAsync();
        });

        #endregion

        #region Methods

        public override void Init(object initData)
        {
            base.Init(initData);
            NewGameModel = new GameModel();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            var rackets = await _racketsService.GetAllRacketsAsync();
            var shuttleCocks = await _shuttleCocksService.GetAllShuttleCocksAsync();
            var locations = await _locationsService.GetAllLocationsAsync();

            Rackets = new ObservableCollection<RacketModel>(rackets.Results);
            ShuttleCocks = new ObservableCollection<ShuttleCockModel>(shuttleCocks.Results);
            Locations = new ObservableCollection<LocationModel>(locations.Results);
        }

        private async Task SaveAsync()
        {
            NewGameModel.LocationId = SelectedLocation?.Id;
            NewGameModel.RacketId = SelectedRacket?.Id;
            NewGameModel.ShuttleCockId = SelectedShuttleCock?.Id;

            var validated = Validate();
            if (!validated) return;

            var addedGame = await _gamesService.AddGameAsync(NewGameModel);
            if (addedGame is null)
            {
                await CoreMethods.DisplayAlert("Error", ErrorMessages.addingGameError, "Ok");
                return;
            }

            await CoreMethods.PopPageModel();
        }

        private bool Validate()
        {
            var validator = new GamesValidator();
            var validationResult = validator.Validate(NewGameModel);
            if (validationResult.IsValid) return validationResult.IsValid;
            var errorModel = new GameErrorModel();
            validationResult.Errors.ForEach(error =>
            {
                switch (error.PropertyName)
                {
                    case nameof(NewGameModel.Opponent):
                        errorModel.OpponentError = error.ErrorMessage;
                        break;
                }
            });
            ErrorModel = errorModel;

            return validationResult.IsValid;
        }

        #endregion
    }
}