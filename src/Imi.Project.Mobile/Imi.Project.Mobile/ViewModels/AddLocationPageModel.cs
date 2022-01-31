using System;
using System.Threading.Tasks;
using FreshMvvm;
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
    public class AddLocationPageModel : FreshBasePageModel
    {
        public AddLocationPageModel(ILocationsService locationsService, IVibrationService vibrationService)
        {
            _locationsService = locationsService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly ILocationsService _locationsService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private LocationModel _newLocation;

        public LocationModel NewLocation
        {
            get { return _newLocation; }
            set
            {
                _newLocation = value;
                RaisePropertyChanged();
            }
        }

        private LocationErrorModel _errorModel;

        public LocationErrorModel ErrorModel
        {
            get { return _errorModel; }
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
            NewLocation = new LocationModel();
        }

        private bool Validate()
        {
            var validator = new LocationsValidator();
            var validation = validator.Validate(NewLocation);
            if (validation.IsValid) return true;
            var errorModel = new LocationErrorModel();
            validation.Errors.ForEach(error =>
            {
                switch (error.PropertyName)
                {
                    case nameof(NewLocation.Name):
                        errorModel.NameError = error.ErrorMessage;
                        break;
                    case nameof(NewLocation.PostalCode):
                        errorModel.PostalCodeError = error.ErrorMessage;
                        break;
                    case nameof(NewLocation.City):
                        errorModel.CityError = error.ErrorMessage;
                        break;
                    case nameof(NewLocation.Street):
                        errorModel.StreetError = error.ErrorMessage;
                        break;
                    case nameof(NewLocation.Longitude):
                        errorModel.LongitudeError = error.ErrorMessage;
                        break;
                    case nameof(NewLocation.Latitude):
                        errorModel.LatitudeError = error.ErrorMessage;
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
            var addedLocation = await _locationsService.AddLocationAsync(NewLocation);
            if (addedLocation is null)
            {
                await CoreMethods.DisplayAlert("Error", ErrorMessages.addingLocationError, "Ok");
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
                NewLocation.Image = photo;
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

        private async Task TakePictureAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null) return;

                var stream = await photo.OpenReadAsync();
                SelectedImage = ImageSource.FromStream(() => stream);
                NewLocation.Image = photo;
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