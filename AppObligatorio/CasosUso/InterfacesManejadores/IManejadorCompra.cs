using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;

namespace CasosUso.InterfacesManejadores
{
    public interface IManejadorCompra
    {
        bool AgregarNuevaCompra(Compra c);
        IEnumerable<Compra> TraerTodasLasCompras();

    }
}
