using Dominio.EntidadNegocio;
using Dominio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using System;
using System.Diagnostics;

namespace Repositorios
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private Conexion ManejadorConexion = new Conexion();

        public Usuario BuscarUsuarioPorEmail(string email)
        {
            Usuario usuarioBuscado = new Usuario();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM Usuarios WHERE Email = @email";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@email", email);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuarioBuscado.Email = dr["Email"].ToString();
                    usuarioBuscado.Contrasenia = dr["Contrasenia"].ToString();
                    return usuarioBuscado;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo encontrar el usuario");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool ValidarCredenciales(string email, string contra)
        {
            Usuario usu = BuscarUsuarioPorEmail(email);

            if (usu.Contrasenia == contra)
                return true;
            else
                return false;
        }
    }
}