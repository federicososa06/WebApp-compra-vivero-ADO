using AppMVC.Models;
using CasosUso.InterfacesManejadores;
using Dominio.EntidadesNegocio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AppMVC.Controllers
{
    public class BusquedaController : Controller
    {
        public IManejadorBusqueda ManejadorBusqueda { get; set; }
        public IManejadorPlanta ManejadorPlanta { get; set; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        public BusquedaController(IManejadorBusqueda manejador, IManejadorPlanta manePlanta, IWebHostEnvironment whenv)
        {
            ManejadorBusqueda = manejador;
            ManejadorPlanta = manePlanta;
            WebHostEnvironment = whenv;
        }

        // GET: BusquedaController
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
                ViewModelBusqueda vm = new ViewModelBusqueda();

                //pasarle la lista de los tipos de planta
                vm.ListaTipos = ManejadorPlanta.TraerTodosLosTipos();
                return View(vm);
            }
        }

        // GET: BusquedaController/BuscarPorNombreCientifico
        public ActionResult BuscarPorNombreCientifico()
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

        // POST: BusquedaController/BuscarPorNombreCientifico
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorNombreCientifico(ViewModelBusqueda vmRecibido) 
        {
            try
            {
                //resultados
                ViewModelBusqueda vmBusqueda = new ViewModelBusqueda();
                Planta plantaResultado = ManejadorBusqueda.BuscarPlantaPorNombreCientifico(vmRecibido.NombreCientificoBuscado);

                //pasarle la lista de los tipos de planta
                vmBusqueda.ListaTipos = ManejadorPlanta.TraerTodosLosTipos();

                if (plantaResultado != null)
                {
                    vmBusqueda.ListaResultados = new List<Planta>();
                    vmBusqueda.ListaResultados.Add(plantaResultado);
                   
                    return View("Index", vmBusqueda);
                }
                else
                {
                    ViewBag.ErrorNom = "No se encontró la planta";
                    return View("Index", vmBusqueda);
                }
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: BusquedaController/BuscarPorAltura
        public ActionResult BuscarPorAltura()
        {
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View("Index");
            }
        }

        // POST: BusquedaController/BuscarPorAltura
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorAltura(ViewModelBusqueda vmRecibido) 
        {
            try
            {
                //resultados
                //determinar el metodo seleccionado
                ViewModelBusqueda vmBusqueda = new ViewModelBusqueda();
                if (vmRecibido.MetodoBuscado == 1)
                    vmBusqueda.ListaResultados = (List<Planta>)ManejadorBusqueda.BuscarPlantasMasBajas(vmRecibido.AlturaBuscada);
                else if (vmRecibido.MetodoBuscado == 2)
                    vmBusqueda.ListaResultados = (List<Planta>)ManejadorBusqueda.BuscarPlantasMasAltas(vmRecibido.AlturaBuscada);

                //pasarle la lista de los tipos de planta
                vmBusqueda.ListaTipos = ManejadorPlanta.TraerTodosLosTipos();

                if (vmBusqueda.ListaResultados != null && vmBusqueda.ListaResultados.Count() > 0)
                {
                    return View("Index", vmBusqueda);
                }
                else
                {
                    ViewBag.ErrorAlt = "No se encontró la planta";
                    return View("Index", vmBusqueda);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BusquedaController/BuscarPorTipo
        public ActionResult BuscarPorTipo()
        {
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View("Index");
            }
        }

        // POST: BusquedaController/BuscarPorTipo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorTipo(ViewModelBusqueda vmRecibido)
        {
            try
            {
                //resultados
                ViewModelBusqueda vmBusqueda = new ViewModelBusqueda();
                vmBusqueda.ListaResultados = (List<Planta>)ManejadorBusqueda.BuscarPorTipo(vmRecibido.idTipoSeleccionado);

                //pasarle la lista de los tipos de planta
                vmBusqueda.ListaTipos = ManejadorPlanta.TraerTodosLosTipos(); 
                

                if (vmBusqueda.ListaResultados != null && vmBusqueda.ListaResultados.Count()>0)
                {
                    return View("Index", vmBusqueda);
                }
                else
                {
                    ViewBag.ErrorTipo = "No se encontró la planta";
                    return View("Index", vmBusqueda);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: BusquedaController/BuscarPorAmbiente
        public ActionResult BuscarPorAmbiente(int id)
        {
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                return View("Index");
            }
        }

        // POST: BusquedaController/BuscarPorAmbiente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscarPorAmbiente(ViewModelBusqueda vmRecibido) 
        {
            try
            {
                //resultados
                ViewModelBusqueda vmBusqueda = new ViewModelBusqueda();
                vmBusqueda.ListaResultados = (List<Planta>)ManejadorBusqueda.BuscarPorAmbiente(vmRecibido.AmbienteSeleccionado);

                //pasarle la lista de los tipos de planta
                vmBusqueda.ListaTipos = ManejadorPlanta.TraerTodosLosTipos();

                if (vmBusqueda.ListaResultados != null && vmBusqueda.ListaResultados.Count() > 0)
                {
                    return View("Index", vmBusqueda);
                }
                else
                {
                    ViewBag.ErrorAmb = "No se encontró la planta";
                    return View("Index", vmBusqueda);
                }
            }
            catch
            {
                return View("Index");
            }
        }

        // GET: BusquedaController/BuscarTodas
        public ActionResult BuscarTodas()
        {            
            // limitar acceso por url
            string sesion = HttpContext.Session.GetString("usuario");
            if (sesion == null)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                //resultados
                ViewModelBusqueda vmBusqueda = new ViewModelBusqueda();
                vmBusqueda.ListaResultados = (List<Planta>)ManejadorBusqueda.BuscarTodas();

                //pasarle la lista de los tipos de planta
                vmBusqueda.ListaTipos = ManejadorPlanta.TraerTodosLosTipos();

                if (vmBusqueda.ListaResultados != null && vmBusqueda.ListaResultados.Count() > 0)
                {
                    return View("Index", vmBusqueda);
                }
                else
                {
                    ViewBag.ErrorTodas = "No se encontró la planta";
                    return View("Index", vmBusqueda);
                }
            }
        }
    }
}