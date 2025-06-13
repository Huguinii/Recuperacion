using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAulaAtecA.Interface;
using WpfAulaAtecA.Models;
using WpfAulaAtecA.Utils;

namespace WpfAulaAtecA.Services
{
    public class GetReservaApiService : IGetReservaApiService
    {
        public async Task<ReservaDTO> GetReserva()
        {

            string url = Constants.RESERVAS_URL;


            ReservaDTO reserva = await HttpJsonClient<ReservaDTO>.Get(url);
            return reserva;
        }

        public async Task AddReservaToApi(object reserva)
        {
            try
            {
                if (reserva == null) return;
                var response = await HttpJsonClient<ReservaDTO>.Post(Constants.RESERVAS_URL, reserva);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<List<ReservaDTO>> GetAllReservas()
        {
            try
            {
                var pokemons = await HttpJsonClient<List<ReservaDTO>>.Get(Constants.RESERVAS_URL);
                return pokemons ?? new List<ReservaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de la  API: {ex.Message}");
                return new List<ReservaDTO>();
            }
        }
        public async Task<List<ReservaDTO>> GetReservasPendientes()
        {
            try
            {
                string url = Constants.RESERVAS_URL + "/pendientes";
                var reservas = await HttpJsonClient<List<ReservaDTO>>.Get(url);
                return reservas ?? new List<ReservaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener reservas pendientes: {ex.Message}");
                return new List<ReservaDTO>();
            }
        }
        public async Task<bool> AprobarReserva(string id)
        {
            try
            {
                string url = $"{Constants.RESERVAS_URL}/{id}/aprobar";
                var result = await HttpJsonClient<bool>.Patch(url, null); // cuerpo vacío
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al aprobar reserva: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RechazarReserva(string id)
        {
            try
            {
                string url = $"{Constants.RESERVAS_URL}/{id}/rechazar";
                var result = await HttpJsonClient<bool>.Patch(url, null); // cuerpo vacío
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al rechazar reserva: {ex.Message}");
                return false;
            }
        }


    }
}

