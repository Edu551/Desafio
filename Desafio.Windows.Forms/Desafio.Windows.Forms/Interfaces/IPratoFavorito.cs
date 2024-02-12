namespace Desafio.Windows.Forms
{
    public interface IPratoFavorito
    {
        void ValidarPrato(PratoModel prato, ref bool acertou, ref bool continuaProcura);
    }
}
