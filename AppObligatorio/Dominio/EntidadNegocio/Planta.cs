using Dominio.OtrasInterfaces;
using System.Collections.Generic;

namespace Dominio.EntidadesNegocio
{
    public class Planta : IValidable, IValidarDescripcion
    {
        public int Id { get; set; }

        public TipoPlanta Tipo { get; set; }

        public string NombreCientifico { get; set; }

        public List<NombreVulgar> ListaNombreVulgares { get; set; }

        public string Descripcion { get; set; }

        public static int TopeDescMin = 10;

        public static int TopeDescMax = 500;

        public int AlturaMax { get; set; }

        public string UrlFoto { get; set; }

        public FichaCuidados Cuidados { get; set; }

        public Ambiente Ambiente { get; set; }

        public bool Validar()
        {
            return !string.IsNullOrEmpty(this.NombreCientifico);
                //&& this.Tipo != null;
        }

        public bool ValidarParametrosDescripcion(decimal min, decimal max)
        {
            if (this.Descripcion.Length >= min && this.Descripcion.Length <= max)
                return true;
            else
                return false;
        }


        public override string ToString()
        {
            return ($"{this.Id} - " +
                    $"{this.NombreCientifico} - {this.Descripcion} - " +
                    $"{this.AlturaMax} - {this.UrlFoto} - " +
                    $"{this.Ambiente}");
        }

    }
}