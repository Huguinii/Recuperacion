using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.Entity
{
    public class ReservaEntity
    {
        public int Id { get; set; }

        [Required]
        public DateOnly Fecha { get; set; }

        [Required]
        public TimeOnly HoraInicio { get; set; }

        [Required]
        public TimeOnly HoraFin { get; set; }

        [Required]
        public string Grupo { get; set; }

        [Required]
        public string Estado { get; set; } = "Pendiente";

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Required]
        public string NombreProfesor { get; set; }
    }
}
