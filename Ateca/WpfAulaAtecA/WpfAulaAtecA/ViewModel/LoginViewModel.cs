using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using WpfAulaAtecA.Models;
using WpfAulaAtecA.Utils;

namespace WpfAulaAtecA.ViewModel
{
    public partial class LoginViewModel : ViewModelBase
    {

        [ObservableProperty]
        public string _UserName;


        public string Password;
        [ObservableProperty] 
        public string _ErrorMessage;


        public override Task LoadAsync()
        {
            return base.LoadAsync();
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);
        }

        [RelayCommand]
        public async Task Login()
        {
            ErrorMessage = string.Empty;

            if (!CanLogin())
            {
                ErrorMessage = "Usuario o contraseña vacíos.";
                return;
            }

            var loginDto = new UserLoginDTO
            {
                UserName = UserName,
                Password = Password
            };

            var response = await HttpJsonClient<ApiResponse<LoginUserDTO>>.Post("https://localhost:7777/api/users/login", loginDto);

            if (response != null && response.IsSuccess && response.Result != null)
            {
                var usuario = response.Result;
                AuthSession.Token = usuario.Token;

                App.Current.Services.GetService<MainViewModel>().SelectedViewModel = App.Current.Services.GetService<MainViewModel>().ReservasPendientesViewModel;
            }
            else
            {
                ErrorMessage = "Credenciales incorrectas.";
            }
        }
    }
}