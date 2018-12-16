using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Entidades;

namespace FrmCentralitaTelefonica
{
    public partial class FrmLlamador : Form
    {
        Thread hilo;
        Queue<string> numero = new Queue<string>();
        Centralita centralitaLlamador;
        Local local;
        Provincial provincial;

        public FrmLlamador()
        {
            InitializeComponent();
            cmbFranja.DataSource = Enum.GetValues(typeof(Provincial.Franja));
            this.txtNroOrigen.Text = "0800 - 333 - 9285";
        }

        public FrmLlamador(Centralita c)
            : this()
        {
            this.centralitaLlamador = c;
        }

        public Centralita PropCentralita
        {
            get { return this.centralitaLlamador; }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            numero.Enqueue("1");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            numero.Enqueue("2");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            numero.Enqueue("3");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            numero.Enqueue("4");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            numero.Enqueue("5");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            numero.Enqueue("6");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            numero.Enqueue("7");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            numero.Enqueue("8");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            numero.Enqueue("9");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            numero.Enqueue("0");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btnAsterisco_Click(object sender, EventArgs e)
        {
            numero.Enqueue("*");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private void btnNumeral_Click(object sender, EventArgs e)
        {
            numero.Enqueue("#");
            this.txtNroDestino.Text = this.MostrarNumero();
        }

        private string MostrarNumero()
        {
            if (this.numero.Count == 4)
                this.numero.Enqueue(" - ");
            StringBuilder sb = new StringBuilder();
            foreach (string item in this.numero)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }

        private void txtNroDestino_TextChanged(object sender, EventArgs e)
        {
            if (this.numero.Count != 0)
            {
                if (this.numero.Peek() != "#")
                    this.cmbFranja.Enabled = false;
                else
                    this.cmbFranja.Enabled = true;
            }
        }

        /// <summary>
        /// Llama al método que limpia el formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {            
            this.LimpiarFormulario();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {            
            if(this.hilo.IsAlive)
                this.hilo.Abort();
            this.Close();
            //new FrmMenu().ShowDialog();
        }

        private void btnLlamar_Click(object sender, EventArgs e)
        {
            if (this.numero.Count != 0)
            {
                try
                {
                    hilo = new Thread(new ParameterizedThreadStart(this.AsignarTiempo));
                    Random random = new Random();
                    int duracion = random.Next(1, 15);
                    Provincial.Franja franjas;
                    Enum.TryParse<Provincial.Franja>(cmbFranja.SelectedValue.ToString(), out franjas);
                    float costoLocal = (float)random.Next(1, 6);
                    //hilo.Start(duracion);

                    if (this.numero.Peek() != "#")
                    {
                        int antes = this.centralitaLlamador.Llamadas.Count;
                        local = new Local(this.txtNroOrigen.Text, duracion, this.txtNroDestino.Text, costoLocal);
                        this.centralitaLlamador += local;
                        int despues = this.centralitaLlamador.Llamadas.Count;
                        if (antes != despues)
                        {
                            CentralitaDAO centralitaBDD = new CentralitaDAO();
                            centralitaBDD.Guardar(local);
                            hilo.Start(duracion);
                        }
                    }
                    else
                    { 
                        int antes = this.centralitaLlamador.Llamadas.Count;
                        provincial = new Provincial(this.txtNroOrigen.Text, franjas, duracion, this.txtNroDestino.Text);
                        this.centralitaLlamador += provincial;
                        int despues = this.centralitaLlamador.Llamadas.Count;
                        if (antes != despues)
                        {
                            CentralitaDAO centralitaBDD = new CentralitaDAO();
                            centralitaBDD.Guardar(provincial);
                            hilo.Start(duracion);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Realiza una simulación de llamada. Al final muestra la duración de la llamada.
        /// </summary>
        /// <param name="duracion"></param>
        public void AsignarTiempo(Object duracion)
        {
            int tiempo = 0;
            string texto = "Llamando";
            int j = 0;
            int max = 6;

            // Muestra el mensaje "Llamando" y "Conectando" con el efecto de parpadear.
            for (int i = 0; i < max; i++)
            {
                if (this.txtNroDestino.InvokeRequired)
                {
                    this.txtNroDestino.BeginInvoke((MethodInvoker)delegate()
                    {
                        if (i == max - 1)
                            this.txtNroDestino.Text = "Conectando";
                        else if (i % 2 == 0)
                            this.txtNroDestino.Text = string.Empty;
                        else
                            this.txtNroDestino.Text = texto;
                    }
                    );
                }
                System.Threading.Thread.Sleep(800);
            }

            // Simula un contador de duración de llamada.
            while (j < (int)duracion)
            {
                tiempo = (int)duracion;
                if (this.txtNroDestino.InvokeRequired)
                {
                    this.txtNroDestino.BeginInvoke((MethodInvoker)delegate()
                    {
                        if (j < 10)
                            this.txtNroDestino.Text = "00:0" + j.ToString();
                        else
                            this.txtNroDestino.Text = "00:" + j.ToString();
                        j++;
                    }
                    );
                }
                else
                {
                    this.txtNroDestino.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                }
                System.Threading.Thread.Sleep(1000);
            }

            // Muestra el mensaje de finalización de llamada.
            if (this.txtNroDestino.InvokeRequired)
            {
                this.txtNroDestino.BeginInvoke((MethodInvoker)delegate()
                {
                    this.txtNroDestino.Text = "Fin de la llamada";                    
                }
                );
                Thread.Sleep(1000);                
            }

            // Muestra el mensaje de duración de llamada.
            if (this.txtNroDestino.InvokeRequired)
            {
                this.txtNroDestino.BeginInvoke((MethodInvoker)delegate()
                {
                    if(tiempo < 10)
                        this.txtNroDestino.Text = "Duración <00:0" + tiempo + ">";
                    else
                        this.txtNroDestino.Text = "Duración <00:" + tiempo + ">";
                }                
                );
                Thread.Sleep(1000);
            }

            // Limpia todo el formulario.
            if (this.txtNroDestino.InvokeRequired)
            {
                this.txtNroDestino.BeginInvoke((MethodInvoker)delegate()
                {
                    this.LimpiarFormulario();
                }
                );
            }
        }

        /// <summary>
        /// Método que limpia todo el formulario Llamador.
        /// </summary>
        private void LimpiarFormulario()
        {
            this.txtNroDestino.Clear();
            this.numero.Clear();
            this.cmbFranja.Enabled = true;
        }
    }
}
