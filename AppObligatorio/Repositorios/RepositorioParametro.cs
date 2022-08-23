using Dominio.InterfacesRepositorios;
using Dominio.ParametroConfiguracion;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Repositorios
{
    public class RepositorioParametro : IRepositorioParametro
    {
        private Conexion ManejadorConexion = new Conexion();

        public bool Add(Parametros obj)
        {
            if (obj == null || !obj.Validar())
                return false;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "INSERT INTO Parametros VALUES (@nom, @valor)";
            SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@nom", obj.Nombre);
            cmd.Parameters.AddWithValue("@valor", obj.Valor);

            try
            {
                if (ManejadorConexion.AbrirConexion(cn))
                    cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo hacer el alta de un parametro");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public IEnumerable<Parametros> FindAll()
        {
            List<Parametros> listaParametros = new List<Parametros>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM Parametros";
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Parametros nuevoParametro = new Parametros()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Valor = dr.GetDecimal(2)
                    };

                    listaParametros.Add(nuevoParametro);
                }

                return listaParametros;
            }
            catch (Exception)
            {
                Debug.WriteLine("No se pudo traer todos los parametros");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public Parametros FindById(object id)
        {
            Parametros buscado = null;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM Parametros WHERE Id=" + id + ";";
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    buscado = new Parametros()
                    {
                        Id = dr.GetInt32(0),
                        Nombre = dr.GetString(1),
                        Valor = dr.GetDecimal(2)
                    };
                }

                return buscado;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo obtener el parametro");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        //Devuelve el valor de un parametro según su nombre
        public string GetValorParametro(string nom)
        {
            string ret = "";

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT VALOR FROM Parametros WHERE Nombre= @nombreParametro";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nombreParametro", nom);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                ret = cmd.ExecuteScalar().ToString();
                return ret;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo obtener el parametro");
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
            string sql = "DELETE FROM Parametros WHERE Id=" + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo borrar el parametro");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }


        //Cambia el valor de un parametro existente
        public bool SetValorParametro(string nom, decimal nuevoVal)
        {
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "UPDATE Parametros SET Valor = @val WHERE Nombre = @nom";
            SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@nom", nom);
            cmd.Parameters.AddWithValue("@val", nuevoVal);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                Debug.WriteLine("No se pudo establecer el nuevo valor");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        //Se actualiza según el Id
        public bool Update(Parametros obj)
        {
            if (obj.Validar())
            {
                SqlConnection cn = ManejadorConexion.CrearConexion();
                string sql = "UPDATE Parametros SET Nombre = @nom, Valor = @val WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", obj.Id);
                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@val", obj.Valor);
                
                try
                {
                    ManejadorConexion.AbrirConexion(cn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("No se pudo actualizar el parametro");
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