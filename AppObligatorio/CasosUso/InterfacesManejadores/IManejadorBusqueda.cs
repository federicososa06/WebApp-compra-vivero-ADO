using Dominio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesManejadores
{
    public interface IManejadorBusqueda
    {
        public IEnumerable<Planta> BuscarPorNombre(string nom);
        public IEnumerable<Planta> BuscarPorTipo(int idTipo);
        public IEnumerable<Planta> BuscarPorAmbiente(Ambiente amb);
        public IEnumerable<Planta> BuscarPlantasMasAltas(int altura);
        public IEnumerable<Planta> BuscarPlantasMasBajas(int altura);
        public Planta BuscarPlantaPorNombreCientifico(string nom);

        public IEnumerable<Planta> BuscarTodas();
    }
}
