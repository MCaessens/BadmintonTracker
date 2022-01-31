using Imi.Project.Wpf.Core.Interfaces;
using Imi.Project.Wpf.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Imi.Project.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IGamesService, GamesService>();
            services.AddTransient<ILocationsService, LocationsService>();
            services.AddTransient<IShuttleCocksService, ShuttleCocksService>();
            services.AddTransient<IRacketsService, RacketsService>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
