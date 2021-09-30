using FinancieraAcme.PrestaFacil.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FinancieraAcme.PrestaFacil.UI.Web.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using FinancieraAcme.PrestaFacil.UI.Web.Extensions;
using FinancieraAcme.PrestaFacil.Domain.Entities;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.UI.Web.Controllers
{
    public class SolicitudPrestamoTxController : Controller
    {
        private readonly ISolicitudCabeceraRepository _repoCabecera;
        private readonly IClienteRepository _repoCliente;
        private readonly IUnitOfWork _uow;

        public SolicitudPrestamoTxController(ISolicitudCabeceraRepository repoCabecera, IClienteRepository repoCliente, IUnitOfWork uow)
        {
            _repoCabecera = repoCabecera;
            _repoCliente = repoCliente;
            _uow = uow;
        }

        public IActionResult Registrar()
        {
            SolicitudCabeceraViewModel vm = new SolicitudCabeceraViewModel(_repoCliente.TraerTodos().ToList());
            return View(vm);
        }

        [HttpPost]
        public IActionResult Registrar(SolicitudCabeceraViewModel vm)
        {
            // se guardan los datos del view model en una sesion
            HttpContext.Session.Set<SolicitudCabecera>("SolicitudCabecera", vm.SolicitudCabecera);
            var sc = HttpContext.Session.Get<SolicitudCabecera>("SolicitudCabecera");

            // preparar el llenado de detalles
            HttpContext.Session.Set<List<SolicitudDetalle>>("ListaSolicitudDetalles", new List<SolicitudDetalle>());
            ViewBag.Detalles = HttpContext.Session.Get<List<SolicitudDetalle>>("ListaSolicitudDetalles");

            // se pasa a la siguiente vista, para el llenado del detalle
            var cliente = _repoCliente.TraerPorId(sc.ClienteId);
            ViewBag.Cliente = $"{cliente.Apellidos}, {cliente.Nombres} (Id: {cliente.Id})"; // interpolacion de cadenas
            ViewBag.Fecha = sc.FechaSolicitud.ToShortDateString();
            return View("RegistrarDetalle");
        }

        [HttpPost]
        public IActionResult RegistrarDetalle(SolicitudDetalle solicitudDetalle)
        {
            List<SolicitudDetalle> detalles = HttpContext.Session.Get<List<SolicitudDetalle>>("ListaSolicitudDetalles");
            detalles.Add(solicitudDetalle);
            HttpContext.Session.Set<List<SolicitudDetalle>>("ListaSolicitudDetalles", detalles);
            ViewBag.Detalles = HttpContext.Session.Get<List<SolicitudDetalle>>("ListaSolicitudDetalles");

            var sc = HttpContext.Session.Get<SolicitudCabecera>("SolicitudCabecera");
            var cliente = _repoCliente.TraerPorId(sc.ClienteId);
            ViewBag.Cliente = $"{cliente.Apellidos}, {cliente.Nombres} (Id: {cliente.Id})"; // interpolacion de cadenas
            ViewBag.Fecha = sc.FechaSolicitud.ToShortDateString();
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPedido()
        {
            // se hace la transaccion para guardar el pedido completo (cabecera + detalles)
            SolicitudCabecera cabecera = HttpContext.Session.Get<SolicitudCabecera>("SolicitudCabecera");
            List<SolicitudDetalle> detalles = HttpContext.Session.Get<List<SolicitudDetalle>>("ListaSolicitudDetalles");
            _uow.SolicitudCabeceraRepository.Agregar(cabecera);
            foreach (var detalle in detalles)
            {
                _uow.SolicitudDetalleRepository.Agregar(detalle);
            }
            await _uow.GuardarAsync();
            ViewBag.Mensaje = "Pedido guardado con exito";
            return View();
        }
    }
}
