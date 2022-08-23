using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Repositorios
{
    public class RepositorioFichaCuidados : IRepositorioFichaCuidados
    {
        private Conexion ManejadorConexion = new Conexion();

        // método Add -> sin uso, solo se puede agregar una ficha desde el repo plantas por la foreign key
        public bool Add(FichaCuidados obj)
        {
            if (obj == null || !obj.Validar())
                return false;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sqlFicha = "INSERT INTO FichaCuidados VALUES (@idPlanta, @frecRiegoTi, @frecRiegoCant, @temp, @idIlum) ";
            SqlCommand cmdFicha = new SqlCommand(sqlFicha, cn);

            cmdFicha.Parameters.AddWithValue("@frecRiegoTi", obj.FrecuenciaRiegoUnidadTiempo);
            cmdFicha.Parameters.AddWithValue("@frecRiegoCant", obj.FrecuenciaRiegoCantidad);
            cmdFicha.Parameters.AddWithValue("@idIlum", obj.Iluminacion.Id);
            cmdFicha.Parameters.AddWithValue("@temp", obj.Temperatura);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmdFicha.ExecuteNonQuery();
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

        public TipoIluminacion TraerIluminacionPorId(object id)
        {
            TipoIluminacion buscado;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM TipoIluminacion WHERE IdTipoIluminacion = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    buscado = new TipoIluminacion()
                    {
                        Id = (int)dr["IdTipoIluminacion"],
                        Tipo = dr["Tipo"].ToString()
                    };
                    return buscado;
                }
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo encontrar el tipo de iluminación");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public IEnumerable<FichaCuidados> FindAll()
        {
            List<FichaCuidados> listaRetorno = new List<FichaCuidados>();
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM FichaCuidados";
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    FichaCuidados nuevaFicha = new FichaCuidados()
                    {
                        IdPlanta = (int)dr["IdPlanta"],
                        FrecuenciaRiegoUnidadTiempo = dr["FrecuenciaRiegoUnidadTiempo"].ToString(),
                        FrecuenciaRiegoCantidad = (int)dr["FrecuenciaRiegoCantidad"],
                        Temperatura = (int)dr["Temperatura"],
                        Iluminacion = TraerIluminacionPorId((int)dr["idIluminacion"])
                    };
                    listaRetorno.Add(nuevaFicha);
                }
                return listaRetorno;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se puede listar las fichas de cuidado");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public FichaCuidados FindById(object id)
        {
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM FichaCuidados WHERE idPlanta = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    FichaCuidados fichaRetorno = new FichaCuidados()
                    {
                        IdPlanta = (int)dr["IdPlanta"],
                        FrecuenciaRiegoUnidadTiempo = dr["FrecuenciaRiegoUnidadTiempo"].ToString(),
                        FrecuenciaRiegoCantidad = (int)dr["FrecuenciaRiegoCantidad"],
                        Temperatura = (int)dr["Temperatura"],
                        Iluminacion = TraerIluminacionPorId((int)dr["idIluminacion"])
                    };
                    return fichaRetorno;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se puede listar las fichas de cuidado");
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
            string sql = "DELETE FROM FichaCuidados WHERE IdPlanta = " + id;
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

        public IEnumerable<TipoIluminacion> TraerTipoListaIluminaciones()
        {
            List<TipoIluminacion> lista = new List<TipoIluminacion>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM TipoIluminacion";
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoIluminacion nuevoTipo = new TipoIluminacion()
                    {
                        Id = (int)dr["IdTipoIluminacion"],
                        Tipo = dr["Tipo"].ToString()
                    };

                    lista.Add(nuevoTipo);
                }

                return lista;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer la lista de iluminaciones");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool Update(FichaCuidados obj)
        {
            if (obj == null || !obj.Validar())
                return false;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sqlFicha = "UPDATE FichaCuidados SET FrecuenciaRiegoUnidadTiempo = @frecRiegoTi, " +
                                "FrecuenciaRiegoCantidad = @frecRiegoCant, Temperatura = @temp, IdIuminacion = @idIlum)" +
                                "WHERE IdPlanta = @id";

            SqlCommand cmdFicha = new SqlCommand(sqlFicha, cn);

            cmdFicha.Parameters.AddWithValue("@frecRiegoTi", obj.FrecuenciaRiegoUnidadTiempo);
            cmdFicha.Parameters.AddWithValue("@frecRiegoCant", obj.FrecuenciaRiegoCantidad);
            cmdFicha.Parameters.AddWithValue("@temp", obj.Temperatura);
            cmdFicha.Parameters.AddWithValue("@idIlum", obj.Iluminacion.Id);
            cmdFicha.Parameters.AddWithValue("@id", obj.Id);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmdFicha.ExecuteNonQuery();
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
    }
}