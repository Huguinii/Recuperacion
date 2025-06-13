using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LoginRegister.Helpers;
using LoginRegister.Interface;
using LoginRegister.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace LoginRegister.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly IHttpJsonProvider<UserDTO> _httpProvider;
        private readonly IJuegoServiceToApi _juegoService;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _passwordView;

        public LoginViewModel(
            IHttpJsonProvider<UserDTO> httpProvider,
            IJuegoServiceToApi juegoService)
        {
            _httpProvider = httpProvider;
            _juegoService = juegoService;
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(PasswordView))
            {
                MessageBox.Show("Por favor, rellene ambos campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var loginDto = App.Current.Services.GetRequiredService<LoginDTO>();
            loginDto.UserName = Name;
            loginDto.Password = PasswordView;

            try
            {
                // Llama /api/users/login
                var userDto = await _httpProvider.LoginPostAsync(Constants.LOGIN_PATH, loginDto);

                if (userDto != null && userDto.IsSuccess)
                {
                    var token = userDto.Result.Token;
                    var userName = JwtHelper.GetClaim(token, "unique_name");
                    App.Current.Properties["userName"] = userName;

                    // Navegar a Dashboard
                    var mainVm = App.Current.Services.GetRequiredService<MainViewModel>();
                    mainVm.SelectedViewModel = mainVm.DashboardViewModel;
                    await mainVm.LoadAsync();

                    Name = PasswordView = string.Empty;
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        public void Register()
        {
            var mainVm = App.Current.Services.GetRequiredService<MainViewModel>();
            mainVm.SelectedViewModel = mainVm.RegistroViewModel;
        }
    }
}
