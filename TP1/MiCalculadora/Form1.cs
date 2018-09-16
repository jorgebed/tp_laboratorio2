using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace MiCalculadora
{
    public partial class LaCalculadora : Form
    {
        public LaCalculadora()
        {
            InitializeComponent();
        }

        private void btnOperar_Click(object sender, EventArgs e)
        {
            this.lblResultado.Text = (LaCalculadora.Operar(this.txtNumero1.Text, this.txtNumero2.Text, this.cmbOperador.Text)).ToString();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        /// <summary>
        /// El evento click del botón btnConvertirABinario convertirá el resultado, de existir, a binario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertirABinario_Click(object sender, EventArgs e)
        {
            double numero;
            //Valido que en lblResultado exista dato sino que no realice ninguna operación
            if (double.TryParse(this.lblResultado.Text, out numero))
            {
                Numero num1 = new Numero(this.lblResultado.Text);
                this.lblResultado.Text = num1.DecimalBinario(this.lblResultado.Text);
            }
        }

        /// <summary>
        /// El evento click del botón btnConvertirADecimal convertirá el resultado, de existir y ser binario, a decimal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConvertirADecimal_Click(object sender, EventArgs e)
        {
            double numero;
            //La Validación de ser un número positivo se encuentra en el método BinarioDecimal de la Clase Numero
            //Valido que en lblResultado exista dato sino que no realice ninguna operación
            if (double.TryParse(this.lblResultado.Text, out numero))
            {
                Numero num1 = new Numero(this.lblResultado.Text);
                this.lblResultado.Text = num1.BinarioDecimal(this.lblResultado.Text);
            }
        }

        /// <summary>
        /// El botón btnCerrar deberá cerrar el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// El método Limpiar será llamado por el evento click del botón btnLimpiar y borrará los datos de los TextBox, ComboBox y Label de la pantalla.
        /// </summary>
        private void Limpiar()
        {
            this.lblResultado.ResetText();
            this.txtNumero1.Clear();
            this.txtNumero2.Clear();
            this.cmbOperador.ResetText();
        }

        /// <summary>
        /// El método Operar será estático recibirá los dos números y el operador para luego llamar al método Operar de Calculadora y retornar el resultado al método de evento del botón btnOperar que reflejará el resultado en el Label txtResultado.
        /// </summary>
        /// <param name="numero1"></param>
        /// <param name="numero2"></param>
        /// <param name="operador"></param>
        /// <returns></returns>
        static double Operar(string numero1, string numero2, string operador)
        {
            Numero num1 = new Numero(numero1);
            Numero num2 = new Numero(numero2);
            return Calculadora.operar(num1, num2, operador);
        }
    }
}
