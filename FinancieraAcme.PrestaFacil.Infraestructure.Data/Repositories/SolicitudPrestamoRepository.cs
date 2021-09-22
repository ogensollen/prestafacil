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
    public class SolicitudPrestamoRepository : ISolicitudPrestamoRepository
    {
        private readonly PrestaFacilDbContext _db;

        public SolicitudPrestamoRepository(PrestaFacilDbContext db)
        {
            _db = db;
        }

        public IQueryable<SolicitudPrestamo> TraerTodos()
        {
            return _db.SolicitudPrestamos.AsQueryable();
        }

        public SolicitudPrestamo TraerPorId(int id)
        { 
            return _db.SolicitudPrestamos.Find(id);
        }

        public void Agregar(SolicitudPrestamo solicitudPrestamo)
        {
            _db.Add<SolicitudPrestamo>(solicitudPrestamo);
        }

        public void Editar(SolicitudPrestamo solicitudPrestamo)
        {
            _db.SolicitudPrestamos.Attach(solicitudPrestamo).State = EntityState.Modified;
        }

        public void Eliminar(int id)
        {
            var solicitudPrestamo = TraerPorId(id);
            if (solicitudPrestamo != null)
            {
                _db.Remove<SolicitudPrestamo>(solicitudPrestamo);
            }
        }

        public async Task GuardarAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<int> AgregarSPAsync(SolicitudPrestamo solicitudPrestamo)
        {
            var output = new SqlParameter();
            output.ParameterName = "@Id";
            output.SqlDbType = System.Data.SqlDbType.Int;
            output.Direction = System.Data.ParameterDirection.Output;
            string sp = "uspSolicitudPrestamoInsertar";
            FormattableString sql = $@"{sp} {output} out, {solicitudPrestamo.FechaRegistro}, {solicitudPrestamo.Cliente}, {solicitudPrestamo.MontoSolicitado}";
            await _db.Database.ExecuteSqlInterpolatedAsync(sql);
            return (int)output.Value;
        }

        public async Task EditarSPAsync(SolicitudPrestamo solicitudPrestamo)
        {
            string sp = "uspSolicitudPrestamoActualizar";
            FormattableString sql = $@"{sp} {solicitudPrestamo.Id}, {solicitudPrestamo.FechaRegistro}, {solicitudPrestamo.Cliente}, {solicitudPrestamo.MontoSolicitado}";
            await _db.Database.ExecuteSqlInterpolatedAsync(sql);
        }

        public async Task EliminarSPAsync(int id)
        {
            string sp = "uspSolicitudPrestamoEliminar";
            FormattableString sql = $@"{sp} {id}";
            await _db.Database.ExecuteSqlInterpolatedAsync(sql);
        }
    }
}
