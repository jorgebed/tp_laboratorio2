using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Llamada
    {
        public enum TipoLlamada { Local, Provincial, Todas };
        
        protected float duracion;
        protected string nroDestino;
        protected string nroOrigen;

        #region CONSTRUCTORES
        public Llamada(float duracion, string nroDestino, string nroOrigen)
        {
            this.duracion = duracion;
            this.nroDestino = nroDestino;
            this.nroOrigen = nroOrigen;
        }
        #endregion

        #region PROPIEDADES
        public abstract float CostoLlamada { get; }

        public float Duracion { get { return this.duracion; } }

        public string NroDestino { get { return this.nroDestino; } }

        public string NroOrigen { get { return this.nroOrigen; } }
        #endregion

        #region METODOS
        /// <summary>
        /// Es un método de instancia. Utiliza StringBuilder.
        /// </summary>
        /// <returns></returns>
        protected virtual string Mostrar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("NUMERO ORIGEN: " + this.NroOrigen);
            sb.AppendLine("NUMERO DESTINO: " + this.NroDestino);
            sb.AppendLine("DURACION: " + this.Duracion + " segundos.");

            return sb.ToString();
        }

        /// <summary>
        /// Es un método de clase que recibirá dos Llamadas. Se utilizará para ordenar una lista de llamadas de forma ascendente.
        /// </summary>
        /// <param name="llamada1"></param>
        /// <param name="llamada2"></param>
        /// <returns></returns>
        public static int OrdenarPorDuracion(Llamada llamada1, Llamada llamada2)
        {
            if (llamada1.Duracion > llamada2.Duracion)
                return 1;
            else if (llamada1.Duracion < llamada2.Duracion)
                return -1;
            return 0;
        }
        #endregion

        #region SOBRECARGAS
        /// <summary>
        /// Dos llamadas son iguales si su origen y destino son los mismos.
        /// </summary>
        /// <param name="llamada1"></param>
        /// <param name="llamada2"></param>
        /// <returns></returns>
        public static bool operator ==(Llamada llamada1, Llamada llamada2)
        {
            return llamada1.Equals(llamada2) && llamada1.NroOrigen == llamada2.NroOrigen && llamada1.NroDestino == llamada2.NroDestino;
        }

        public static bool operator !=(Llamada llamada1, Llamada llamada2)
        {
            return !(llamada1 == llamada2);
        }
        #endregion
    }
}
