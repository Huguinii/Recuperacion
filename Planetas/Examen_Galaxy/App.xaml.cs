using Examen_Galaxy.Interface;
using Examen_Galaxy.Service;
using Examen_Galaxy.View;
using Examen_Galaxy.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Examen_Galaxy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Current.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //view principal
            services.AddTransient<MainWindow>();
            services.AddTransient<AddPlaneta>();

            //view viewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<GalaxyOverviewViewModel>();
            services.AddTransient<ExplorePlanetViewModel>();
            services.AddTransient<AddPlanetaViewModel>();
            services.AddTransient<ExportViewModel>();
            services.AddTransient<GalaxyGridViewModel>();
            services.AddTransient<ImportViewModel>();
       


            //Services
            services.AddSingleton(typeof(IHttpJsonProvider<>), typeof(HttpJsonService<>));
            services.AddSingleton<IStringUtils, StringUtils>();
            services.AddSingleton(typeof(IFileService<>), typeof(FileService<>));
            return services.BuildServiceProvider();
        }
    }

}
