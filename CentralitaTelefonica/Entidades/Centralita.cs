using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Centralita
    {
        private List<Llamada> listaDeLlamadas;
        protected string razonSocial;

        #region CONSTRUCTORES
        /// <summary>
        /// La lista sólo se inicializará en el constructor por defecto Centralita().
        /// </summary>
        public Centralita()
        {
            this.listaDeLlamadas = new List<Llamada>();
            try
            {
                CentralitaDAO ListaRestaurada = new CentralitaDAO();
                listaDeLlamadas = ListaRestaurada.Leer("ListaLlamados");
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            
        }

        public Centralita(string nombreEmpresa)
            : this()
        {
            this.razonSocial = nombreEmpresa;
        }
        #endregion

        #region PROPIEDADES
        /// <summary>
        /// Retornarán el precio de lo facturado según el criterio. Dichos valores se calcularán en el método CalcularGanancia().
        /// </summary>
        public float GananciasPorLocal { get { return this.CalcularGanancia(Llamada.TipoLlamada.Local); } }

        public string RazonSocial 
        {
            get { return this.razonSocial; }
            set { this.razonSocial = value; }
        }

        /// <summary>
        /// Retornarán el precio de lo facturado según el criterio. Dichos valores se calcularán en el método CalcularGanancia().
        /// </summary>
        public float GananciasPorProvincial { get { return this.CalcularGanancia(Llamada.TipoLlamada.Provincial); } }

        /// <summary>
        /// Retornarán el precio de lo facturado según el criterio. Dichos valores se calcularán en el método CalcularGanancia().
        /// </summary>
        public float GananciasPorTotal { get { return CalcularGanancia(Llamada.TipoLlamada.Todas); } }

        /// <summary>
        /// Retorna La lista de llamadas.
        /// </summary>
        public List<Llamada> Llamadas { get { return this.listaDeLlamadas; } }
        #endregion

        #region METODOS
        /// <summary>
        /// Este método recibe un Enumerado TipoLlamada y retornará el valor de lo recaudado, según el criterio elegido (ganancias por las llamadas del tipo Local, Provincial o de Todas según corresponda).
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        private float CalcularGanancia(Llamada.TipoLlamada tipo)
        {
            float aux = 0;
            foreach (Llamada item in this.listaDeLlamadas)
            {
                switch (tipo)
                {
                    case Llamada.TipoLlamada.Local:
                        if (item is Local)
                            aux += ((Local)item).CostoLlamada;
                        break;
                    case Llamada.TipoLlamada.Provincial:
                        if (item is Provincial)
                            aux += ((Provincial)item).CostoLlamada;
                        break;
                    case Llamada.TipoLlamada.Todas:
                        if (item is Local)
                            aux += ((Local)item).CostoLlamada;
                        else if (item is Provincial)
                            aux += ((Provincial)item).CostoLlamada;
                        break;
                    default:
                        break;
                }
            }
            return aux;
        }
        
        public void OrdenarLlamadas()
        {
            this.listaDeLlamadas.Sort(Llamada.OrdenarPorDuracion);
        }

        private void AgregarLlamada(Llamada nuevaLlamada)
        {
            this.listaDeLlamadas.Add(nuevaLlamada);
        }

        /// <summary>
        /// expondrá la razón social, la ganancia total, ganancia por llamados locales y provinciales y el detalle de las llamadas realizadas.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Title(this.RazonSocial.ToUpper()));
            sb.AppendLine(this.Body("GANANCIA LOCAL:", this.GananciasPorLocal.ToString()));
            sb.AppendLine(this.Line('-'));
            sb.AppendLine(this.Body("GANANCIA PROVINCIAL:", this.GananciasPorProvincial.ToString()));
            sb.AppendLine(this.Line('-'));
            sb.AppendLine(this.Body("GANANCIA TODAS:", this.GananciasPorTotal.ToString()));
            sb.AppendLine(this.Line('-'));
            
            sb.AppendLine(this.Title("LISTA DE LLAMADOS"));
            
            foreach (Llamada item in this.listaDeLlamadas)
            {
                sb.Append(item.ToString());
                sb.AppendLine("---------------------------------------");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Arma un encabezado para un menú con ancho específico.
        /// </summary>
        /// <param name="titulo"></param>
        /// <returns></returns>
        public string Title(string titulo)
        {
            int capacidad = 39;
            int espacio = ((capacidad - titulo.Length) / 2);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < capacidad; i++)
            {
                sb.Append("-");
            }
            sb.AppendLine("");
            for (int i = 0; i < espacio; i++)
            {
                sb.Append(" ");
            }
            sb.Append(titulo);
            sb.AppendLine();
            for (int i = 0; i < capacidad; i++)
            {
                sb.Append("-");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Arma un cuerpo simil facturación con una columna para poner los montos alineados a derecha.
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public string Body(string texto, string valor)
        {
            string formatoPesos = String.Format("{0:C}", decimal.Parse(valor));
            int capacidad = 39;
            int interno = 29;
            int espacio = ((interno - texto.Length));
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}", texto);
            for (int i = 0; i < espacio; i++)
            {
                sb.Append(" ");
            }
            sb.Append("|");
            for (int i = 0; i < 9 - formatoPesos.Length; i++)
            {
                sb.Append(" ");
            }
            sb.AppendFormat(formatoPesos);

            return sb.ToString();
        }

        /// <summary>
        /// Arma una línea divisoria con el caracter que se envía por parámetro.
        /// </summary>
        /// <param name="caracter"></param>
        /// <returns></returns>
        public string Line(char caracter)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 29; i++)
            {
                sb.Append(caracter);
            }
            sb.Append("|");
            for (int i = 0; i < 9; i++)
            {
                sb.Append(caracter);
            }
            
            return sb.ToString();
        }
        #endregion

        #region SOBRECARGAS
        public static bool operator ==(Centralita c, Llamada llamada)
        {
            foreach (Llamada item in c.listaDeLlamadas)
            {
                if (item == llamada)
                    return true;
            }
            return false;
        }

        public static bool operator !=(Centralita c, Llamada llamada)
        {
            return !(c == llamada);
        }

        public static Centralita operator +(Centralita c, Llamada nuevaLlamada)
        {
            if (c != nuevaLlamada)
            {
                c.AgregarLlamada(nuevaLlamada);               
                return c;
            }
            else
                throw new CentralitaException("La llamada ya se encuentra registrada", c.GetType().Name, "Método agregar (+)");
        }
        #endregion
    }
}
