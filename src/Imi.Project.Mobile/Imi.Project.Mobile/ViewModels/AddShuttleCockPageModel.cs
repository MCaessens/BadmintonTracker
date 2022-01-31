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
    public class AddShuttleCockPageModel : FreshBasePageModel
    {
        public AddShuttleCockPageModel(IShuttleCocksService shuttleCocksService, IVibrationService vibrationService)
        {
            _shuttleCocksService = shuttleCocksService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IShuttleCocksService _shuttleCocksService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private ObservableCollection<ShuttleType> _shuttleTypes;

        public ObservableCollection<ShuttleType> ShuttleTypes
        {
            get => _shuttleTypes;
            set
            {
                _shuttleTypes = value;
                RaisePropertyChanged();
            }
        }

        private ShuttleCockModel _newShuttle;

        public ShuttleCockModel NewShuttle
        {
            get => _newShuttle;
            set
            {
                _newShuttle = value;
                RaisePropertyChanged();
            }
        }

        private ShuttleCockErrorModel _errorModel;

        public ShuttleCockErrorModel ErrorModel
        {
            get => _errorModel;
            set { _errorModel = value; }
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
            NewShuttle = new ShuttleCockModel();
            ShuttleTypes = new ObservableCollection<ShuttleType>(Enum.GetValues(typeof(ShuttleType)).Cast<ShuttleType>());
        }

        private bool Validate()
        {
            var validator = new ShuttleCocksValidator();
            var validationResults = validator.Validate(NewShuttle);
            if (validationResults.IsValid) return true;
            var errorModel = new ShuttleCockErrorModel();
            validationResults.Errors.ForEach(error =>
            {
                switch (error.PropertyName)
                {
                    case nameof(NewShuttle.Model):
                        errorModel.ModelError = error.ErrorMessage;
                        break;
                    case nameof(NewShuttle.Brand):
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

            var addedShuttle = await _shuttleCocksService.AddShuttleCockAsync(NewShuttle);
            if (addedShuttle is null)
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
                NewShuttle.Image = photo;
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
                    "You have not granted the right permissions to perform this task.", "Ok");
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
                NewShuttle.Image = photo;
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
                    "You have not granted the right permissions to perform this task.", "Ok");
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        #endregion
    }
}