using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Calculadora
    {
        /// <summary>
        /// El método ValidarOperador será privado y estático. Deberá validar que el operador recibido sea +, -, / o *. Caso contrario retornará +.
        /// </summary>
        /// <param name="operador"></param>
        /// <returns></returns>
        static string ValidarOperador(string operador)
        {
            if (operador == "+" || operador == "-" || operador == "*" || operador == "/")
                return operador;
            else
                return "+";
        }

        /// <summary>
        /// El método Operar validará y realizará la operación pedida entre ambos números.
        /// </summary>
        /// <param name="numero1"></param>
        /// <param name="numero2"></param>
        /// <param name="operador"></param>
        /// <returns></returns>
        public static double operar(Numero num1, Numero num2, string operador)
        {
            double numero = 0;

            switch (Calculadora.ValidarOperador(operador))
            {
                case "+":
                    numero = num1 + num2;
                    break;
                case "-":
                    numero = num1 - num2;
                    break;
                case "*":
                    numero = num1 * num2;
                    break;
                case "/":
                    numero = num1 / num2;
                    break;
            }
            return numero;
        }
    }
}
