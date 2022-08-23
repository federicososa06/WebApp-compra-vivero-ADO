using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Repositorios
{
    public class RepositorioTipoPlanta : IRepositorioTipoPlanta
    {
        private Conexion ManejadorConexion = new Conexion();

        public bool Add(TipoPlanta obj)
        {
            if (obj == null || !obj.Validar())
                return false;

            //validar que no exista el nombre del tipo de planta
            TipoPlanta existeTipoPlanta = BuscarPorNombre(obj.Nombre);

            //validar topes de descripción
            decimal topeDescaMax = ObtenerTopeDescMax("TipoPlantaTopeDescrMax");
            decimal topeDescaMin = ObtenerTopeDescMax("TipoPlantaTopeDescrMin");

            if (obj.ValidarParametrosDescripcion(topeDescaMin, topeDescaMax) && existeTipoPlanta == null)
            {
                SqlConnection cn = ManejadorConexion.CrearConexion();
                string sql = "INSERT INTO TipoPlantas VALUES (@nom, @desc)";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@desc", obj.Descripcion);

                try
                {
                    ManejadorConexion.AbrirConexion(cn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("No se pudo agregar");
                    return false;
                }
                finally
                {
                    ManejadorConexion.CerrarConexion(cn);
                }
            }
            return false;
        }

        public IEnumerable<TipoPlanta> FindAll()
        {
            List<TipoPlanta> listaTipoPlantas = new List<TipoPlanta>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM TipoPlantas";
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoPlanta tp = new TipoPlanta()
                    {
                        Id = (int)dr["IdTipoPlanta"],
                        Nombre = dr["NombreTipoPlanta"].ToString(),
                        Descripcion = dr["DescripcionTipoPlanta"].ToString()
                    };

                    listaTipoPlantas.Add(tp);
                }

                return listaTipoPlantas;
            }
            catch (Exception)
            {
                Debug.WriteLine("No se pudo traer los tipo de plantas");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public TipoPlanta FindById(object id)
        {
            TipoPlanta tpBuscada = null;
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM TipoPlantas WHERE IdTipoPlanta = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    tpBuscada = new TipoPlanta()
                    {
                        Id = (int)dr["IdTipoPlanta"],
                        Nombre = dr["NombreTipoPlanta"].ToString(),
                        Descripcion = dr["DescripcionTipoPlanta"].ToString()
                    };
                }

                return tpBuscada;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo encontrar el tipo de planta");
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
            string sql = "DELETE FROM TipoPlantas WHERE IdTipoPlanta = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo borrar el tipo de planta");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool Update(TipoPlanta obj)
        {
            if (obj == null || !obj.Validar())
                return false;

            //validar topes de descripción
            decimal topeDescaMax = ObtenerTopeDescMax("TipoPlantaTopeDescrMax");
            decimal topeDescaMin = ObtenerTopeDescMax("TipoPlantaTopeDescrMin");

            if(obj.ValidarParametrosDescripcion(topeDescaMin, topeDescaMax))
            {
                SqlConnection cn = ManejadorConexion.CrearConexion();
                string sql = "UPDATE TipoPlantas SET NombreTipoPlanta = @nom, " +
                                "DescripcionTipoPlanta = @desc " +
                                "WHERE IdTipoPlanta = @id";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@id", obj.Id);
                cmd.Parameters.AddWithValue("@nom", obj.Nombre);
                cmd.Parameters.AddWithValue("@desc", obj.Descripcion);

                try
                {
                    ManejadorConexion.AbrirConexion(cn);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("No se pudo actualizar el tipo de planta");
                    return false;
                }
                finally
                {
                    ManejadorConexion.CerrarConexion(cn);
                }
            }
            
            return false;
            
        }

        public TipoPlanta BuscarPorNombre(string nom)
        {
            TipoPlanta tpBuscada = null;
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM TipoPlantas WHERE NombreTipoPlanta = @nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nom", nom);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    tpBuscada = new TipoPlanta()
                    {
                        Id = (int)dr["IdTipoPlanta"],
                        Nombre = dr["NombreTipoPlanta"].ToString(),
                        Descripcion = dr["DescripcionTipoPlanta"].ToString()
                    };
                }

                return tpBuscada;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo encontrar el tipo de planta");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        // -- MÉTODOS AUXILIARES --
        // Comprobar parametros descripción, se accede desde el repoParametro:
        private RepositorioParametro repoParametro = new RepositorioParametro();

        private decimal ObtenerTopeDescMin(string nombreParametro)
        {
            decimal topeMin = decimal.Parse(repoParametro.GetValorParametro(nombreParametro));
            return topeMin;
        }

        private decimal ObtenerTopeDescMax(string nombreParametro)
        {
            decimal topeMax = decimal.Parse(repoParametro.GetValorParametro(nombreParametro));
            return topeMax;
        }

  
    }
}