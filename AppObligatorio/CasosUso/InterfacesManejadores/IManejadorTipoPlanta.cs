using Dominio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesManejadores
{
    public interface IManejadorTipoPlanta
    {
        public bool AgregarNuevoTipoPlanta(TipoPlanta tp);
        public IEnumerable<TipoPlanta> ListarTiposPlanta();
        public TipoPlanta BuscarTipoPlantaPorNombre(string nombre);
        public TipoPlanta BuscarPlantaPorId(object id);
        public bool EliminarTipoPlanta(object id);
        public bool ModificarDescripcionTipo(TipoPlanta tp);
    }
}
