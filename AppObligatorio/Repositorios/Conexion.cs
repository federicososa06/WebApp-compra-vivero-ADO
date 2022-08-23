using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace Repositorios
{
    class Conexion
    {
        //private string cadenaConexion = @"SERVER=(localdb)\MsSqlLocalDb;
        //                                  DATABASE = ObligatorioP3_01;
        //                                  INTEGRATED SECURITY = TRUE ";

        public static string ObtenerCadenaConexion()
        {
            string cadenaConexion = "";
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
            cadenaConexion = config.GetConnectionString("miConexion");
            return cadenaConexion;
        }
        // 'C:\Users\feder\Desktop\Obligatorio\Gestion_vivero\bin\Debug\netcoreapp3.1\appSettings.json'.'
        public SqlConnection CrearConexion()
        {
            string cadenaConexion = ObtenerCadenaConexion();
            return new SqlConnection(cadenaConexion);
        }


        public bool AbrirConexion(SqlConnection cn)
        {
            try
            {
                if (cn == null)
                    return false;

                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    return true;
                }

                else return false;
            }
            catch (Exception e) 
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                Debug.WriteLine("Entré al finally de abrir conexión");
            }
        }

        public bool CerrarConexion(SqlConnection cn)
        {
            try
            {
                if(cn == null)
                    return false;

                if(cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    cn.Dispose();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
