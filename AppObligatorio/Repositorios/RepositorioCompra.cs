using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Repositorios
{
    public class RepositorioCompra : IRepositorioCompra
    {
        private Conexion ManejadorConexion = new Conexion();

        string TablasJoineadas = "SELECT * FROM Compras AS c " +
                           "LEFT JOIN ComprasImportadas AS ci ON ci.IdComImp = c.IdCompra " +
                           "LEFT JOIN ComprasPlazas AS cp ON cp.IdComPlaza = c.IdCompra";

        public bool Add(Compra obj) 
        {
            if (obj == null || !obj.Validar())
                return false;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            SqlTransaction trn = null;

            //Insertar en la tablas de Compras
            string sqlCompras = "INSERT INTO Compras VALUES (@fecha) " +
                                "SELECT CAST(SCOPE_IDENTITY() AS INT)";
            SqlCommand cmdCompras = new SqlCommand(sqlCompras, cn);

            cmdCompras.Parameters.AddWithValue("@fecha", obj.Fecha);

            //Insertar en a tabla importada o plaza
            string sqlImportado = "INSERT INTO ComprasImportadas VALUES (@id, @america, @medidas)";
            string sqlPlaza = "INSERT INTO ComprasPlazas VALUES (@id, @costoFlete)";

            //Insertar en la tablas de Items
            string sqlItem = "INSERT INTO Items VALUES(@cant, @precioUni, @idPlanta, @idCompra) " +
                             "SELECT CAST(SCOPE_IDENTITY() AS INT)";
            SqlCommand cmdItem = new SqlCommand(sqlItem, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                trn = cn.BeginTransaction();
                cmdCompras.Transaction = trn;
                cmdItem.Transaction = trn;

                int idCompra = (int)cmdCompras.ExecuteScalar();
                obj.Id = idCompra; //se asigna el id generado al objeto

                foreach (var item in obj.ListaItems)
                {
                    cmdItem.Parameters.AddWithValue("@cant", item.Cantidad);
                    cmdItem.Parameters.AddWithValue("@precioUni", item.PrecioUnitario);
                    cmdItem.Parameters.AddWithValue("@idPlanta", item.PlantaComprada.Id);
                    cmdItem.Parameters.AddWithValue("@idCompra", idCompra);

                    int idItem = (int)cmdItem.ExecuteScalar();
                    cmdItem.Parameters.Clear();
                }

                if (obj.Tipo() == "Importada")
                {
                    CompraImportada compra = obj as CompraImportada; //se transforma el obj Compra que recibe como parametro en CompraImportada
                    cmdCompras.CommandText = sqlImportado; //se cambia el commandText para el de sqlImportado

                    cmdCompras.Parameters.AddWithValue("@id", idCompra);
                    cmdCompras.Parameters.AddWithValue("@america", compra.AmericaDelSur);
                    cmdCompras.Parameters.AddWithValue("@medidas", compra.MedidasSanitarias);

                    cmdCompras.ExecuteNonQuery();
                }
                else if (obj.Tipo() == "Plaza")
                {
                    CompraPlaza compra = obj as CompraPlaza; //se transforma el obj Compra que recibe como parametro en CompraPlaza
                    cmdCompras.CommandText = sqlPlaza; //se cambia el commandText para el de sqlPlaza

                    cmdCompras.Parameters.AddWithValue("@id", idCompra);
                    cmdCompras.Parameters.AddWithValue("@costoFlete", compra.CostoFlete);

                    cmdCompras.ExecuteNonQuery();
                }

                // cmdCompras.Parameters.Clear();

                trn.Commit();
                return true;
            }
            catch (Exception e)
            {
                trn.Rollback();
                Debug.WriteLine("No se pudo realizar la compra");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public IEnumerable<Compra> FindAll()
        {
            List<Compra> listaCompra = new List<Compra>();

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = TablasJoineadas;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Compra com = CrearCompra(dr);
                    listaCompra.Add(com);
                }

                return listaCompra;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer todas las compras");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public Compra FindById(object id)
        {
            Compra buscado = null;

            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = TablasJoineadas;

            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    buscado = CrearCompra(dr);
                }

                return buscado;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo obtener la compra");
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
            string sql = "DELETE FROM Compras WHERE IdCompra = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            //Borrar tambien de la lista de items. Si se borra una compra se deberia borrar tambien los items
            List<int> listaItems = ObtenerIdItemSegunIdCompra((int)id);
            IRepositorioItem repoItem = new RepositorioItem();
            foreach (var item in listaItems)
            {
                repoItem.Remove(item);
            }

            //Borrar de la tabla de compra importada o de plaza 
            RemoveCompraImportada((int)id);
            RemoveCompraPlaza((int)id);
            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo borrar una compra");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        public bool Update(Compra obj)
        {
            throw new NotImplementedException();
        }

        // -- MÉTODOS AUXILIARES --
        private Compra CrearCompra(SqlDataReader reader)
        {
            Compra compra = null;

            if (reader["AmericaDelSur"] is DBNull) //si en la tabla la fila America del Sur es null entonces es COMPRA DE PLAZA
            {
                compra = new CompraPlaza()
                {
                    Id = (int)reader["IdComPlaza"],
                    CostoFlete = (decimal)reader["CostoFlete"]
                };
            }
            else // COMPRA IMPORTADA
            {
                compra = new CompraImportada()
                {
                    Id = (int)reader["IdComImp"],
                    AmericaDelSur = (bool)reader["AmericaDelSur"],
                    MedidasSanitarias = reader["MedidasSanitarias"].ToString()
                };
            }

            compra.Id = (int)reader["IdCompra"];
            compra.Fecha = (DateTime)reader["Fecha"];
            //compra.ListaItems = TraerTodosLosItem((int)reader["IdItems"]);

            return compra;
        }


        // MÉTODO SIN USO, LA TABLA COMPRAS NO TIENE IdItem
        private IEnumerable<Item> TraerTodosLosItem(int id)
        {
            List<Item> listaItems = new List<Item>();
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT * FROM Items WHERE Items.IdItem = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            //repoPlanta para traer la planta que tiene el item
            IRepositorioPlanta repoPlanta = new RepositorioPlanta();

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Item nuevoItem = new Item()
                    {
                        Id = (int)dr["IdItem"],
                        Cantidad = (int)dr["Cantidad"],
                        PrecioUnitario = (decimal)dr["PrecioUnitario"],
                        PlantaComprada = repoPlanta.FindById((int)dr["IdPlanta"])
                    };
                    listaItems.Add(nuevoItem);
                }

                return listaItems;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo traer la lista de items");
                return null;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        //Buscar los id de los items por el id de la compra
        private List<int> ObtenerIdItemSegunIdCompra(int id)
        {
            List<int> listaId = new List<int>();
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "SELECT IdItem FROM Items WHERE IdCompra = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int numEncontrado = (int)dr.GetInt32(0);
                    listaId.Add(numEncontrado);
                }
                return listaId;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se econtró ninguna compra");
                return listaId;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        //Borrar de la tabla de compra importada 
        private bool RemoveCompraImportada(int id)
        {
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "DELETE FROM ComprasImportadas WHERE IdComImp = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo borrar");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }

        //Borrar de la tabla de compra plaza
        private bool RemoveCompraPlaza(int id)
        {
            SqlConnection cn = ManejadorConexion.CrearConexion();
            string sql = "DELETE FROM ComprasPlazas WHERE IdComPlaza = " + id;
            SqlCommand cmd = new SqlCommand(sql, cn);

            try
            {
                ManejadorConexion.AbrirConexion(cn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("No se pudo borrar");
                return false;
            }
            finally
            {
                ManejadorConexion.CerrarConexion(cn);
            }
        }
    }
}