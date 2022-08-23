using Dominio.EntidadesNegocio;
using System.Collections;
using System.Collections.Generic;

namespace Dominio.InterfacesRepositorios
{
	public interface IRepositorioPlanta : IRepositorio<Planta>
	{
        public IEnumerable<Planta> BuscarPorNombre(string nom);
        public IEnumerable<Planta> BuscarPorTipo(int idTipo);
        public IEnumerable<Planta> BuscarPorAmbiente(Ambiente amb);
        public IEnumerable<Planta> BuscarPlantasMasAltas(int altura);
        public IEnumerable<Planta> BuscarPlantasMasBajas(int altura);
        public Planta BuscarPlantaPorNombreCientifico(string nom);

    }

}

