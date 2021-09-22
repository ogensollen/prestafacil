using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FinancieraAcme.PrestaFacil.Domain.Entities
{
    public class SolicitudCabecera
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }
        public List<SolicitudDetalle> SolicitudDetalles { get; set; }
    }
}
