using FinancieraAcme.PrestaFacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.Domain.Interfaces
{
    public interface ISolicitudDetalleRepository
    {
        IQueryable<SolicitudDetalle> TraerTodos();
        SolicitudDetalle TraerPorId(int id);
        void Agregar(SolicitudDetalle solicitudDetalle);
        void Editar(SolicitudDetalle solicitudDetalle);
        void Eliminar(int id);
        Task GuardarAsync();
    }
}
