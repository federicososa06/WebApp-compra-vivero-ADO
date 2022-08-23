using Dominio.OtrasInterfaces;

namespace Dominio.ParametroConfiguracion
{
    public class Parametros:IValidable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }

        public bool Validar()
        {
            return !string.IsNullOrEmpty(Nombre);
        }

        public override string ToString()
        {
            return ($"{this.Id} - {this.Nombre} - {this.Valor}"); 
        }
    }
}