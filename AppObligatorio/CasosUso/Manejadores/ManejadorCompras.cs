using System;
using System.Collections.Generic;
using System.Text;
using CasosUso.InterfacesManejadores;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;

namespace CasosUso.Manejadores
{
    public class ManejadorCompras:IManejadorCompra
    {

        public IRepositorioCompra RepoCompra { get; set; }

        public ManejadorCompras(IRepositorioCompra repo)
        {
            RepoCompra = repo;
        } 

        public bool AgregarNuevaCompra(Compra c)
        {
           return RepoCompra.Add(c);
        }

        public IEnumerable<Compra> TraerTodasLasCompras()
        {
            return RepoCompra.FindAll();
        }
    }
}
