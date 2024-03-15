namespace VisitasApp.Models
{
    public class Departamento
    {
        public Departamento()
        {
            Visita = new HashSet<Visita>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdAreaSuperior { get; set; }
        public long? IdTipoUnidad { get; set; }
        public double? Orden { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool? IdEstado { get; set; }

        public virtual ICollection<Visita> Visita { get; set; }
    }
}
