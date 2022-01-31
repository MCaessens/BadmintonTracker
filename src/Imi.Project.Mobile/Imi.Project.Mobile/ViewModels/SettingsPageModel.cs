using System.Threading.Tasks;
using FreshMvvm;
using Imi.Project.Mobile.Core.Constants;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Services;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class SettingsPageModel : FreshBasePageModel
    {
        public SettingsPageModel(IAuthService authService, IVibrationService vibrationService)
        {
            _authService = authService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IAuthService _authService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        #endregion

        #region Commands

        public Command OnLogOut => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            var result = await CoreMethods.DisplayActionSheet("Are you sure you want to logout?", "No", "Yes");
            if (result == "Yes")
            {
                await LogOutAsync();
            }
        });

        public Command OnRemoveAccount => new Command(() => { });

        #endregion

        #region Methods

        private async Task LogOutAsync()
        {
            try
            {
                var didLogoutSucceed = await _authService.LogoutAsync();
                var removeTokenSuccesful = TokenService.ResetToken();

                if (didLogoutSucceed && removeTokenSuccesful)
                    CoreMethods.SwitchOutRootNavigation(NavigationContainerNames.authContainer);
                else await CoreMethods.DisplayAlert("Error", ErrorMessages.basicError, "Ok");
            }
            catch
            {
                await CoreMethods.DisplayAlert("Error", ErrorMessages.serverError, "Ok");
            }
        }

        #endregion
    }
}