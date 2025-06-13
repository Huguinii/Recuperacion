using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAulaAtecA.Models
{
    public class ReservaDTO
    {
        public string Id { get; set; }
        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }    // JSON devuelve fecha y hora completas
        public DateOnly Fecha { get; set; }              // Solo fecha como string (opcional convertir a DateOnly)
        public TimeOnly HoraInicio { get; set; }         // Como string "23:04:05"
        public TimeOnly HoraFin { get; set; }            // Como string "23:08:08"

        public string Grupo { get; set; }
        public string NombreProfesor { get; set; }     // En JSON se llama "nombreProfesor"
    }

}
