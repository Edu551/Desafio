using System.Windows.Forms;

namespace Desafio.Windows.Forms
{
    public class ValidarPratoHelper : IPratoFavorito
    {
        public void ValidarPrato(PratoModel pratoInicial, ref bool acertou, ref bool continuaProcura)
        {
            acertou = false;
            var indicePratoInicial = pratoInicial.ListaDePratos.Count;

            while (indicePratoInicial > 0)
            {
                if (!continuaProcura)
                {
                    break;
                }

                indicePratoInicial--;
                var pratoSelecionado = pratoInicial.ListaDePratos[indicePratoInicial];

                var respostaUsuario = MessageBox.Show($"O prato que você pensou é {pratoSelecionado.Prato}?", "Adivinhação de Prato", MessageBoxButtons.YesNo);

                if (respostaUsuario == DialogResult.Yes)
                {
                    acertou = true;
                    var indice = pratoSelecionado.ListaDePratos.Count;

                    if (pratoSelecionado.ListaDePratosEstaVazia)
                    {
                        continuaProcura = false;
                        break;
                    }

                    while (indice >= 1 && continuaProcura)
                    {
                        indice--;
                        pratoSelecionado.BuscarPrato(new ValidarPratoHelper(), ref acertou, ref continuaProcura);

                        if (acertou)
                        {
                            break;
                        }

                        if (continuaProcura)
                        {
                            InserirPratoHelper.InserirNovoPrato(pratoSelecionado);
                            continuaProcura = false;
                        }
                    }
                }
            }
        }
    }
}
