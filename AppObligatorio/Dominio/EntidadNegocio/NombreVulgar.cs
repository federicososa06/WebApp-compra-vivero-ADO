using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.EntidadesNegocio
{
    public class NombreVulgar
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPlanta { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
