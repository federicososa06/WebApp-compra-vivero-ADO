using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using Dominio.ParametroConfiguracion;
using Repositorios;
using System;
using System.Collections.Generic;

namespace Gestion_vivero
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //PruebaParametros();

            PruebaPlantas();

            //PruebaCompra();

            //IRepositorioItem repoItem = new RepositorioItem();
            //repoItem.Remove(3);

            //PruebaTipoPlanta();
        }

        private static void PruebaTipoPlanta()
        {
            IRepositorioTipoPlanta repoTipoPlanta = new RepositorioTipoPlanta();

            TipoPlanta tp = new TipoPlanta()
            {
                Id = 5,
                Nombre = "tipo de planta",
                Descripcion = "asdljasdalskdjaslkdjaskldjakñsdjasdjaksjdlkasjdklasjdasdajsdlajscansc,"
            };

            tp.Nombre = "Tipo de Planta nombre nuevo";

            //ADD <- FUNCIONA
            //if(repoTipoPlanta.Add(tp))
            //    Console.WriteLine("se agregó") ;
            //else
            //    Console.WriteLine("no se agregó");

            //FINDALL <-FUNCIONA
            //IEnumerable<TipoPlanta> lista = repoTipoPlanta.FindAll();
            //foreach (var item in lista)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            //FINDBYID <- FUNCIONA
            //TipoPlanta tipo = repoTipoPlanta.FindById(5);
            //Console.WriteLine(tipo.ToString());

            //tipo.Nombre = "nombre nuevo";
            //UPDATE <- FUNCIONA
            //if (repoTipoPlanta.Update(tipo))
            //    Console.WriteLine("se modificó");
            //else
            //    Console.WriteLine("no se modificó");
        }

        private static void PruebaCompra()
        {
            IRepositorioCompra repoCompra = new RepositorioCompra();

            IEnumerable<Item> listaItem = new List<Item>()
            {
               new Item { Id = 4, Cantidad = 1, PlantaComprada = new Planta{ Id = 6}, PrecioUnitario = 40 },
               new Item { Id = 1, Cantidad = 5, PlantaComprada = new Planta{ Id = 1}, PrecioUnitario = 100 },
            };

            Compra c1 = new CompraPlaza()
            {
                Fecha = new DateTime(2011, 6, 10),
                ListaItems = listaItem,
                CostoFlete = 100
            };

            //FINDBYID <- FUNCIONA
            //IEnumerable<Compra> listaCompras = repoCompra.FindAll();
            //foreach (var item in listaCompras)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            //FINDBYID <- FUNCIONA
            // Console.WriteLine(repoCompra.FindById(1009).ToString());

            //ADD <- FUNCIONA
            // repoCompra.Add(c1);

            // REMOVE <- FUNCIONA
            //repoCompra.Remove(1008);
        }

        private static void PruebaPlantas()
        {
            IRepositorioPlanta repoPlanta = new RepositorioPlanta();

            TipoPlanta hierbas = new TipoPlanta()
            {
                Id = 3,
                Nombre = "Hierba",
                Descripcion = "Una hierba es una planta de tamaño pequeño que presenta un tallo tierno y no leñoso. "
            };

            ////BUSCARPORTIPO -> FUINCIONA
            //Console.WriteLine("BUSCAR POR TIPO ");
            //IEnumerable<Planta> listaPlantas1 = repoPlanta.BuscarPorTipo(hierbas);
            //foreach (var item in listaPlantas1)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.WriteLine("");

            ////BUSCARPORNOMBRE -> FUINCIONA
            //Console.WriteLine("BUSCAR POR NOMBRE");
            //IEnumerable<Planta> listaPlantas2 = repoPlanta.BuscarPorNombre("Solanum tuberosum");
            //foreach (var item in listaPlantas2)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.WriteLine("");

            ////BUSCARPORAMBIENTE -> FUINCIONA
            //Console.WriteLine("BUSCAR POR AMBIENTE");
            //IEnumerable<Planta> listaPlantas3 = repoPlanta.BuscarPorAmbiente(Ambiente.Mixta);
            //foreach (var item in listaPlantas3)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.WriteLine("");

            ////BUSCARPLANTASMASBAJAS->FUINCIONA
            //Console.WriteLine("BUSCAR POR PLANTAS BAJAS");
            //IEnumerable<Planta> listaPlantasAltas4 = repoPlanta.BuscarPlantasMasBajas(54);
            //foreach (var item in listaPlantasAltas4)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.WriteLine("");

            ////BUSCARPLANTASMASALTAS->FUINCIONA
            //Console.WriteLine("BUSCAR POR PLANTAS ALTAS");
            //IEnumerable<Planta> listaPlantasAltas5 = repoPlanta.BuscarPlantasMasAltas(15);
            //foreach (var item in listaPlantasAltas5)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.WriteLine(" ");

            //TipoIluminacion luzSolar = new TipoIluminacion() { Tipo = "Luz solar directa" };

            FichaCuidados fc = new FichaCuidados()
            {
                FrecuenciaRiegoUnidadTiempo = "Semanas",
                FrecuenciaRiegoCantidad = 2,
                Iluminacion = new TipoIluminacion() { Id = 1 },
                Temperatura = 3
            };
            List<NombreVulgar> listaNom = new List<NombreVulgar>
            {
                new NombreVulgar { Nombre = "Boniato" },
                new NombreVulgar { Nombre = "Batata" }
            };

            Planta planta1 = new Planta()
            {
                Tipo = new TipoPlanta { Id = 4 },
                NombreCientifico = "Ipomoea batatas 3",
                ListaNombreVulgares = listaNom,
                Descripcion = "Son unos tubérculos con una forma normalmente alargada y de distintos colores: desde el rojo claro, pasando por el amarillo oscuro, hasta la tonalidad más blanquecina",
                AlturaMax = 2,
                UrlFoto = "",
                Ambiente = Ambiente.Mixta,
                Cuidados = fc
            };

            //ADD-> FUNCIONA
            //if (repoPlanta.Add(planta1))
            //    Console.WriteLine("se agregó");

            //FINDALL-> FUNCIONA
            //IEnumerable<Planta> ListaPlantas = repoPlanta.FindAll();
            //foreach (var item in ListaPlantas)
            //{
            //    Console.WriteLine(item.ToString());
            //    foreach (var nom in item.ListaNombreVulgares)
            //    {
            //        Console.WriteLine(nom.ToString());
            //    }
            //}

            //FINDBYID -> FUNCIONA
            //Planta buscadaPorId = repoPlanta.FindById(13);
            //Console.WriteLine("buscada por id #13  --> " + buscadaPorId.ToString());
            //foreach (var item in buscadaPorId.ListaNombreVulgares)
            //{
            //    Console.WriteLine(item.Nombre);
            //}

            //REMOVE->FUNCIONA
            //repoPlanta.Remove(1022);

            //UPDATE -> FUNCIONA 

            Planta pl = repoPlanta.FindById(1028);
            //pl.Ambiente = Ambiente.Exterior;

            //if (repoPlanta.Update(pl))
            //    Console.WriteLine("se modificó");
            //else
            //    Console.WriteLine("no se modificó");

            Console.WriteLine(pl.Ambiente);

        }

        private static void PruebaParametros()
        {
            IRepositorioParametro repoParametro = new RepositorioParametro();

            Parametros parametro2 = new Parametros()
            {
                Id = 7,
                Nombre = "Tasa Importación",
                Valor = 21.21M
            };

            // ADD -> FUNCIONA
            //repoParametro.Add(parametro2);

            //REMOVE -> FUNCIONA
            //repoParametro.Remove(9);

            //UPDATE -> FUNCIONA
            // parametro2.Nombre = "Tasa Aranceles America del Sur";
            //repoParametro.Update(parametro2);

            // GETVALORPARAMETRO -> FUNCIONA
            //string res = repoParametro.GetValorParametro("Tasa Aranceles America del Sur");
            //Console.WriteLine("Valor del parametro:" + res);

            // FINDBYID -> FUNCIONA
            //Parametros para = repoParametro.FindById(7);
            //Console.WriteLine(para.ToString());

            // FINDALL -> FUNCIONA
            //IEnumerable<Parametros> listaParametros = repoParametro.FindAll();
            //foreach (var item in listaParametros)
            //    Console.WriteLine(item.ToString());

            //SETNUEVOVALOR -> FUNCIONA
            //repoParametro.SetValorParametro("TasaImportacion", 23);
        }
    }
}