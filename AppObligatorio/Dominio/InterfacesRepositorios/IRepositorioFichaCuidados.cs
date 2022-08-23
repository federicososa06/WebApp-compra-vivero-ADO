using Dominio.EntidadesNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorios
{
   public interface IRepositorioFichaCuidados :IRepositorio<FichaCuidados>
    {
        public IEnumerable<TipoIluminacion> TraerTipoListaIluminaciones();
        public TipoIluminacion TraerIluminacionPorId(object id);
    }
}
