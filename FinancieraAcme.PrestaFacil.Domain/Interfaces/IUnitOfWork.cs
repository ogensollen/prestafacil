using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ISolicitudCabeceraRepository SolicitudCabeceraRepository { get; }
        ISolicitudDetalleRepository SolicitudDetalleRepository { get; }
        Task GuardarAsync();
    }

}
