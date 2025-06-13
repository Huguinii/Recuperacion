using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAulaAtecA.Services
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(string username, string password);
    }

    public class LoginService : ILoginService
    {
        public async Task<bool> LoginAsync(string username, string password)
        {
            // Falta llamada a la api con HttpClient
            await Task.Delay(500);

            return username == "admin@iescomercio.com" && password == "admin123";
        }
    }
}

