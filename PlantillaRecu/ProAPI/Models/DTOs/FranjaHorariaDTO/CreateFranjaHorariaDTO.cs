using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.FranjaHorariaDTO;

public class CreateFranjaHorariaDTO
{

    [Required(ErrorMessage = "HoraInicio is required")]
    public TimeOnly HoraInicio { get; set; }

    [Required(ErrorMessage = "HoraFin is required")]
    public TimeOnly HoraFin { get; set; }

    public bool EsRecreo { get; set; } = false;
    public bool Disponible { get; set; } = true;

}
