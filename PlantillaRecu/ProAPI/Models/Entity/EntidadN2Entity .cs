using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models.Entity
{
    public class EntidadN2Entity
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public List<EntidadN1N2> EntidadesN1 { get; set; }
    }
}
