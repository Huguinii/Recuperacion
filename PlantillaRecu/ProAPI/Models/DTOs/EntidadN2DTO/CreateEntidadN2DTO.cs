using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.FranjaHorariaDTO;

public class CreateEntidadN2DTO
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public List<int> Entidad2Ids { get; set; } = new List<int>();


}
