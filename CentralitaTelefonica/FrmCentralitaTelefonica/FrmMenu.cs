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

namespace FrmCentralitaTelefonica
{
    public partial class FrmMenu : Form
    {
        Centralita centralita;

        public FrmMenu()
        {
            try
            {
                InitializeComponent();
                this.centralita = new Centralita();
                this.centralita.RazonSocial = "Telefónica de Argentina";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGenerarLlamada_Click(object sender, EventArgs e)
        {
            //this.Hide();
            FrmLlamador llamador = new FrmLlamador(centralita);
            llamador.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFacturacionTotal_Click(object sender, EventArgs e)
        {
            FrmMostrar mostrar = new FrmMostrar(centralita);
            mostrar.Text = "Facturación Total";
            mostrar.RichTextBox = this.centralita.ToString();
            
            try
            {
                Texto texto = new Texto();
                Xml<string> xml = new Xml<string>();
                texto.Guardar(this.centralita.RazonSocial + " Facturación Total.txt", this.centralita.ToString());
                xml.Guardar(this.centralita.RazonSocial + " Facturación Total.xml", this.centralita.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mostrar.ShowDialog();
        }

        private void btnFacturacionLocal_Click(object sender, EventArgs e)
        {
            FrmMostrar mostrar = new FrmMostrar(centralita);
            mostrar.Text = "Facturación Local";
            mostrar.RichTextBox = "GANANCIA LOCAL: " + String.Format("{0:C}", decimal.Parse(this.centralita.GananciasPorLocal.ToString()));
            mostrar.ShowDialog();
        }

        private void btnFacturacionProvincial_Click(object sender, EventArgs e)
        {
            FrmMostrar mostrar = new FrmMostrar(centralita);
            mostrar.Text = "Facturación Provincial";
            mostrar.RichTextBox = "GANANCIA PROVINCIAL: " + String.Format("{0:C}", decimal.Parse(this.centralita.GananciasPorProvincial.ToString()));
            mostrar.ShowDialog();
        }
    }
}
