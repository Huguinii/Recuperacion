using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.Entity
{
    public class FranjaHorariaEntity
    {
        public int Id { get; set; }

        [Required]
        public TimeOnly HoraInicio { get; set; }

        [Required]
        public TimeOnly HoraFin { get; set; }

        public bool EsRecreo { get; set; } = false;

        public bool Disponible { get; set; } = true;
    }
}
