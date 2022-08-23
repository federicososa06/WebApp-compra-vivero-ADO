using CasosUso.InterfacesManejadores;
using Dominio.EntidadesNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AppMVC.Controllers
{
    public class TipoPlantaController : Controller
    {
        public IManejadorTipoPlanta ManejadorTipoPlanta { get; set; }

        public TipoPlantaController(IManejadorTipoPlanta manejador)
        {
            ManejadorTipoPlanta = manejador;
        }

        // GET: TipoPlantaController
        public ActionResult Index()
        {            
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                IEnumerable<TipoPlanta> listaTipoPlantas = ManejadorTipoPlanta.ListarTiposPlanta();
                if (listaTipoPlantas != null && listaTipoPlantas.Count() > 0)
                    return View(listaTipoPlantas);
                else
                {
                    ViewBag.Error = "No existe ningun tipo de planta";
                    return View();
                }
            }
        }

        // GET: TipoPlantaController/Details/5
        public ActionResult Details(int id)
        {            
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                TipoPlanta tp = ManejadorTipoPlanta.BuscarPlantaPorId(id);
                return View(tp);
            }
        }

        // GET: TipoPlantaController/Create
        public ActionResult Create()
        {
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                TipoPlanta tp = new TipoPlanta();
                return View(tp);
            }
        }

        // POST: TipoPlantaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoPlanta tp)
        {
            try
            {
                bool ok = ManejadorTipoPlanta.AgregarNuevoTipoPlanta(tp);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "No se pudo agregar el tipo de planta";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoPlantaController/Edit/5
        public ActionResult Edit(int id)
        {
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                TipoPlanta tp = ManejadorTipoPlanta.BuscarPlantaPorId(id);
                return View(tp);
            }
        }

        // POST: TipoPlantaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoPlanta tp)
        {
            try
            {
                bool ok = ManejadorTipoPlanta.ModificarDescripcionTipo(tp);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "No se pudo actualizar la descripcion de la planta. No cumple con los topes de descripción";
                    TipoPlanta tipoPlanta = ManejadorTipoPlanta.BuscarPlantaPorId(tp.Id);
                    return View(tipoPlanta);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoPlantaController/Delete/5
        public ActionResult Delete(int id)
        {
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                TipoPlanta tp = ManejadorTipoPlanta.BuscarPlantaPorId(id);
                return View(tp);
            }
        }

        // POST: TipoPlantaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TipoPlanta tp)
        {
            try
            {
                bool ok = ManejadorTipoPlanta.EliminarTipoPlanta(tp.Id);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TipoPlanta tipoPlanta = ManejadorTipoPlanta.BuscarPlantaPorId(tp.Id);
                    ViewBag.Error = "No se puede eliminar la planta. Hay plantas con este Tipo de Planta";
                    return View(tipoPlanta);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoPlantaController/Create
        public ActionResult BuscarPorNombre()
        {   
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View();
            }
        }

        // POST: TipoPlantaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorNombre(string nombreBuscado)
        {
            try
            {
                TipoPlanta buscada = ManejadorTipoPlanta.BuscarTipoPlantaPorNombre(nombreBuscado);
                if (buscada != null)
                {
                    return View("Details", buscada);
                }
                else
                {
                    ViewBag.Error = "No se encontró el tipo de planta";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}