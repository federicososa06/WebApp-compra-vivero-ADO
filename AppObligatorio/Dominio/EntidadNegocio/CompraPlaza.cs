using Dominio.EntidadesNegocio;
using System.Linq;

namespace Dominio.EntidadesNegocio
{
	public class CompraPlaza : Compra
	{
		public decimal CostoFlete{ get; set; }

		private static decimal TasaIva;

        public override string Tipo()
        {
            return "Plaza";
        }

        public override decimal CalcularTotalCompra()
        {
            decimal total = 0;

            foreach (Item item in this.ListaItems)
                total += item.TotalItem();

            total += TasaIva * total / 100;
            total += this.CostoFlete;

            return total;
        }

        public override bool Validar()
        {
            return this.CostoFlete >= 0 && 
                   this.Fecha != null && 
                   this.ListaItems != null &&
                   this.ListaItems.Count() > 0; //no puede haber compra sin items
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Tipo()} - {this.CostoFlete} ";
        }
    }

}

