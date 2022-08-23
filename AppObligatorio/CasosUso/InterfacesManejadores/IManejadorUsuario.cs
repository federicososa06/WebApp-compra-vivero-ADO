using Dominio.EntidadNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace CasosUso.InterfacesManejadores
{
    public interface IManejadorUsuario
    {
        public Usuario BuscarUsuarioPorEmail(string email);
        public bool ValidarCredenciales(string email, string contra);
    }
}
