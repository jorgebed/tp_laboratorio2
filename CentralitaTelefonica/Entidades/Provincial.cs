using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Provincial : Llamada
    {
        public enum Franja { Franja_1, Franja_2, Franja_3 };
        
        protected Franja franjaHoraria;        

        #region CONSTRUCTORES
        public Provincial(Franja miFranja, Llamada llamada) : this(llamada.NroOrigen, miFranja, llamada.Duracion, llamada.NroDestino)
        {
        }

        public Provincial(string origen, Franja miFranja, float duracion, string destino) : base(duracion, destino, origen)
        {
            this.franjaHoraria = miFranja;
        }
        #endregion

        #region PROPIEDADES
        public override float CostoLlamada { get { return this.CalcularCosto(); } }

        public string FranjaHoraria { get { return this.franjaHoraria.ToString(); } }
        #endregion

        #region METODOS
        /// <summary>
        /// Retornará el valor de la llamada a partir de la duración y el costo de la misma. 
        /// Los valores serán: Franja_1: 0.99, Franja_2: 1.25 y Franja_3: 0.66.
        /// </summary>
        /// <returns></returns>
        private float CalcularCosto()
        {
            float aux = 0;
            switch (this.franjaHoraria)
            {
                case Franja.Franja_1:
                    aux = (float)0.99 * this.Duracion;
                    break;
                case Franja.Franja_2:
                    aux = (float)1.25 * this.Duracion;
                    break;
                case Franja.Franja_3:
                    aux = (float)0.66 * this.Duracion;
                    break;
                default:
                    break;
            }
            return aux;
        }

        /// <summary>
        /// Expondrá, además de los atributos de la clase base, la propiedad CostoLlamada y franjaHoraria. 
        /// Utilizar StringBuilder.
        /// </summary>
        /// <returns></returns>
        protected override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.Mostrar());
            sb.AppendLine("COSTO: " + String.Format("{0:C}", decimal.Parse(this.CostoLlamada.ToString())));
            sb.AppendLine("FRANJA HORARIA: " + this.franjaHoraria.ToString());

            return sb.ToString();
        }

        public override string ToString()
        {
            return this.Mostrar();
        }

        public override bool Equals(object obj)
        {
            return (obj is Provincial);
        }
        #endregion        
    }
}
