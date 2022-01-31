using System.Threading.Tasks;
using FluentValidation.Results;
using FreshMvvm;
using Imi.Project.Mobile.Core.Constants;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Validators;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class RegisterPageModel : FreshBasePageModel
    {
        public RegisterPageModel(IAuthService authService, IVibrationService vibrationService)
        {
            _authService = authService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IAuthService _authService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private RegisterModel _registerModel;

        public RegisterModel RegisterModel
        {
            get => _registerModel;
            set
            {
                _registerModel = value;
                RaisePropertyChanged();
            }
        }

        private bool _isRegistering;

        public bool IsRegistering
        {
            get => _isRegistering;
            set
            {
                _isRegistering = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public Command OnRegister => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            var validationResult = Validate();
            if (!validationResult.IsValid)
            {
                await ShowValidationError(validationResult);
                return;
            }

            await RegisterAsync();
        });

        #endregion

        #region Methods

        private async Task RegisterAsync()
        {
            IsRegistering = true;
            try
            {
                var didRegisterSucceed = await _authService.RegisterAsync(RegisterModel);
                if (didRegisterSucceed) await CoreMethods.PopToRoot(true);
            }
            catch
            {
                await CoreMethods.DisplayAlert("Error", ErrorMessages.serverError, "Ok");
            }
            finally
            {
                IsRegistering = false;
            }
        }

        private async Task ShowValidationError(ValidationResult validationResult)
        {
            string message = "";
            validationResult.Errors.ForEach(m => message += $"{m.ErrorMessage}\n");
            await CoreMethods.DisplayAlert("Error", message, "Ok");
        }

        private ValidationResult Validate()
        {
            var validator = new RegisterValidator();
            var validationResult = validator.Validate(RegisterModel);
            return validationResult;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            RegisterModel = new RegisterModel();
        }

        #endregion
    }
}