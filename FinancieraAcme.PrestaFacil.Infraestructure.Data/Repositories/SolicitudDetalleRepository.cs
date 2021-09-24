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
    public class SolicitudDetalleRepository : ISolicitudDetalleRepository
    {
        private readonly PrestaFacilDbContext _db;

        public SolicitudDetalleRepository(PrestaFacilDbContext db)
        {
            _db = db;
        }

        public IQueryable<SolicitudDetalle> TraerTodos()
        {
            return _db.SolicitudDetalles.AsQueryable();
        }

        public SolicitudDetalle TraerPorId(int id)
        { 
            return _db.SolicitudDetalles.Find(id);
        }

        public void Agregar(SolicitudDetalle solicitudDetalle)
        {
            _db.Add<SolicitudDetalle>(solicitudDetalle);
        }

        public void Editar(SolicitudDetalle solicitudDetalle)
        {
            _db.SolicitudDetalles.Attach(solicitudDetalle).State = EntityState.Modified;
        }

        public void Eliminar(int id)
        {
            var solicitudDetalle = TraerPorId(id);
            if (solicitudDetalle != null)
            {
                _db.Remove<SolicitudDetalle>(solicitudDetalle);
            }
        }

        public async Task GuardarAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
