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

namespace MainCorreo
{
    public partial class FrmPpal : Form
    {
        private Correo correo;
        private Paquete paquete;
        private ContextMenu cmsLista = new ContextMenu();

        public FrmPpal()
        {
            InitializeComponent();
            correo = new Correo();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string direccion = this.txtDireccion.Text;
            string trackingID = this.mtxtTrackingID.Text;

            if (direccion == "")
                MessageBox.Show("Es necesario ingresar una dirección");
            if (trackingID == "")
                MessageBox.Show("Para realizar un seguimiento es necesario ingresar TrackingID");
            if (direccion != "" && trackingID != "")
            {
                paquete = new Paquete(this.txtDireccion.Text, this.mtxtTrackingID.Text);
                paquete.InformarEstado += paq_InformaEstado;
                try
                {
                    correo += paquete;
                    //this.ActualizarEstados();
                }
                catch (TrackingIdRepetidoException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
        }

        private void ActualizarEstados()
        {
            this.lstEstadoIngresado.Items.Clear();
            this.lstEstadoEnViaje.Items.Clear();
            this.lstEstadoEntregado.Items.Clear();

            foreach (Paquete item in correo.Paquetes)
            {
                switch (item.Estado)
                {
                    case Paquete.EEstado.Ingresado:
                        if (item.Estado == Paquete.EEstado.Ingresado)
                            this.lstEstadoIngresado.Items.Add(item);
                        break;
                    case Paquete.EEstado.EnViaje:
                        if (item.Estado == Paquete.EEstado.EnViaje)                       
                            lstEstadoEnViaje.Items.Add(item.ToString());                      
                        break;
                    case Paquete.EEstado.Entregado:
                        if (item.Estado == Paquete.EEstado.Entregado)
                            lstEstadoEntregado.Items.Add(item.ToString());
                        break;
                }
            }
        }

        private void MostrarInformacion<T>(IMostrar<T> elemento)
        {            
            if (!Object.ReferenceEquals(elemento, null))
            {
                if (elemento is Paquete)                
                    rtbMostrar.Text = ((Paquete)elemento).ToString();
                
                else if (elemento is Correo)
                    rtbMostrar.Text = ((Correo)elemento).MostrarDatos((Correo)elemento);
                
                rtbMostrar.Text.Guardar("Correo.txt");
            }
        }

        /// <summary>
        /// Cierra todos los hilos abiertos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPpal_FormClosing(object sender, FormClosingEventArgs e)
        {
            correo.FinEntregas();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }

        private void paq_InformaEstado(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Paquete.DelegadoEstado d = new Paquete.DelegadoEstado(paq_InformaEstado);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                ActualizarEstados();
            }
        }
    }
}
