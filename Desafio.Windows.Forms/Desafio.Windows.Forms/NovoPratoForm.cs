using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desafio.Windows.Forms
{
    public partial class NovoPratoForm : Form
    {
        public string NomePratoNovo { get; private set; }
        public string CaracteristicaPratoNovo { get; private set; }

        public NovoPratoForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            NomePratoNovo = txtNomePratoNovo.Text;
            CaracteristicaPratoNovo = txtCaracteristicaPratoNovo.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
