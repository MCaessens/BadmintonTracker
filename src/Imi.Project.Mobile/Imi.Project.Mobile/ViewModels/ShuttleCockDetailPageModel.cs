using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FreshMvvm;
using Imi.Project.Common.Enums;
using Imi.Project.Mobile.Core.Constants;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class ShuttleCockDetailPageModel : FreshBasePageModel
    {
        public ShuttleCockDetailPageModel(IShuttleCocksService shuttleCocksService, IVibrationService vibrationService)
        {
            _shuttleCocksService = shuttleCocksService;
            _vibrationService = vibrationService;
            IsModelPictureValid = true;
            IsNewPictureSelected = false;
        }

        #region Services

        private readonly IShuttleCocksService _shuttleCocksService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private ShuttleCockModel _selectedModel;

        public ShuttleCockModel SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                RaisePropertyChanged();
            }
        }

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
            SelectedModel = (ShuttleCockModel) initData;
            ShuttleTypes = new ObservableCollection<ShuttleType>(Enum.GetValues(typeof(ShuttleType)).Cast<ShuttleType>());
        }

        private async Task SaveAsync()
        {
            var updatedShuttle = await _shuttleCocksService.UpdateShuttleCockAsync(SelectedModel);
            if (updatedShuttle is null) await CoreMethods.DisplayAlert("Error", ErrorMessages.saveError, "Ok");
            else await CoreMethods.PopPageModel();
        }

        private async Task DeleteAsync()
        {
            var updated = await _shuttleCocksService.DeleteShuttleCockAsync(SelectedModel.Id);
            if (!updated) await CoreMethods.DisplayAlert("Error", ErrorMessages.deleteError, "Ok");
            else await CoreMethods.PopPageModel();
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
                await CoreMethods.DisplayAlert("Error", "You have not granted the right permissions to perform this task.",
                    "Ok");
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
                await CoreMethods.DisplayAlert("Error", "You have not granted the right permissions to perform this task.",
                    "Ok");
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        #endregion
    }
}