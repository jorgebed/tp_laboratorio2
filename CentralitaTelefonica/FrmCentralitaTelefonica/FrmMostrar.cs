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
    public partial class FrmMostrar : Form
    {
        Centralita centalita;
        public FrmMostrar()
        {
            InitializeComponent();
        }

        public FrmMostrar(Centralita c)
            : this()
        {
            this.centalita = c;
        }

        public string RichTextBox
        {
            set { this.richTextBox.Text = value; }
        }
    }
}
