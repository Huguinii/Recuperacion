using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.DiaNoLectivoDTO;

public class CreateDiaDTO
{
    [Required(ErrorMessage = "Fecha is required")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Motivo is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]
    public string Motivo { get; set; }
}
