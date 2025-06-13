using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs.ReservaDTO;

public class CreateReservaDTO
{
    [Required(ErrorMessage = "Fecha is required")]
    public DateOnly Fecha { get; set; }

    [Required(ErrorMessage = "HoraInicio is required")]
    public TimeOnly HoraInicio { get; set; }

    [Required(ErrorMessage = "HoraFin is required")]
    public TimeOnly HoraFin { get; set; }

    [Required(ErrorMessage = "Grupo is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]
    public string Grupo { get; set; }

    [Required(ErrorMessage = "NombreProfesor is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]
    public string NombreProfesor { get; set; }

}
