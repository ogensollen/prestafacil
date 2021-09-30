using FinancieraAcme.PrestaFacil.Domain.Interfaces;
using FinancieraAcme.PrestaFacil.Infraestructure.Data.Model;
using FinancieraAcme.PrestaFacil.Infraestructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.Infraestructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PrestaFacilDbContext _db;
        private ISolicitudCabeceraRepository _solicitudCabecera;
        private ISolicitudDetalleRepository _solicitudDetalle;

        public UnitOfWork(PrestaFacilDbContext db)
        {
            _db = db;
        }

        public ISolicitudCabeceraRepository SolicitudCabeceraRepository
        {
            get 
            {
                if (this._solicitudCabecera == null)
                    this._solicitudCabecera = new SolicitudCabeceraRepository(_db);
                return _solicitudCabecera;
            }
        }

        public ISolicitudDetalleRepository SolicitudDetalleRepository
        {
            get
            {
                if (this._solicitudDetalle == null)
                    this._solicitudDetalle = new SolicitudDetalleRepository(_db);
                return _solicitudDetalle;
            }
        }

        public async Task GuardarAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
