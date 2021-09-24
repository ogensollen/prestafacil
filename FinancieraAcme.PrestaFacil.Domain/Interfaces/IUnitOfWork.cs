using System;
using System.Collections.Generic;
using System.Text;

namespace FinancieraAcme.PrestaFacil.Domain.Interfaces
{
    public interface IUnitOfWork
    {
         ISolicitudPrestamoRepository SolicitudPrestamoRepository { get; }
         Task GuardarAsync();
    }

}
