using System.ComponentModel.DataAnnotations;

namespace VisitasApp.Models
{
    public class Visita
    {
        public int Id { get; set; }
        [Display(Name = "Tipo Documento")]
        public int? IdTipoDocumento { get; set; }
        public int? IdDepartamento { get; set; }

        [Display(Name = "No. Doc.")]
        public string? Documento { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Comentario { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? Entrada { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? Salida { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Display(Name = "Registro")]
        public DateTime? FechaRegistro { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        [Display(Name = "Modificado")]
        public DateTime? FechaModificado { get; set; }

        [Display(Name = "Estatus")]
        public int? IdEstado { get; set; }

        [Display(Name = "Usuario")]
        public string? IdUsuario { get; set; }

        [Display(Name = "Tipo Doc.")]
        public virtual TipoDocumento? TipoDocumento { get; set; }

        public virtual Departamento? Departamento { get; set; }
        
    }
}
