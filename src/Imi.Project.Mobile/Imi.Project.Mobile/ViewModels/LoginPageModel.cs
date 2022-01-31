using System;
using System.Linq;
using System.Threading.Tasks;
using FreshMvvm;
using Imi.Project.Mobile.Core.Constants;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Core.Services;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        public LoginPageModel(IAuthService authService, IVibrationService vibrationService)
        {
            _authService = authService;
            _vibrationService = vibrationService;
        }

        #region Services

        private readonly IAuthService _authService;
        private readonly IVibrationService _vibrationService;

        #endregion

        #region Properties

        private LoginModel _loginModel;

        public LoginModel LoginModel
        {
            get => _loginModel;
            set
            {
                _loginModel = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public Command OnLogin => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await LoginAsync();
        });

        public Command OnGoToRegister => new Command(async () =>
        {
            await _vibrationService.Vibrate();
            await CoreMethods.PushPageModel<RegisterPageModel>();
        });

        #endregion

        #region Methods

        public override void Init(object initData)
        {
            base.Init(initData);
            LoginModel = new LoginModel();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            //When logged in automatically switch to MainContainer 
            if (TokenService.GetToken().Length > 0) CoreMethods.SwitchOutRootNavigation(NavigationContainerNames.mainContainer);
        }

        private async Task LoginAsync()
        {
            try
            {
                var response = await _authService.LoginAsync(LoginModel);
                var token = (response.Results.ToArray().First()).Token;
                if (response.Succeeded)
                {
                    LoginModel = new LoginModel();
                    TokenService.SaveToken(token);
                    CoreMethods.SwitchOutRootNavigation(NavigationContainerNames.mainContainer);
                }
                else
                {
                    await CoreMethods.DisplayAlert("Error", ErrorMessages.loginError, "Ok");
                }
            }
            catch
            {
                await CoreMethods.DisplayAlert("Error", ErrorMessages.serverError, "Ok");
            }
        }

        #endregion
    }
}