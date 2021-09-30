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
    public class ClienteRepository : IClienteRepository
    {
        private readonly PrestaFacilDbContext _db;

        public ClienteRepository(PrestaFacilDbContext db)
        {
            _db = db;
        }

        public IQueryable<Cliente> TraerTodos()
        {
            return _db.Clientes.AsQueryable();
        }

        public Cliente TraerPorId(int id)
        {
            return _db.Clientes.Find(id);
        }

    }
}
