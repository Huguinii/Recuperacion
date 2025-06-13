using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.Entity
{
    public class DiaEntity
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Motivo { get; set; }

    }
}
