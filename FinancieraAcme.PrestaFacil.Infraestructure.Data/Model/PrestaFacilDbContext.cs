using FinancieraAcme.PrestaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancieraAcme.PrestaFacil.Infraestructure.Data.Model
{
    public class PrestaFacilDbContext : DbContext
    {
        public PrestaFacilDbContext(DbContextOptions<PrestaFacilDbContext> options) : base(options)
        {
        }

        public DbSet<SolicitudPrestamo> SolicitudPrestamos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<SolicitudCabecera> SolicitudCabeceras { get; set; }
        public DbSet<SolicitudDetalle> SolicitudDetalles { get; set; }

        // en este método se pueden hacer inicializaciones de tablas hacia la base de datos, por ejemplo, nombres, tipos de datos, tamaño, etc
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API (permite definir el tipo y tamaño de las columnas, precision, escala, relaciones entre tablas, escenarios de llave primaria compuesta, llaves foraneas, etc)
            //modelBuilder.Entity<SolicitudPrestamo>().ToTable("TBL_MA_SOLICITUD_PRESTAMOS");
            modelBuilder.Entity<SolicitudPrestamo>()
                .Property(sp => sp.MontoSolicitado)
                .HasColumnType("decimal(7,2)");

            // definicion de llave compuesta
            modelBuilder.Entity<SolicitudDetalle>()
                .HasKey(sd => new { sd.SolicitudCabeceraId, sd.Item });

            // definicion de relaciones entre entidades
            // Cliente -> SolicitudCabecera
            modelBuilder.Entity<SolicitudCabecera>()
                .HasOne(sc => sc.Cliente)
                .WithMany(c => c.SolicitudCabeceras)
                .HasForeignKey(sc => sc.ClienteId);

            // SolicitudCabecera -> SolicitudDetalle
            modelBuilder.Entity<SolicitudDetalle>()
                .HasOne(sd => sd.SolicitudCabecera)
                .WithMany(sc => sc.SolicitudDetalles);

        }

        // para generar posteriores migraciones considerar este comando:
        //Add-Migration ActualizarSolicitudPrestamo -Project FinancieraAcme.PrestaFacil.Infraestructure.Data -Context PrestaFacilDbContext
        //Update-Database -Context PrestaFacilDbContext
        // aplicar migracion especifica
        // Update-Database -Context PrestaFacilDbContext -Migration ActualizarSolicitudPrestamo
        // eliminar migracion
        // Remove-Migration

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        //    => optionsBuilder.LogTo(Console.WriteLine);
    }
}
