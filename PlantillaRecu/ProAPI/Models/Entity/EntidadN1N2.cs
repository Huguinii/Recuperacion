namespace RestAPI.Models.Entity
{
    public class EntidadN1N2
    {
        public int EntidadN1Id { get; set; }
        public EntidadN1Entity EN1 { get; set; }

        public int EntidadN2Id { get; set; }
        public EntidadN2Entity EN2 { get; set; }

        //se pueden añadir propiedades propias de la relacion
        public DateTime FechaRelacion { get; set; } = DateTime.Now;

        //lo siguiente es configurar el dbcontext
    }

}
