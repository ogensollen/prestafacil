using System;
using System.Collections.Generic;
using System.Text;

namespace FinancieraAcme.PrestaFacil.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string DocumentoIdentidad { get; set; }

        public List<SolicitudCabecera> SolicitudCabeceras { get; set; }
    }
}
