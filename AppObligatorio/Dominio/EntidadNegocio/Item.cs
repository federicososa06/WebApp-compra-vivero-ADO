using Dominio.OtrasInterfaces;
using System;
using System.Collections.Generic;

namespace Dominio.EntidadesNegocio
{
    public class Item : IValidable
    {
        public int Id { get; set; }

        public int IdCompra { get; set; }

        public Planta PlantaComprada { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        //CONSTRUCTOR
        //	 para crear un item hay que pasar los siguientes parametros:
        //public Item(Planta planta, int cantidad, decimal precio)
        //{
        //	this.PlantaComprada = planta;
        //	this.Cantidad = cantidad;
        //	this.PrecioUnitario = precio;
        //}

        public decimal TotalItem()
        {
            return this.PrecioUnitario * this.Cantidad;
        }

        public bool Validar()
        {
            return PlantaComprada != null && Cantidad > 0 && PrecioUnitario > 0;
        }

        public static implicit operator Item(List<Item> v)
        {
            throw new NotImplementedException();
        }

        //public override string ToString()
        //{
        //    return this.PlantaComprada.NombreCientifico;
        //}
    }
}