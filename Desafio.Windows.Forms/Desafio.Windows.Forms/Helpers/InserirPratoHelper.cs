using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Desafio.Windows.Forms
{
    public static class InserirPratoHelper
    {
        public static void InserirNovoPrato(PratoModel prato)
        {
            using (var form = new NovoPratoForm())
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    var nomePratoNovo = form.NomePratoNovo;
                    var caracteristicaPratoNovo = form.CaracteristicaPratoNovo;

                    if (!string.IsNullOrWhiteSpace(nomePratoNovo))
                    {
                        bool pratoOuDescricaoJaExiste = ValidaPratoNovo(prato, nomePratoNovo, caracteristicaPratoNovo);

                        if (!pratoOuDescricaoJaExiste)
                        {
                            var pratoNovo = string.IsNullOrWhiteSpace(caracteristicaPratoNovo)
                            ? new PratoModel(nomePratoNovo)
                            : new PratoModel(caracteristicaPratoNovo, new List<PratoModel> { new PratoModel(nomePratoNovo) });

                            prato.ListaDePratos.Add(pratoNovo);
                        }
                    }
                }
            }
        }

        private static bool ValidaPratoNovo(PratoModel prato, string nomePratoNovo, string caracteristicaPratoNovo)
        {
            return prato.ListaDePratos.Any(p => p.Prato == nomePratoNovo || p.Prato == caracteristicaPratoNovo);
        }
    }
}
