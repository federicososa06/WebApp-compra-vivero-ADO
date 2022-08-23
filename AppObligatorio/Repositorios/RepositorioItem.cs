using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Repositorios
{
    public class RepositorioItem : IRepositorioItem
    {
        private Conexion ManejadorConexion = new Conexion();

        public bool Add(Item obj)
        {
            if (obj == null || !obj.Validar())
                return false;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "INSERT INTO Items VALUES(@cant, @precioUni, @idPlanta)";
            SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@cant", obj.Cantidad);
            cmd.Parameters.AddWithValue("@precioUni", obj.PrecioUnitario);
            cmd.Parameters.AddWithValue("@idPlanta", obj.PlantaComprada.Id);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo agregar el item");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public IEnumerable<Item> FindAll()
        {
            List<Item> listaItems = new List<Item>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM Items ";
            SqlCommand cmd = new SqlCommand(sql, cn);

            IRepositorioPlanta repoPlanta = new RepositorioPlanta();

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Item buscado = new Item
                    {
                        Cantidad = (int)dr["Cantidad"],
                        PrecioUnitario = (decimal)dr["PrecioUnitario"],
                        PlantaComprada = repoPlanta.FindById((int)dr["IdPanta"]),
                        IdCompra = (int)dr["IdCompra"]
                    };
                    listaItems.Add(buscado);
                }

                return listaItems;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se encontró el item");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public Item FindById(object id)
        {
            Item buscado = null;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM Items WHERE IdItem = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            IRepositorioPlanta repoPlanta = new RepositorioPlanta();

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    buscado = new Item
                    {
                        Cantidad = (int)dr["Cantidad"],
                        PrecioUnitario = (decimal)dr["PrecioUnitario"],
                        PlantaComprada = repoPlanta.FindById((int)dr["IdPanta"]),
                        IdCompra = (int)dr["IdCompra"]
                    };
                }

                return buscado;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se encontró el item");
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
            string sql = "DELETE FROM Items WHERE IdItem = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo borrar el item");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool Update(Item obj)
        {
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "UPDATE Items SET Cantidad = @cant, PrecioUnitario = @precio WHERE IdItem = @idItem";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@cant", obj.Cantidad);
            cmd.Parameters.AddWithValue("@precio", obj.PrecioUnitario);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo actualzar");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }
    }
}