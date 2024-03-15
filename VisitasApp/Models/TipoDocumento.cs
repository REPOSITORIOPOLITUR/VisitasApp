namespace VisitasApp.Models
{
    public class TipoDocumento
    {
        public TipoDocumento()
        {
            Visita = new HashSet<Visita>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? ValorS { get; set; }
        public string? Codigo { get; set; }
        public int? Orden { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdEstado { get; set; }

        public virtual ICollection<Visita> Visita { get; set; }
    }
  
}
