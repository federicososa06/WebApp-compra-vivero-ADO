using System;
using System.Collections.Generic;
using System.Text;
using CasosUso.InterfacesManejadores;
using Dominio.EntidadNegocio;
using Dominio.InterfacesRepositorios;

namespace CasosUso.Manejadores
{
    public class ManejadorUsuario : IManejadorUsuario
    {
        public IRepositorioUsuario RepoUsuario { get; set; }
        public ManejadorUsuario(IRepositorioUsuario repoUsu)
        {
            RepoUsuario = repoUsu;
        }
        public Usuario BuscarUsuarioPorEmail(string email)
        {
            return RepoUsuario.BuscarUsuarioPorEmail(email);
        }

        public bool ValidarCredenciales(string email, string contra)
        {
            return RepoUsuario.ValidarCredenciales(email, contra);
        }
    }
}
