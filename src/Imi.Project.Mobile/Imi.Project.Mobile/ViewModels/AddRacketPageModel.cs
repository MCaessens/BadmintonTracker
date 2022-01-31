using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FreshMvvm;
using Imi.Project.Common.Enums;
using Imi.Project.Mobile.Core.Constants;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Models.ErrorModels;
using Imi.Project.Mobile.Core.Validators;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class AddRacketPageModel : FreshBasePageModel
    {
        public AddRacketPageModel(IRacketsService racketsService, IVibrationService vibrationService)
        {
            _racketsService = racketsService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IRacketsService _racketsService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private RacketModel _newRacket;

        public RacketModel NewRacket
        {
            get => _newRacket;
            set
            {
                _newRacket = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RacketType> _racketTypes;

        public ObservableCollection<RacketType> RacketTypes
        {
            get => _racketTypes;
            set
            {
                _racketTypes = value;
                RaisePropertyChanged();
            }
        }

        private RacketErrorModel _errorModel;

        public RacketErrorModel ErrorModel
        {
            get => _errorModel;
            set
            {
                _errorModel = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource _selectedImage;

        public ImageSource SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
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

        public Command OnUploadImage => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await UploadImageAsync();
        });

        public Command OnTakePicture => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await TakePictureAsync();
        });

        #endregion

        #region Methods

        public override void Init(object initData)
        {
            base.Init(initData);
            _newRacket = new RacketModel();
            RacketTypes = new ObservableCollection<RacketType>(Enum.GetValues(typeof(RacketType)).Cast<RacketType>());
        }

        private bool Validate()
        {
            var validator = new RacketsValidator();
            var validationResults = validator.Validate(NewRacket);
            if (validationResults.IsValid) return true;
            var errorModel = new RacketErrorModel();
            validationResults.Errors.ForEach(error =>
            {
                switch (error.PropertyName)
                {
                    case nameof(NewRacket.Model):
                        errorModel.ModelError = error.ErrorMessage;
                        break;
                    case nameof(NewRacket.Brand):
                        errorModel.BrandError = error.ErrorMessage;
                        break;
                }
            });
            ErrorModel = errorModel;
            return false;
        }

        private async Task SaveAsync()
        {
            var validated = Validate();
            if (!validated) return;

            var addedRacket = await _racketsService.AddRacketAsync(NewRacket);
            if (addedRacket is null)
            {
                await CoreMethods.DisplayAlert("Error", ErrorMessages.addingShuttleError, "Ok");
                return;
            }

            await CoreMethods.PopPageModel();
        }

        private async Task UploadImageAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                if (photo == null) return;

                var stream = await photo.OpenReadAsync();
                SelectedImage = ImageSource.FromStream(() => stream);
                NewRacket.Image = photo;
            }
            catch (FeatureNotSupportedException)
            {
                // Feature is not supported on the device
                await CoreMethods.DisplayAlert("Error", "Device does not support this feature.", "Ok");
            }
            catch (PermissionException)
            {
                // Permissions not granted
                await CoreMethods.DisplayAlert("Error",
                    "You have not granted the right permissions to perform this task.",
                    "Ok");
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async Task TakePictureAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null) return;

                var stream = await photo.OpenReadAsync();
                SelectedImage = ImageSource.FromStream(() => stream);
                NewRacket.Image = photo;
            }
            catch (FeatureNotSupportedException)
            {
                // Feature is not supported on the device
                await CoreMethods.DisplayAlert("Error", "Device does not support this feature.", "Ok");
            }
            catch (PermissionException)
            {
                // Permissions not granted
                await CoreMethods.DisplayAlert("Error", "You have not granted the right permissions to perform this task.", "Ok");
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        #endregion
    }
}