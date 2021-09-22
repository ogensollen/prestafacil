using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinancieraAcme.PrestaFacil.Domain.Entities
{
    //[Table("MOV010SOLPRES")]
    public class SolicitudPrestamo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage = "Es obligatorio ingresar el cliente")]
        [Column(TypeName = "varchar(500)")]
        public string Cliente { get; set; }

        [Required]
        [Range(1,50000)]
        public decimal MontoSolicitado { get; set; }

        public SolicitudPrestamo()
        {
            this.FechaRegistro = DateTime.Now;
            this.MontoSolicitado = 1;
        }
    }
}
