using CasosUso.InterfacesManejadores;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.Manejadores
{
    public class ManejadorBusqueda : IManejadorBusqueda
    {
        public IRepositorioPlanta RepoPlanta { get; set; }
        public ManejadorBusqueda(IRepositorioPlanta repo)
        {
            RepoPlanta = repo;
        }
        public Planta BuscarPlantaPorNombreCientifico(string nom)
        {
            return RepoPlanta.BuscarPlantaPorNombreCientifico(nom);
        }

        public IEnumerable<Planta> BuscarPlantasMasAltas(int altura)
        {
            return RepoPlanta.BuscarPlantasMasAltas(altura);
        }

        public IEnumerable<Planta> BuscarPlantasMasBajas(int altura)
        {
            return RepoPlanta.BuscarPlantasMasBajas(altura);
        }

        public IEnumerable<Planta> BuscarPorAmbiente(Ambiente amb)
        {
            return RepoPlanta.BuscarPorAmbiente(amb);
        }

        public IEnumerable<Planta> BuscarPorNombre(string nom)
        {
            return RepoPlanta.BuscarPorNombre(nom);
        }

        public IEnumerable<Planta> BuscarPorTipo(int idTipo)
        {
            return RepoPlanta.BuscarPorTipo(idTipo);
        }

        public IEnumerable<Planta> BuscarTodas()
        {
            return RepoPlanta.FindAll();
        }
    }
}
