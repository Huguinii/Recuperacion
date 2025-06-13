using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.FranjaHorariaDTO;

public class EntidadN1N2DTO
{
    public int EntidadN1Id { get; set; }
    public int EntidadN2Id { get; set; }
    public DateTime FechaRelacion { get; set; }

}
