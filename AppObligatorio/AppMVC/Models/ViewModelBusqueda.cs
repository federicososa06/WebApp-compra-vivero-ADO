using Dominio.EntidadesNegocio;
using System.Collections.Generic;

namespace AppMVC.Models
{
    public class ViewModelBusqueda
    {
        public Planta Planta { get; set; }

        public string NombreCientificoBuscado { get; set; }

        public int AlturaBuscada { get; set; }
        public int MetodoBuscado { get; set; }

        public List<Planta> ListaResultados { get; set; }

        public IEnumerable<TipoPlanta> ListaTipos { get; set; }
        public int idTipoSeleccionado { get; set; }

        public List<Ambiente> ListaAmbientes { get; set; }
        public Ambiente AmbienteSeleccionado { get; set; }
        
    
    
        // Buscar Todas
    }
}