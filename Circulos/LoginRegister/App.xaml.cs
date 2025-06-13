using LoginRegister.View;
using LoginRegister.ViewModel;
using LoginRegister.Interface;
using LoginRegister.Services;
using LoginRegister.Models;
using LoginRegister.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Windows;
using LoginRegister.Service;

namespace LoginRegister
{
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // 1) Un único HttpClient con BaseAddress
            services.AddSingleton(new HttpClient { BaseAddress = new Uri(Constants.BASE_URL) });

            // 2) Servicio genérico que recibirá ese HttpClient
            services.AddTransient(typeof(IHttpJsonProvider<>), typeof(HttpJsonService<>));

            // 3) DTOs y servicios
            services.AddSingleton<LoginDTO>();
            services.AddSingleton<UserRegistroDTO>();
            services.AddSingleton<IJuegoServiceToApi, DicatadorServiceToApi>();
            services.AddSingleton<IStringUtils, StringUtils>();
            services.AddSingleton(typeof(IFileService<>), typeof(FileService<>));

            // 4) ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistroViewModel>();
            services.AddTransient<AddDicatadorViewModel>();
            services.AddTransient<InformacionViewModel>();
            services.AddTransient<DetallesViewModel>();

            // 5) Views
            services.AddSingleton<MainWindow>();
            services.AddTransient<AddDicatadorView>();
            services.AddTransient<DashboardView>();
            services.AddTransient<LoginView>();
            services.AddTransient<RegistroView>();
            services.AddTransient<CartasView>();
            services.AddTransient<TablaView>();
            services.AddTransient<FormuView>();
            services.AddTransient<InformacionView>();
            services.AddTransient<DetallesView>();

            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Services.GetRequiredService<MainWindow>().Show();
        }
    }
}
