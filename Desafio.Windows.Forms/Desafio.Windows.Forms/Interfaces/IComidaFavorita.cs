namespace Desafio.Windows.Forms
{
    public interface IComidaFavorita
    {
        void BuscarPrato(IPratoFavorito pratoFavorito, ref bool acertou, ref bool continuaProcura);
    }
}
