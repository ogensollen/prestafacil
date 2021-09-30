using FinancieraAcme.PrestaFacil.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.UI.Web.ViewModels
{
    public class SolicitudCabeceraViewModel
    {
        //public int Id { get; set; }
        //public DateTime FechaSolicitud { get; set; }
        //public int ClienteId { get; set; }
        public SolicitudCabecera SolicitudCabecera { get; set; }
        public SelectList Clientes { get; set; }

        public SolicitudCabeceraViewModel()
        {

        }

        public SolicitudCabeceraViewModel(IList<Cliente> clientes)
        {
            this.Clientes = new SelectList(clientes, "Id", "Apellidos");
        }
    }
}
