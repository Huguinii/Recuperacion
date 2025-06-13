using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAulaAtecA.Models
{
    public class FranjaDTO
    {
        public string Id { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public bool EsRecreo { get; set; }
        public bool Disponible { get; set; }
    }

}
