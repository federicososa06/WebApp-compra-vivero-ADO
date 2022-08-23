using Dominio.EntidadesNegocio;
using System.Collections.Generic;

namespace CasosUso.InterfacesManejadores
{
    public interface IManejadorPlanta
    {
        public bool AgregarPlanta(Planta planta, int idTipo, int idIluminacion, string listaNom);

        public bool EliminarPlanta(object id);

        public bool ActualizarPlanta(Planta planta);

        public IEnumerable<Planta> TraerTodasLasPLantas();

        public Planta BuscarPlantaPorId(object id);

        public IEnumerable<TipoPlanta> TraerTodosLosTipos();

        public IEnumerable<TipoIluminacion> TraerTodosLasIluminaciones();
    }
}