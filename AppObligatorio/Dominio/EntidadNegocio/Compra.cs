using System;
using Dominio.EntidadesNegocio;
using System.Collections.Generic;
using Dominio.OtrasInterfaces;

namespace Dominio.EntidadesNegocio
{
	public abstract class Compra:IValidable
	{
		public int Id { get; set; }

		public DateTime Fecha{ get; set; }

		public IEnumerable<Item> ListaItems { get; set; }

        public abstract decimal CalcularTotalCompra();

        public abstract string Tipo(); //devuelve el tipo de compra (plaza o importada)

		public abstract bool Validar();

        //public static implicit operator Compra(Compra v) => throw new NotImplementedException();
    }

}

