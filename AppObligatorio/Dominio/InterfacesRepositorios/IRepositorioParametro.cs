using Dominio.ParametroConfiguracion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorios
{
    public interface IRepositorioParametro:IRepositorio<Parametros>
    {
        public string GetValorParametro(string nom);

        public bool SetValorParametro(string nom, decimal val);
    }
}
