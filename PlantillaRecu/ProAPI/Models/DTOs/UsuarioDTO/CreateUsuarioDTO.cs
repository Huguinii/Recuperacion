using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.DTOs;

public class CreateUsuarioDTO
{
    [Required(ErrorMessage = "Nombre is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]

    public String Nombre { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]

    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]

    public string Password { get; set; }

    [Required(ErrorMessage = "Rol is required")]
    [MaxLength(50, ErrorMessage = "Max char is 50")]

    public string Rol { get; set; }


}
