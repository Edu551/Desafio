using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desafio.Windows.Forms
{
    public partial class JogoGourmetForm : Form
    {
        private readonly List<PratoModel> prato = new List<PratoModel>
        {
            new PratoModel("Bolo de chocolate"),
            new PratoModel("Massa", new List<PratoModel>
            {
                new PratoModel("Lasanha"),
                new PratoModel("Macarrão")
            }),
        };

        private PratoModel pratoAtual;

        public JogoGourmetForm()
        {
            InitializeComponent();
            pratoAtual = new PratoModel("", prato);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool acertou = false;
            bool continuaProcura = true;

            pratoAtual.BuscarPrato(new ValidarPratoHelper(), ref acertou, ref continuaProcura);

            if (acertou)
            {
                MessageBox.Show("Acertei de novo!");
            }

            if (!acertou && continuaProcura)
            {
                InserirPratoHelper.InserirNovoPrato(pratoAtual);
            }
        }
    }
}
