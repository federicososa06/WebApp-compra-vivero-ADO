using CasosUso.InterfacesManejadores;
using Dominio.EntidadNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppMVC.Controllers
{
    public class UsuariosController : Controller
    {
        public IManejadorUsuario ManejadorUsuario { get; set; }

        public UsuariosController(IManejadorUsuario manejador)
        {
            ManejadorUsuario = manejador;
        }

        public IActionResult Index()
        {
            return View("LogIn");
        }

        [HttpPost]
        public IActionResult Index(Usuario usuRecibido)
        {
            Usuario usuarioIngresado = ManejadorUsuario.BuscarUsuarioPorEmail(usuRecibido.Email);

            if (usuarioIngresado != null)
            {
                bool usuarioCorrecto = ManejadorUsuario.ValidarCredenciales(usuarioIngresado.Email, usuRecibido.Contrasenia);

                if (usuarioCorrecto)
                {
                    HttpContext.Session.SetString("usuario", usuarioIngresado.Email); //guarda el nombre de usuario que ingreso
                    return RedirectToAction("Index", "Home"); //lleva al index del home
                }
                else
                {
                    ViewBag.ErrorPass = "Verificar contraseña";
                    return View("LogIn");
                }
            }
            ViewBag.ErrorUsuario = "Verificar usuario";
            return View("LogIn");
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return View("LogIn");
        }
    }
}