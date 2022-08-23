using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.OtrasInterfaces
{
    public interface IValidarDescripcion
    {
        public bool ValidarParametrosDescripcion(decimal min, decimal max);
    }
}
