using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAulaAtecA.Models;

namespace WpfAulaAtecA.Interface
{
    public interface IGetReservaApiService
    {
        Task<ReservaDTO> GetReserva();
        Task AddReservaToApi(object reserva);
        Task<List<ReservaDTO>> GetAllReservas();
        Task<List<ReservaDTO>> GetReservasPendientes();
        Task<bool> AprobarReserva(string id);
        Task<bool> RechazarReserva(string id);
    }

}
