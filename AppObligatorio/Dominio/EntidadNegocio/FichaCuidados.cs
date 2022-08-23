using Dominio.EntidadesNegocio;
using Dominio.OtrasInterfaces;

namespace Dominio.EntidadesNegocio
{
	public class FichaCuidados : IValidable
	{
		public int Id { get; set; }

		public string FrecuenciaRiegoUnidadTiempo { get; set; }

		public int FrecuenciaRiegoCantidad { get; set; }

		public decimal Temperatura { get; set; }

		public TipoIluminacion Iluminacion { get; set; }

		public int IdPlanta { get; set; }

        public bool Validar()
        {
			return !string.IsNullOrEmpty(FrecuenciaRiegoUnidadTiempo) &&
					this.FrecuenciaRiegoCantidad > 0 &&
					this.Iluminacion != null;
					//temperatura no se valida, puede tomar valores negativos
        }

        public override string ToString()
        {
			return $"{this.FrecuenciaRiegoCantidad} x {this.FrecuenciaRiegoUnidadTiempo} - {this.Temperatura} - {this.Iluminacion.ToString()}";

		}
    }

}

