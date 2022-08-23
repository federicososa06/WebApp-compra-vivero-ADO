using AppMVC.Models;
using CasosUso.InterfacesManejadores;
using Dominio.EntidadesNegocio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace AppMVC.Controllers
{
    public class PlantasController : Controller
    {
        public IManejadorPlanta ManejadorPlanta { get; set; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        public PlantasController(IManejadorPlanta manejador, IWebHostEnvironment whenv)
        {
            ManejadorPlanta = manejador;
            WebHostEnvironment = whenv;
        }

        // GET: PlantasController
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
                IEnumerable<Planta> listaPlantas = ManejadorPlanta.TraerTodasLasPLantas();
                return View(listaPlantas);
            }
        }

        // GET: PlantasController/Details/5
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
                Planta planta = ManejadorPlanta.BuscarPlantaPorId(id);
                return View(planta);
            }
        }

        // GET: PlantasController/Create
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
                ViewModelPlanta vmPlanta = new ViewModelPlanta();

                vmPlanta.ListaTipos = ManejadorPlanta.TraerTodosLosTipos();
                vmPlanta.ListaIluminacion = ManejadorPlanta.TraerTodosLasIluminaciones();

                return View(vmPlanta);
            }
        }

        // POST: PlantasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelPlanta vmPlanta)
        {
            //string sec = "";
            string sec = vmPlanta.Secuenciador.ToString().PadLeft(3, '0');

            string nomArchivo = vmPlanta.Planta.NombreCientifico + "_" + sec + ".jpg";
            vmPlanta.Planta.UrlFoto = nomArchivo.Replace(" ", "_");

            try
            {
                bool ok = ManejadorPlanta.AgregarPlanta(vmPlanta.Planta, vmPlanta.idTipoSeleccionado, vmPlanta.idTipoIluminacionSeleccionada, vmPlanta.listaNombres);
                if (ok)
                {
                    string rutaRaizApp = WebHostEnvironment.WebRootPath;
                    rutaRaizApp = Path.Combine(rutaRaizApp, "img");
                    string rutaCompleta = Path.Combine(rutaRaizApp, nomArchivo).Replace(" ", "_");
                    FileStream stream = new FileStream(rutaCompleta, FileMode.Create);
                    vmPlanta.Imagen.CopyTo(stream);

                    vmPlanta.Secuenciador++;

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "No se pudo agregar a la planta";
                    return View(vmPlanta);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantasController/Edit/5
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
                Planta planta = ManejadorPlanta.BuscarPlantaPorId(id);
                return View(planta);
            }
        }

        // POST: PlantasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Planta planta)
        {
            try
            {
                bool ok = ManejadorPlanta.ActualizarPlanta(planta);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "No se pudo actualizar a la planta";
                    return View(planta);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: PlantasController/Delete/5
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
                Planta planta = ManejadorPlanta.BuscarPlantaPorId(id);
                return View(planta);
            }
        }

        // POST: PlantasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Planta planta)
        {
            try
            {
                bool ok = ManejadorPlanta.EliminarPlanta(planta.Id);
                if (ok)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "No se pudo borrar a la planta";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult MostrarFichaCuidados(int id)
        {
            Planta planta = ManejadorPlanta.BuscarPlantaPorId(id);
            return View(planta);
        }

    }
}