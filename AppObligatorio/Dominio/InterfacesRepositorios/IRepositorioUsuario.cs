using Dominio.EntidadNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorios
{
    public interface IRepositorioUsuario
    {
        public Usuario BuscarUsuarioPorEmail(string email);
        public bool ValidarCredenciales(string email, string contra);
    }
}
