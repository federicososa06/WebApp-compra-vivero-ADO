using Dominio.EntidadesNegocio;
using Dominio.OtrasInterfaces;

namespace Dominio.EntidadesNegocio
{
	public class TipoPlanta : IValidable, IValidarDescripcion
	{
		public int Id { get; set; }
		public string Nombre{ get; set; }

		public string Descripcion{ get; set; }

        private static int TopeDescMin = 10;

        private static int TopeDescMax = 200;

        public bool Validar()
        {
            return !string.IsNullOrEmpty(this.Nombre) &&
                !string.IsNullOrEmpty(this.Descripcion);
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
            return $"{this.Nombre} - {this.Descripcion}";
        }
    }

}

