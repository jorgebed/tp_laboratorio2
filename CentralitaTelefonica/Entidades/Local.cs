using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Local : Llamada
    {
        protected float costo;

        #region CONSTRUCTORES
        public Local(Llamada llamada, float costo) : this(llamada.NroOrigen, llamada.Duracion, llamada.NroDestino, costo)
        {
        }

        public Local(string origen, float duracion, string destino, float costo) : base(duracion, destino, origen)
        {
            this.costo = costo;
        }
        #endregion

        #region PROPIEDADES
        /// <summary>
        /// Retornará el precio, que se calculará en el método CalcularCosto.
        /// </summary>
        public override float CostoLlamada { get { return this.CalcularCosto(); } }
        #endregion

        #region METODOS
        /// <summary>
        /// Retornará el valor de la llamada a partir de la duración y el costo de la misma.
        /// </summary>
        /// <returns></returns>
        private float CalcularCosto()
        {
            return this.costo * this.Duracion;
        }

        /// <summary>
        /// Expondrá, además de los atributos de la clase base, la propiedad CostoLlamada. Utilizar StringBuilder.
        /// </summary>
        /// <returns></returns>
        protected override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.Mostrar());
            sb.AppendLine("COSTO: " + String.Format("{0:C}", decimal.Parse(this.CostoLlamada.ToString())));

            return sb.ToString();
        }

        public override string ToString()
        {
            return this.Mostrar();
        }

        /// <summary>
        /// Retorna true si el objeto es Local.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is Local);
        }
        #endregion        
    }
}
