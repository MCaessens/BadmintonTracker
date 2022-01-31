using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GameDetailPageModel : FreshBasePageModel
    {
        public GameDetailPageModel(ILocationsService locationsService, IGamesService gamesService,
            IShuttleCocksService shuttleCocksService, IRacketsService racketsService, IVibrationService vibrationService)
        {
            _locationsService = locationsService;
            _gamesService = gamesService;
            _shuttleCocksService = shuttleCocksService;
            _racketsService = racketsService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly ILocationsService _locationsService;
        private readonly IGamesService _gamesService;
        private readonly IRacketsService _racketsService;
        private readonly IShuttleCocksService _shuttleCocksService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private GameErrorModel _errorModel;

        public GameErrorModel ErrorModel
        {
            get => _errorModel;
            set
            {
                _errorModel = value;
                RaisePropertyChanged();
            }
        }

        private GameModel _selectedModel;

        public GameModel SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                RaisePropertyChanged();
            }
        }

        private List<LocationModel> _locations;

        public List<LocationModel> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                RaisePropertyChanged();
            }
        }

        private List<RacketModel> _rackets;

        public List<RacketModel> Rackets
        {
            get => _rackets;
            set
            {
                _rackets = value;
                RaisePropertyChanged();
            }
        }

        private List<ShuttleCockModel> _shuttleCocks;

        public List<ShuttleCockModel> ShuttleCocks
        {
            get => _shuttleCocks;
            set
            {
                _shuttleCocks = value;
                RaisePropertyChanged();
            }
        }

        private LocationModel _selectedLocation;

        public LocationModel SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value;
                RaisePropertyChanged();
            }
        }

        private RacketModel _selectedRacket;

        public RacketModel SelectedRacket
        {
            get => _selectedRacket;
            set
            {
                _selectedRacket = value;
                RaisePropertyChanged();
            }
        }

        private ShuttleCockModel _selectedShuttleCock;

        public ShuttleCockModel SelectedShuttleCock
        {
            get => _selectedShuttleCock;
            set
            {
                _selectedShuttleCock = value;
                RaisePropertyChanged();
            }
        }

        public int Score
        {
            get => SelectedModel.Score;
            set
            {
                SelectedModel.Score = value;
                RaisePropertyChanged();
            }
        }

        public int OpponentScore
        {
            get => SelectedModel.OpponentScore;
            set
            {
                SelectedModel.OpponentScore = value;
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

        public Command OnDelete => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            var result = await CoreMethods.DisplayActionSheet("Are you sure you want to delete this?", "Cancel", "Yes");
            if (result != "Yes") return;
            await DeleteAsync();
        });

        #endregion

        #region Methods

        public override void Init(object initData)
        {
            base.Init(initData);
            SelectedModel = (GameModel) initData;
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await FillInDetails();
        }

        private async Task FillInDetails()
        {
            try
            {
                var locationsResults = await _locationsService.GetAllLocationsAsync();
                var racketsResults = await _racketsService.GetAllRacketsAsync();
                var shuttleCockResults = await _shuttleCocksService.GetAllShuttleCocksAsync();
                Locations = locationsResults.Results.ToList();
                Rackets = racketsResults.Results.ToList();
                ShuttleCocks = shuttleCockResults.Results.ToList();
                SelectedLocation = Locations.FirstOrDefault(l => l.Id == SelectedModel.LocationId);
                SelectedRacket = Rackets.FirstOrDefault(r => r.Id == SelectedModel.RacketId);
                SelectedShuttleCock = ShuttleCocks.FirstOrDefault(sc => sc.Id == SelectedModel.ShuttleCockId);
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private bool Validate()
        {
            var validator = new GamesValidator();
            var validationResult = validator.Validate(SelectedModel);
            if (validationResult.IsValid) return validationResult.IsValid;
            var errorModel = new GameErrorModel();
            validationResult.Errors.ForEach(error =>
            {
                switch (error.PropertyName)
                {
                    case nameof(SelectedModel.Opponent):
                        errorModel.OpponentError = error.ErrorMessage;
                        break;
                }
            });
            ErrorModel = errorModel;

            return validationResult.IsValid;
        }

        private async Task SaveAsync()
        {
            try
            {
                var validated = Validate();
                if (!validated) return;
                SelectedModel.LocationId = SelectedLocation?.Id;
                SelectedModel.ShuttleCockId = SelectedShuttleCock?.Id;
                SelectedModel.RacketId = SelectedRacket?.Id;
                var updatedGame = await _gamesService.UpdateGameAsync(SelectedModel);
                if (updatedGame is null)
                {
                    await CoreMethods.DisplayAlert("Error", ErrorMessages.saveError, "Ok");
                    return;
                }

                await CoreMethods.PopPageModel();
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async Task DeleteAsync()
        {
            try
            {
                if (await _gamesService.DeleteGameAsync(_selectedModel.Id)) await CoreMethods.PopPageModel();
                else await CoreMethods.DisplayAlert("Error", ErrorMessages.deleteError, "Ok");
            }
            catch
            {
                await CoreMethods.DisplayAlert("Error", ErrorMessages.serverError, "Ok");
            }
        }

        #endregion
    }
}