using FinancieraAcme.PrestaFacil.Domain.Entities;
using FinancieraAcme.PrestaFacil.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancieraAcme.PrestaFacil.UI.Web.Controllers
{
    public class SolicitudPrestamoController : Controller
    {
        private readonly ISolicitudPrestamoRepository _repo;

        public SolicitudPrestamoController(ISolicitudPrestamoRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            List<SolicitudPrestamo> solicitudes = _repo.TraerTodos().ToList();
            //ViewData["Lista"] = solicitudes;
            return View(solicitudes);
        }

        public IActionResult Detalles(int id)
        {
            SolicitudPrestamo solicitud = _repo.TraerPorId(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return View(solicitud);
        }

        public IActionResult Editar(int id)
        {
            SolicitudPrestamo solicitud = _repo.TraerPorId(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return View(solicitud);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(SolicitudPrestamo solicitud)
        {
            if (!ModelState.IsValid)
            {
                return View(solicitud);
            }
            _repo.Editar(solicitud);
            try
            {
                await _repo.GuardarAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_repo.TraerPorId(solicitud.Id) == null)
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction("Index");
            // otra opcion
            //ViewData["Mensaje"] = "Solicitud editada con exito!";
            //return View(solicitud);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(SolicitudPrestamo solicitud)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _repo.Agregar(solicitud);
            await _repo.GuardarAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            SolicitudPrestamo solicitud=_repo.TraerPorId(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return View(solicitud);
        }

        [HttpPost]
        [ActionName("Eliminar")]
        public async Task<IActionResult> EliminarSolicitud(int id)
        {
            _repo.Eliminar(id);
            await _repo.GuardarAsync();
            return RedirectToAction("Index");
        }
    }
}
