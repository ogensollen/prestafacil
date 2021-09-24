using FinancieraAcme.PrestaFacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.Domain.Interfaces
{
    public interface ISolicitudCabeceraRepository
    {
        IQueryable<SolicitudCabecera> TraerTodos();
        SolicitudCabecera TraerPorId(int id);
        void Agregar(SolicitudCabecera solicitudPrestamo);
        void Editar(SolicitudCabecera solicitudPrestamo);
        void Eliminar(int id);
        Task GuardarAsync();
    }
}
