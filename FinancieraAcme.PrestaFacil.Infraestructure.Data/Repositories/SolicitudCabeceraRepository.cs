using FinancieraAcme.PrestaFacil.Domain.Entities;
using FinancieraAcme.PrestaFacil.Domain.Interfaces;
using FinancieraAcme.PrestaFacil.Infraestructure.Data.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.Infraestructure.Data.Repositories
{
    public class SolicitudCabeceraRepository : ISolicitudCabeceraRepository
    {
        private readonly PrestaFacilDbContext _db;

        public SolicitudCabeceraRepository(PrestaFacilDbContext db)
        {
            _db = db;
        }

        public IQueryable<SolicitudCabecera> TraerTodos()
        {
            return _db.SolicitudCabeceras.AsQueryable();
        }

        public SolicitudCabecera TraerPorId(int id)
        { 
            return _db.SolicitudCabeceras.Find(id);
        }

        public void Agregar(SolicitudCabecera solicitudPrestamo)
        {
            _db.Add<SolicitudCabecera>(solicitudPrestamo);
        }

        public void Editar(SolicitudCabecera solicitudPrestamo)
        {
            _db.SolicitudCabeceras.Attach(solicitudPrestamo).State = EntityState.Modified;
        }

        public void Eliminar(int id)
        {
            var solicitudPrestamo = TraerPorId(id);
            if (solicitudPrestamo != null)
            {
                _db.Remove<SolicitudCabecera>(solicitudPrestamo);
            }
        }

        public async Task GuardarAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
