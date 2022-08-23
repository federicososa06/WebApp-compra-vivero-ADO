using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Repositorios
{
    public class RepositorioPlanta : IRepositorioPlanta
    {
        private Conexion ManejadorConexion = new Conexion();

        private string SelectTablasJoineadas = "SELECT * FROM Plantas AS p " +
                                                "LEFT JOIN TipoPlantas AS tp ON p.IdTipoPlanta = tp.IdTipoPlanta " +
                                                "LEFT JOIN FichaCuidados AS fc ON p.IdPlanta = fc.IdPlanta " +
                                                "LEFT JOIN TipoIluminacion AS ti ON fc.IdIluminacion = ti.IdTipoIluminacion ";
                                                //"LEFT JOIN ListaNombresVulgares AS lnv ON p.IdPlanta = lnv.IdPlanta";

        // --- CRUD ---
        public bool Add(Planta obj)
        {
            // BuscarPlantaPorNombreCientifico(obj.NombreCientifico) != null -> ya existe una planta con ese nombre cientifico
            if (obj == null || !obj.Validar() || BuscarPlantaPorNombreCientifico(obj.NombreCientifico) != null)
                return false;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            SqlTransaction trn = null;

            // Comprobar parametros descripción, se accede desde el repoParametro
            decimal topeMax = ObtenerTopeDescMax("PlantaTopeDescrMax");
            decimal topeMin = ObtenerTopeDescMin("PlantaTopeDescrMin");

            if (obj.ValidarParametrosDescripcion(topeMin, topeMax))
            {
                // Tabla Plantas
                string sql = "INSERT INTO Plantas VALUES (@idTP, @nomCient, @alt, @urlFoto, @ambiente, @desc) " +
                              "SELECT CAST(SCOPE_IDENTITY() AS INT)";
                SqlCommand cmdPlantas = new SqlCommand(sql, cn);

                cmdPlantas.Parameters.AddWithValue("@idTP", obj.Tipo.Id);
                cmdPlantas.Parameters.AddWithValue("@nomCient", obj.NombreCientifico);
                cmdPlantas.Parameters.AddWithValue("@alt", obj.AlturaMax);
                cmdPlantas.Parameters.AddWithValue("@urlFoto", obj.UrlFoto);
                cmdPlantas.Parameters.AddWithValue("@ambiente", obj.Ambiente.ToString());
                cmdPlantas.Parameters.AddWithValue("@desc", obj.Descripcion);

                //Tabla FichaCuidados
                string sqlFicha = "INSERT INTO FichaCuidados VALUES (@idPlanta, @frecRiegoTi, @frecRiegoCant, @temp, @idIlum) ";
                SqlCommand cmdFicha = new SqlCommand(sqlFicha, cn);

                cmdFicha.Parameters.AddWithValue("@frecRiegoTi", obj.Cuidados.FrecuenciaRiegoUnidadTiempo);
                cmdFicha.Parameters.AddWithValue("@frecRiegoCant", obj.Cuidados.FrecuenciaRiegoCantidad);
                cmdFicha.Parameters.AddWithValue("@idIlum", obj.Cuidados.Iluminacion.Id);
                cmdFicha.Parameters.AddWithValue("@temp", obj.Cuidados.Temperatura);

                //Tabla ListaNombresVulgares
                string sqlListaNombres = "INSERT INTO ListaNombresVulgares VALUES(@nomVulg, @idPlanta)";
                SqlCommand cmdListaNom = new SqlCommand(sqlListaNombres, cn);

                try
                {
                    ManejadorConexion.AbrirConexion(cn);
                    trn = cn.BeginTransaction();

                    cmdPlantas.Transaction = trn;
                    cmdFicha.Transaction = trn;
                    cmdListaNom.Transaction = trn;

                    ////id generados para las tablas que tienen foreign key
                    int idPlanta = (int)cmdPlantas.ExecuteScalar();
                    obj.Cuidados.Id = idPlanta;
                    obj.Id = idPlanta;

                    cmdFicha.Parameters.AddWithValue("@idPlanta", idPlanta);
                    cmdFicha.ExecuteNonQuery();

                    // Tabla ListaNombresVulgares
                    if (obj.ListaNombreVulgares != null) // PARA PRUEBAS
                    {
                        foreach (NombreVulgar nom in obj.ListaNombreVulgares)
                        {
                            cmdListaNom.Parameters.AddWithValue("@nomVulg", nom.Nombre);
                            cmdListaNom.Parameters.AddWithValue("@idPlanta", idPlanta);

                            cmdListaNom.ExecuteNonQuery();
                            cmdListaNom.Parameters.Clear();
                        }
                    }


                    trn.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("No se pudo agregar una nueva planta");
                    trn.Rollback();
                    return false;
                }
                finally
                {
                    ManejadorConexion.CerrarConexion(cn);
                }
            }
            return false;
        }

        public IEnumerable<Planta> FindAll()
        {
            List<Planta> listaPlantas = new List<Planta>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            //string sql = "SELECT * FROM Plantas AS p " +
            //            "LEFT JOIN TipoPlantas AS tp ON p.IdTipoPlanta = tp.IdTipoPlanta " +
            //            "LEFT JOIN FichaCuidados AS fc ON p.IdPlanta = fc.IdPlanta " +
            //            "LEFT JOIN TipoIluminacion AS ti ON fc.IdIluminacion = ti.IdTipoIluminacion ";
           //              "LEFT JOIN ListaNombresVulgares AS lnv ON p.IdPlanta = lnv.IdPlanta";

            string sql = SelectTablasJoineadas;

            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Planta p = CrearPlanta(dr);
                    p.ListaNombreVulgares = TraerListaNombresVulgares(p.Id);
                    p.Tipo = CrearTipoPlanta(dr);
                    p.Cuidados = CrearFichaCuidados(dr);

                    listaPlantas.Add(p);
                }

                return listaPlantas;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo listar las plantas");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public Planta FindById(object id)
        {
            Planta plantaBuscada = null;
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = SelectTablasJoineadas + " WHERE p.IdPlanta = " + id;

            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    plantaBuscada = CrearPlanta(dr);
                    plantaBuscada.ListaNombreVulgares = TraerListaNombresVulgares(plantaBuscada.Id);
                    plantaBuscada.Tipo = CrearTipoPlanta(dr);
                    plantaBuscada.Cuidados = CrearFichaCuidados(dr);
                }

                return plantaBuscada;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo encontrar la planta");
                return plantaBuscada;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool Remove(object id)
        {
            bool ret = false;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "DELETE FROM Plantas WHERE IdPlanta = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            IRepositorioFichaCuidados repoFicha = new RepositorioFichaCuidados();
            repoFicha.Remove(id);

            IRepositorioListaNombresVulgares repoLNV = new RepositorioListaNombresVulgares();
            repoLNV.Remove(id);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                ret = true;
                return ret;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo borrar la planta");
                return ret;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool Update(Planta obj)
        {
            if (obj == null || !obj.Validar()) 
                return false;

            SqlConnection cn = ManejadorConexion.CrearConexion();

            string sql = "UPDATE Plantas SET  IdTipoPlanta = @idTP, " +
                            "NombreCientifico = @nomCien, AlturaMax = @alt, UrlFoto = @url, " +
                            "Ambiente = @amb, DescripcionPlanta = @descr " +
                            "WHERE IdPlanta = @id ";

            SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@idTP", obj.Tipo.Id);
            cmd.Parameters.AddWithValue("@nomCien", obj.NombreCientifico);
            cmd.Parameters.AddWithValue("@alt", obj.AlturaMax);
            cmd.Parameters.AddWithValue("@url", obj.UrlFoto);
            cmd.Parameters.AddWithValue("@amb", obj.Ambiente);
            cmd.Parameters.AddWithValue("@descr", obj.Descripcion);
            cmd.Parameters.AddWithValue("@id", obj.Id);

            //Tabla ListaNombresVulgares
            IRepositorioListaNombresVulgares repoLNV = new RepositorioListaNombresVulgares();
            foreach (var nom in obj.ListaNombreVulgares)
            {
                repoLNV.Update(nom);
            }

            // Comprobar parametros descripción, se accede desde el repoParametro:
            decimal topeMin = ObtenerTopeDescMin("PlantaTopeDescrMin");
            decimal topeMax = ObtenerTopeDescMax("PlantaTopeDescrMax");

            if (obj.ValidarParametrosDescripcion(topeMin, topeMax))
            {
                try
                {
                    ManejadorConexion.AbrirConexion(cn);

                    cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("No se pudo actualizar la planta");
                    return false;
                }
                finally
                {
                    ManejadorConexion.CerrarConexion(cn);
                }
            }

            return false;
        }

        // --- MÉTODOS ESPECÍFICOS ---
        public IEnumerable<Planta> BuscarPlantasMasAltas(int altura)
        {
            List<Planta> listaPlantas = null;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = SelectTablasJoineadas + " WHERE p.AlturaMax >= " + altura;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    listaPlantas = new List<Planta>();
                    Planta p = CrearPlanta(dr);
                    p.ListaNombreVulgares = TraerListaNombresVulgares(p.Id);
                    p.Tipo = CrearTipoPlanta(dr);
                    p.Cuidados = CrearFichaCuidados(dr);

                    listaPlantas.Add(p);
                }

                return listaPlantas;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo encontrar las plantas buscadas");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public IEnumerable<Planta> BuscarPlantasMasBajas(int altura)
        {
            List<Planta> listaPlantas = new List<Planta>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = SelectTablasJoineadas + " WHERE p.AlturaMax <= " + altura;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Planta p = CrearPlanta(dr);
                    p.ListaNombreVulgares = TraerListaNombresVulgares(p.Id);
                    p.Tipo = CrearTipoPlanta(dr);
                    p.Cuidados = CrearFichaCuidados(dr);

                    listaPlantas.Add(p);
                }

                return listaPlantas;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo listar las plantas");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public IEnumerable<Planta> BuscarPorAmbiente(Ambiente amb)
        {
            List<Planta> listaPlantas = new List<Planta>();
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = SelectTablasJoineadas + " WHERE p.Ambiente = @ambiente";

            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@ambiente", amb.ToString());

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Planta p = CrearPlanta(dr);
                    p.ListaNombreVulgares = TraerListaNombresVulgares(p.Id);
                    p.Tipo = CrearTipoPlanta(dr);
                    p.Cuidados = CrearFichaCuidados(dr);

                    listaPlantas.Add(p);
                }

                return listaPlantas;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer las plantas según su ambiente");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public IEnumerable<Planta> BuscarPorNombre(string nom)
        {
            List<Planta> listaPlantas = new List<Planta>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = SelectTablasJoineadas + " WHERE p.NombreCientifico = @nomCien OR lnv.NombreVulg = @nomCien";

            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nomCien", nom);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Planta p = CrearPlanta(dr);
                    p.ListaNombreVulgares = TraerListaNombresVulgares(p.Id);
                    p.Tipo = CrearTipoPlanta(dr);
                    p.Cuidados = CrearFichaCuidados(dr);

                    listaPlantas.Add(p);
                }

                return listaPlantas;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer las plantas según su nombre");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public IEnumerable<Planta> BuscarPorTipo(int idTipo)
        {
            List<Planta> listaPlantas = new List<Planta>();
            SqlConnection cn = ManejadorConexion.CrearConexion();

            string sql = SelectTablasJoineadas + " WHERE p.IdTipoPlanta  in (SELECT Plantas.IdTipoPlanta FROM TipoPlantas " +
                                                   "JOIN Plantas ON TipoPlantas.IdTipoPlanta = Plantas.IdTipoPlanta " +
                                                   "WHERE TipoPlantas.IdTipoPlanta = @tipoPlanta)";
            
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@tipoPlanta", idTipo);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Planta p = CrearPlanta(dr);
                    p.ListaNombreVulgares = TraerListaNombresVulgares(p.Id);
                    p.Tipo = CrearTipoPlanta(dr);
                    p.Cuidados = CrearFichaCuidados(dr);

                    listaPlantas.Add(p);
                }

                return listaPlantas;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo encontrar ninguna planta");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public Planta BuscarPlantaPorNombreCientifico(string nom)
        {
            Planta buscada = new Planta();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = SelectTablasJoineadas + " WHERE p.NombreCientifico = @nomCien";

            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nomCien", nom);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    buscada = CrearPlanta(dr);
                    buscada.ListaNombreVulgares = TraerListaNombresVulgares(buscada.Id);
                    buscada.Tipo = CrearTipoPlanta(dr);
                    buscada.Cuidados = CrearFichaCuidados(dr);
                    return buscada;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer las plantas según su nombre");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }


        // --- MÉTODOS AUXILIARES ---

        //Parseo del ambiente que se lee desde el sql
        private Ambiente ObtenerAmbiente(SqlDataReader reader, string paramTabla)
        {
            Ambiente ams = (Ambiente)Enum.Parse(typeof(Ambiente), reader[paramTabla].ToString());
            return ams;
        }

        private Planta CrearPlanta(SqlDataReader reader)
        {
            Planta nuevaPlanta = new Planta()
            {
                Id = (int)reader["IdPlanta"],
                NombreCientifico = reader["NombreCientifico"].ToString(),
                AlturaMax = (int)reader["AlturaMax"],
                UrlFoto = reader["UrlFoto"].ToString(),
                Descripcion = reader["DescripcionPlanta"].ToString(),
                Ambiente = ObtenerAmbiente(reader, "Ambiente"),

            };

            return nuevaPlanta;
        }

        // toma los valores de la tabla joineada donde están todos los datos
        private TipoPlanta CrearTipoPlanta(SqlDataReader reader)
        {
            TipoPlanta ret = new TipoPlanta()
            {
                Id = (int)reader["IdTipoPlanta"],
                Nombre = reader["NombreTipoPlanta"].ToString(),
                Descripcion = reader["DescripcionTipoPlanta"].ToString(),
            };
            return ret;
        }

        // toma los valores de la tabla joineada donde están todos los datos
        private FichaCuidados CrearFichaCuidados(SqlDataReader reader)
        {
            FichaCuidados ret = new FichaCuidados()
            {
                Id = (int)reader["IdPlanta"],
                FrecuenciaRiegoUnidadTiempo = reader["FrecuenciaRiegoUnidadTiempo"].ToString(),
                FrecuenciaRiegoCantidad = (int)reader["FrecuenciaRiegoCantidad"],
                Iluminacion = CrearTipoIluminacion(reader),
                Temperatura = (decimal)reader["Temperatura"],
                IdPlanta = (int)reader["IdPlanta"]
            };
            return ret;
        }

        // toma los valores de la tabla joineada donde están todos los datos
        private TipoIluminacion CrearTipoIluminacion(SqlDataReader reader)
        {
            TipoIluminacion ret = new TipoIluminacion()
            {
                Id = (int)reader["IdTipoIluminacion"],
                Tipo = reader["Tipo"].ToString()
            };
            return ret;
        }

        //Es una tabla independiente porque Planta puede tener varios nombres vulgares
        private List<NombreVulgar> TraerListaNombresVulgares(int id)
        {
            List<NombreVulgar> listaNombres = new List<NombreVulgar>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM ListaNombresVulgares WHERE IdPlanta = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    NombreVulgar nuevoNom = new NombreVulgar()
                    {
                        Id = (int)dr["IdNomVulg"],
                        Nombre = dr["NombreVulg"].ToString(),
                        IdPlanta = (int)dr["IdPlanta"]
                    };

                    listaNombres.Add(nuevoNom);
                }
                return listaNombres;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer la lista de nombres");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

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