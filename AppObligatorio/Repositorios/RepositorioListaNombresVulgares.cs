using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Repositorios
{
    public class RepositorioListaNombresVulgares : IRepositorioListaNombresVulgares
    {
        private Conexion ManejadorConexion = new Conexion();

        public bool Add(NombreVulgar obj)
        {
            if (obj != null)
            {
                SqlConnection cn = ManejadorConexion.CrearConexion();
                string sql = "INSERT INTO ListaNombresVulgares VALUES (@nomVulg, @idPlanta)";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nomVulg", obj.Nombre);
                cmd.Parameters.AddWithValue("@idPlanta", obj.IdPlanta);

                try
                {
                    ManejadorConexion.AbrirConexion(cn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("No se pudo agregar un nuevo nombre vulgar");
                    return false;
                }
                finally
                {
                    ManejadorConexion.CerrarConexion(cn);
                }
            }
            return false;
        }

        public IEnumerable<NombreVulgar> FindAll()
        {
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM ListaNombresVulgares";
            SqlCommand cmd = new SqlCommand(sql, cn);

            List<NombreVulgar> listaNombres = new List<NombreVulgar>();
            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    NombreVulgar nuevoNom = new NombreVulgar()
                    {
                        Nombre = dr["NombreVulg"].ToString(),
                        IdPlanta = (int)dr["IdPlanta"]
                    };
                    listaNombres.Add(nuevoNom);
                }

                return listaNombres;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer la lista");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public NombreVulgar FindById(object id)
        {
            NombreVulgar nuevoNom;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM ListaNombresVulgares WHERE IdNomVulg = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    nuevoNom = new NombreVulgar()
                    {
                        Nombre = dr["NombreVulg"].ToString(),
                        IdPlanta = (int)dr["IdPlanta"]
                    };
                    return nuevoNom;
                }

                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer la lista");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool Remove(object id)
        {
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "DELETE FROM ListaNombresVulgares WHERE IdPlanta = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo eliminar");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool Update(NombreVulgar obj)
        {
            if (obj != null)
            {
                SqlConnection cn = ManejadorConexion.CrearConexion();
                string sql = "UPDATE ListaNombresVulgares SET NombreVulg = @nom, IdPlanta = @idPlanta WHERE IdNomVulg = @idNom";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@idPlanta", obj.IdPlanta);
                cmd.Parameters.AddWithValue("@idNom", obj.Id);

                try
                {
                    ManejadorConexion.AbrirConexion(cn);
                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("No se pudo actualizar");
                    return false;
                }
                finally
                {
                    ManejadorConexion.CerrarConexion(cn);
                }
            }

            return false;
        }
    }
}