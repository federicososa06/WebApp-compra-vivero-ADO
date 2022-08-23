using Dominio.EntidadesNegocio;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AppMVC.Models
{
    public class ViewModelPlanta
    {
        public Planta Planta { get; set; }
        public IFormFile Imagen { get; set; }
        public int Secuenciador = 1;

        public IEnumerable<TipoPlanta> ListaTipos { get; set; }
        public int idTipoSeleccionado { get; set; }

        public IEnumerable<TipoIluminacion> ListaIluminacion { get; set; }
        public int idTipoIluminacionSeleccionada { get; set; }

        public string listaNombres { get; set; }
    }
}