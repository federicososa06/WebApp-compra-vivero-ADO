using CasosUso.InterfacesManejadores;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using Dominio.ParametroConfiguracion;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.Manejadores
{
   public class ManejadorParametros : IManejadorParametro
    {
        public IRepositorioParametro RepoParametros { get; set; }

        public ManejadorParametros(IRepositorioParametro repo)
        {
            RepoParametros = repo;
        }
      
    }
}
