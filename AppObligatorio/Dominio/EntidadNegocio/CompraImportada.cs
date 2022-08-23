using System.Linq;

namespace Dominio.EntidadesNegocio
{
    public class CompraImportada : Compra
    {
        public bool AmericaDelSur { get; set; }

        public string MedidasSanitarias { get; set; }

        private static decimal TasaImportacion;

        private static decimal TasaArancelesAmerica;

        public override string Tipo()
        {
            return "Importada";
        }

        public override decimal CalcularTotalCompra()
        {
            decimal total = 0;

            foreach (Item item in this.ListaItems)
                total += item.TotalItem();

            total += TasaImportacion * total / 100;

            if (AmericaDelSur)
                total -= TasaArancelesAmerica * total / 100;

            return total;
        }

        public override bool Validar()
        {
            return !string.IsNullOrEmpty(this.MedidasSanitarias) && 
                    this.Fecha != null && 
                    this.ListaItems != null && 
                    this.ListaItems.Count() > 0 ; //no puede haber compra sin items
        }

        public override string ToString()
        {
            return $"{this.Id} - {this.Tipo()} - {this.AmericaDelSur} - {this.MedidasSanitarias} ";
        }
    }
}