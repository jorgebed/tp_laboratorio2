using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Numero
    {
        private double numero;
        
        #region CONSTRUCTORES
        /// <summary>
        /// No especifica la documentación que debería realizar el constructor por defecto. Se asigna un cero.
        /// </summary>
        public Numero()
        {
            this.numero = 0;
        }

        /// <summary>
        /// Constructor recibe un double
        /// </summary>
        /// <param name="numero"></param>
        public Numero(double numero)
        {
            this.SetNumero = numero.ToString();
        }

        public Numero(string strNumero)
        {
            this.SetNumero = strNumero;
        }
        #endregion

        #region METODOS
        /// <summary>
        /// Convertirá un número binario a decimal, en caso de ser posible. Caso contrario retornará "Valor inválido".
        /// </summary>
        /// <param name="binario"></param>
        /// <returns></returns>
        public string BinarioDecimal(string binario)
        {
            double resultado = 0;
            int cantidad = binario.Length;
            double numero;
            string aux = string.Empty;
            bool flag = true;

            //Verifico que el dato ingresado sea binario
            for (int i = 0; i < cantidad; i++)
            {
                aux = binario.Substring(i, 1);
                if (aux != "0" && aux != "1")
                {
                    flag = false;
                    break;
                }
            }

            //Si es binario realiza el cálculo
            if (flag)
            {
                aux = string.Empty;
                for (int i = 0; i < cantidad; i++)
                {
                    //Paso a double el caracter del dato ingresado en esa posición.
                    numero = double.Parse(binario.Substring(i, 1));
                    resultado += numero * Math.Pow(2, cantidad - (i + 1));
                }
                return resultado.ToString();
            }
            return "Valor inválido";
        }

        /// <summary>
        /// Convertirán un número decimal a binario, en caso de ser posible. Caso contrario retornará "Valor inválido". Reutilizar código.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public string DecimalBinario(double numero)
        {
            int resultado, resto;
            string binario = string.Empty;

            do
            {
                resultado = (int)numero / 2;
                resto = (int)numero % 2;
                binario = resto.ToString() + binario;
                numero = resultado;

            } while (resultado >= 2);

            return binario = resultado + binario;
        }

        /// <summary>
        /// Convertirán un número decimal a binario, en caso de ser posible. Caso contrario retornará "Valor inválido". Reutilizar código.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public string DecimalBinario(string numero)
        {
            double aux;
            if (double.TryParse(numero, out aux))
                return DecimalBinario(aux);
            return "Valor inválido";
        }

        /// <summary>
        /// ValidarNumero comprobará que el valor recibido sea numérico, y lo retornará en formato double. Caso contrario, retornará 0.
        /// </summary>
        /// <param name="strNumero"></param>
        /// <returns></returns>
        private static double ValidarNumero(string strNumero)
        {
            double numero = 0;

            if (double.TryParse(strNumero, out numero))
                return numero;
            else
                return 0;
        }
        #endregion
        
        #region SOBRECARGAS
        /// <summary>
        /// Los operadores realizarán las operaciones correspondientes entre dos números.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static double operator -(Numero n1, Numero n2)
        {
            return n1.numero - n2.numero;
        }

        /// <summary>
        /// Los operadores realizarán las operaciones correspondientes entre dos números.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static double operator *(Numero n1, Numero n2)
        {
            return n1.numero * n2.numero;
        }

        /// <summary>
        /// Los operadores realizarán las operaciones correspondientes entre dos números.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static double operator /(Numero n1, Numero n2)
        {
            //NO ESTÁ ESPECIFICADO QUE HACER SI LA DIVISION ES POR CERO, DEVUELVE UN INFINITO QUE PUEDE TRAER PROBLEMAS AL SEGUIR OPERANDO.
            //if (n2.numero == 0)
            //    return 0;
            return n1.numero / n2.numero;
        }

        /// <summary>
        /// Los operadores realizarán las operaciones correspondientes entre dos números.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static double operator +(Numero n1, Numero n2)
        {
            return n1.numero + n2.numero;
        }
        #endregion               

        #region PROPIEDADES
        /// <summary>
        /// La propiedad SetNumero asignará un valor al atributo numero, previa validación.
        /// En este lugar será el único en todo el código que llame al método ValidarNumero.
        /// </summary>
        public string SetNumero
        {
            set
            {
                this.numero = Numero.ValidarNumero(value);
            }
        }
        #endregion       
    }	
}