using System.Collections.Generic;

namespace Desafio.Windows.Forms
{
    public class PratoModel : IComidaFavorita
    {
        public string Prato { get; set; }
        public List<PratoModel> ListaDePratos { get; set; } = new List<PratoModel>();
        public bool ListaDePratosEstaVazia => ListaDePratos.Count == 0;

        public PratoModel(string prato)
        {
            Prato = prato;
        }

        public PratoModel(string prato, List<PratoModel> listaDePratos)
        {
            Prato = prato;
            ListaDePratos = listaDePratos;
        }

        public void BuscarPrato(IPratoFavorito pratoFavorito, ref bool acertou, ref bool continuaProcura)
        {
            pratoFavorito.ValidarPrato(this, ref acertou, ref continuaProcura);
        }
    }
}
