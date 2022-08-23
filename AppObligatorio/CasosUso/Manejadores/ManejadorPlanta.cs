using CasosUso.InterfacesManejadores;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorios;
using System;
using System.Collections.Generic;

namespace CasosUso.Manejadores
{
    public class ManejadorPlanta : IManejadorPlanta
    {
        public IRepositorioPlanta RepoPlanta { get; set; }
        public IRepositorioFichaCuidados RepoFicha { get; set; }

        public IRepositorioTipoPlanta RepoTipo { get; set; }

        public IEnumerable<NombreVulgar> ListaNombres { get; set; }
        public string listnom { get; set; }

        public ManejadorPlanta(IRepositorioPlanta repoPlan, IRepositorioFichaCuidados repoFicha,
                            IRepositorioTipoPlanta repoTipo)
        {
            RepoPlanta = repoPlan;
            RepoFicha = repoFicha;
            RepoTipo = repoTipo;
        }

        public bool AgregarPlanta(Planta planta, int idTipo, int idIluminacion, string listaNom)
        {
            bool ret = false;

            //
            //Validar que no exista nombre cientifico
            if (planta == null || !planta.Validar() || planta.UrlFoto == null ||
               RepoPlanta.BuscarPlantaPorNombreCientifico(planta.NombreCientifico) != null)
                return ret;

            //Validar que sea jpg o png
            if (planta.UrlFoto.EndsWith(".jpg") || planta.UrlFoto.EndsWith(".png"))
            {
                TipoIluminacion tipoIluminacion = RepoFicha.TraerIluminacionPorId(idIluminacion);
                if (tipoIluminacion != null)
                {
                    TipoPlanta tipoPlanta = RepoTipo.FindById(idTipo);
                    if (tipoPlanta != null)
                    {
                        planta.Cuidados.Iluminacion = tipoIluminacion;
                        planta.Tipo = tipoPlanta;
                        planta.ListaNombreVulgares = (List<NombreVulgar>)ConvertirListaNombres(listaNom);
                        if (RepoPlanta.Add(planta))
                            ret = true;
                    }
                }
                return ret;
            }

            return ret;
        }

        private IEnumerable<NombreVulgar> ConvertirListaNombres(string lisnom)
        {
            List<NombreVulgar> listaRetorno = new List<NombreVulgar>();

            if (string.IsNullOrEmpty(lisnom))
                return null;

            string separador = ",";
            string[] nombres = lisnom.Split(separador, StringSplitOptions.RemoveEmptyEntries);

            foreach (var nom in nombres)
            {
                NombreVulgar nuevoNom = new NombreVulgar()
                {
                    Nombre = nom
                };

                listaRetorno.Add(nuevoNom);
            }

            return listaRetorno;
        }

        public bool ActualizarPlanta(Planta planta)
        {
            return RepoPlanta.Update(planta);
        }

        public Planta BuscarPlantaPorId(object id)
        {
            return RepoPlanta.FindById(id);
        }

        public bool EliminarPlanta(object id)
        {
            return RepoPlanta.Remove(id);
        }

        public IEnumerable<Planta> TraerTodasLasPLantas()
        {
            return RepoPlanta.FindAll();
        }

        public IEnumerable<TipoPlanta> TraerTodosLosTipos()
        {
            return RepoTipo.FindAll();
        }

        public IEnumerable<TipoIluminacion> TraerTodosLasIluminaciones()
        {
            return RepoFicha.TraerTipoListaIluminaciones();
        }
    }
}