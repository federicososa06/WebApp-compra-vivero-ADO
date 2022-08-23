using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;

namespace Dominio.InterfacesRepositorios
{
    public interface IRepositorioTipoPlanta : IRepositorio<TipoPlanta>
    {
        public TipoPlanta BuscarPorNombre(string nom);
    }
}
