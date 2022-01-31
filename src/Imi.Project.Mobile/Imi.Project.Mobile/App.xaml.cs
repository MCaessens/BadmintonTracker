using FreshMvvm;
using Imi.Project.Mobile.Core.Constants;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Imi.Project.Mobile.Infrastructure.Services;
using Imi.Project.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Imi.Project.Mobile
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();
            RegisterDependencyServices();
            RegisterServices();
            RegisterMainContent();
            var loginPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            MainPage = new FreshNavigationContainer(loginPage, NavigationContainerNames.authContainer);
        }

        private static void RegisterDependencyServices()
        {
            DependencyService.Register<IVibrationService>();
        }

        private static void RegisterServices()
        {
            FreshIOC.Container.Register<IGamesService, GamesService>().AsMultiInstance();
            FreshIOC.Container.Register<IAuthService, AuthService>().AsMultiInstance();
            FreshIOC.Container.Register<ILocationsService, LocationsService>().AsMultiInstance();
            FreshIOC.Container.Register<IRacketsService, RacketsService>().AsMultiInstance();
            FreshIOC.Container.Register<IShuttleCocksService, ShuttleCocksService>().AsMultiInstance();
            FreshIOC.Container.Register(DependencyService.Get<IVibrationService>());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private string DetermineIconLocation(string iconName)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    return iconName;
                default:
                    return $"Icons/{iconName}";
            }
        }

        private void RegisterMainContent()
        {
            var mainNavContainer = new FreshTabbedNavigationContainer(NavigationContainerNames.mainContainer);

            // Visuals
            mainNavContainer.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            mainNavContainer.BarBackgroundColor = (Color) Resources["PrimaryColor"];
            mainNavContainer.BarTextColor = Color.White;
            mainNavContainer.UnselectedTabColor = Color.White;
            mainNavContainer.SelectedTabColor = Color.Gray;

            // Tabs
            mainNavContainer.AddTab<SettingsPageModel>("Settings", DetermineIconLocation("settings_icon.png"));
            mainNavContainer.AddTab<LocationsPageModel>("Locations", DetermineIconLocation("location_icon.png"));
            mainNavContainer.AddTab<GamesPageModel>("Games", DetermineIconLocation("home_icon.png"));
            mainNavContainer.AddTab<RacketsPageModel>("Rackets", DetermineIconLocation("badminton_racket_icon.png"));
            mainNavContainer.AddTab<ShuttleCocksPageModel>("Shuttles", DetermineIconLocation("shuttle_icon.png"));
            mainNavContainer.SwitchSelectedRootPageModel<GamesPageModel>();
        }
    }
}