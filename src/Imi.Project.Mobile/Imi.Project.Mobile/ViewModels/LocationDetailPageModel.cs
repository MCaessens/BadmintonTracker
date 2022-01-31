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
    public class LocationDetailPageModel : FreshBasePageModel
    {
        public LocationDetailPageModel(ILocationsService locationsService, IVibrationService vibrationService)
        {
            _locationsService = locationsService;
            _vibrationService = vibrationService;
            IsModelPictureValid = true;
            IsNewPictureSelected = false;
        }

        #region Services

        private readonly ILocationsService _locationsService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private LocationModel _selectedModel;

        public LocationModel SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                RaisePropertyChanged();
            }
        }

        private LocationErrorModel _errorModel;

        public LocationErrorModel ErrorModel
        {
            get => _errorModel;
            set
            {
                _errorModel = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource _newPicture;

        public ImageSource NewPicture
        {
            get => _newPicture;
            set
            {
                _newPicture = value;
                RaisePropertyChanged();
            }
        }

        private bool _isModelPictureValid;

        public bool IsModelPictureValid
        {
            get => _isModelPictureValid;
            set
            {
                _isModelPictureValid = value;
                RaisePropertyChanged();
            }
        }

        private bool _isNewPictureSelected;

        public bool IsNewPictureSelected
        {
            get => _isNewPictureSelected;
            set
            {
                _isNewPictureSelected = value;
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
            SelectedModel = (LocationModel) initData;
        }

        private bool Validate(LocationModel model)
        {
            var validator = new LocationsValidator();
            var validation = validator.Validate(model);
            if (validation.IsValid) return true;
            var errorModel = new LocationErrorModel();
            validation.Errors.ForEach(error =>
            {
                switch (error.PropertyName)
                {
                    case nameof(SelectedModel.Name):
                        errorModel.NameError = error.ErrorMessage;
                        break;
                    case nameof(SelectedModel.PostalCode):
                        errorModel.PostalCodeError = error.ErrorMessage;
                        break;
                    case nameof(SelectedModel.City):
                        errorModel.CityError = error.ErrorMessage;
                        break;
                    case nameof(SelectedModel.Street):
                        errorModel.StreetError = error.ErrorMessage;
                        break;
                    case nameof(SelectedModel.Longitude):
                        errorModel.LongitudeError = error.ErrorMessage;
                        break;
                    case nameof(SelectedModel.Latitude):
                        errorModel.LatitudeError = error.ErrorMessage;
                        break;
                }
            });
            ErrorModel = errorModel;
            return false;
        }

        private async Task SaveAsync()
        {
            try
            {
                if (!Validate(SelectedModel)) return;
                var updatedLocation = await _locationsService.UpdateLocationAsync(SelectedModel);

                if (updatedLocation is null)
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

        private async Task UploadImageAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                if (photo == null) return;

                var stream = await photo.OpenReadAsync();
                NewPicture = ImageSource.FromStream(() => stream);
                IsModelPictureValid = false;
                IsNewPictureSelected = true;
                SelectedModel.Image = photo;
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
                NewPicture = ImageSource.FromStream(() => stream);
                IsModelPictureValid = false;
                IsNewPictureSelected = true;
                SelectedModel.Image = photo;
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

        private async Task DeleteAsync()
        {
            try
            {
                if (await _locationsService.DeleteLocationAsync(SelectedModel.Id)) await CoreMethods.PopPageModel();
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