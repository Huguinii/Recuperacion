using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.ReservaDTO;

public class ReservaDTO : CreateReservaDTO
{
    
    public string Id { get; set; }
    public string Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    


}
