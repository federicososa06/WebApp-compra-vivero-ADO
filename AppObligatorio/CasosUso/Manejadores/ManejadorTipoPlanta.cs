using CasosUso.InterfacesManejadores;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.Manejadores
{
    public class ManejadorTipoPlanta : IManejadorTipoPlanta
    {
        public IRepositorioTipoPlanta RepoTipoPlanta { get; set; }

        public ManejadorTipoPlanta(IRepositorioTipoPlanta repo)
        {
            RepoTipoPlanta = repo;
        }

        public bool AgregarNuevoTipoPlanta(TipoPlanta tp)
        {
            return RepoTipoPlanta.Add(tp);
        }

        public IEnumerable<TipoPlanta> ListarTiposPlanta()
        {
            return RepoTipoPlanta.FindAll();
        }

        public TipoPlanta BuscarTipoPlantaPorNombre(string nombre)
        {
            return RepoTipoPlanta.BuscarPorNombre(nombre); ;
        }

        public bool EliminarTipoPlanta(object id)
        {
           return RepoTipoPlanta.Remove(id);
        }

        public bool ModificarDescripcionTipo(TipoPlanta tp)
        {
            return RepoTipoPlanta.Update(tp);
        }

        public TipoPlanta BuscarPlantaPorId(object id)
        {
            return RepoTipoPlanta.FindById(id);
        }
    }
}
