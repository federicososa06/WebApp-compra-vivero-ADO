using System.Collections.Generic;

namespace Dominio.EntidadesNegocio
{
    public class TipoIluminacion
    {

        // Sombra, luz solar directa, media sombra
        public int Id { get; set; }
        public string Tipo { get; set; }


        public override string ToString()
        {
            return this.Tipo;
        }

    }
}