using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinancieraAcme.PrestaFacil.Infraestructure.Data.Migrations
{
    public partial class TablasSolicitudCabeceraDetalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    DocumentoIdentidad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudCabeceras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudCabeceras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudCabeceras_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudDetalles",
                columns: table => new
                {
                    SolicitudCabeceraId = table.Column<int>(nullable: false),
                    Item = table.Column<int>(nullable: false),
                    Producto = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudDetalles", x => new { x.SolicitudCabeceraId, x.Item });
                    table.ForeignKey(
                        name: "FK_SolicitudDetalles_SolicitudCabeceras_SolicitudCabeceraId",
                        column: x => x.SolicitudCabeceraId,
                        principalTable: "SolicitudCabeceras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudCabeceras_ClienteId",
                table: "SolicitudCabeceras",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudDetalles");

            migrationBuilder.DropTable(
                name: "SolicitudCabeceras");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
