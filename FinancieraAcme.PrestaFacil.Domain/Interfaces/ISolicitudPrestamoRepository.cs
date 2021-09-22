using FinancieraAcme.PrestaFacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.Domain.Interfaces
{
    public interface ISolicitudPrestamoRepository
    {
        IQueryable<SolicitudPrestamo> TraerTodos();
        SolicitudPrestamo TraerPorId(int id);
        void Agregar(SolicitudPrestamo solicitudPrestamo);
        void Editar(SolicitudPrestamo solicitudPrestamo);
        void Eliminar(int id);
        Task GuardarAsync();
        Task<int> AgregarSPAsync(SolicitudPrestamo solicitudPrestamo);
        Task EditarSPAsync(SolicitudPrestamo solicitudPrestamo);
        Task EliminarSPAsync(int id);
    }
}
