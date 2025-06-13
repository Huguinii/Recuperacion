using Microsoft.Extensions.DependencyInjection;
using WpfAulaAtecA.ViewModel;
using System.Windows;

namespace WpfAulaAtecA
{
    
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

            //view viewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ReservasPendientesViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<DiasNoLectivosViewModel>();
            services.AddSingleton<FranjasHorariasViewModel>();



            return services.BuildServiceProvider();
        }
    }

}
