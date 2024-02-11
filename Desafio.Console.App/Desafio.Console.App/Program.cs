
List<PratoModel> prato = new List<PratoModel>
        {
            new PratoModel("Primeiro item"),
            new PratoModel("Massa", new List<PratoModel>
            {
                new PratoModel("Lasanha"),
                new PratoModel("Macarrão")
            }),
            new PratoModel("Bolo de chocolate")
        };

PratoModel pratoInicial = new("", prato);

IPratoFavorito pratoQueGosta = new ImprimePrato();

var sair = "n";
do
{
    Console.WriteLine("\nPense em um prato que gosta, digite 's' para 'sim' e 'n' para 'não'");

    var acertou = false;
    bool continuaProcura = true;

    var indice = pratoInicial.ListaDePratos.Count;

    while (indice > 0)
    {
        indice--;
        pratoInicial.ListaDePratos[indice].BuscarPrato(pratoQueGosta, ref acertou, ref continuaProcura);

        if (acertou || !continuaProcura)
        {
            break;
        }

        if (indice == 0)
        {
            Funcoes.InserirNovoPrato(pratoInicial);
        }
    }

    if (acertou)
    {
        Console.WriteLine("Acertei de novo! \n");
    }

    Console.WriteLine("Deseja sair? Se sim digite 'sim' \n");
    sair = Console.ReadLine();
    Console.Clear();
} while (sair != "sim");



public interface IPratoFavorito
{
    void ValidarPrato(PratoModel prato, ref bool acertou, ref bool continuaProcura);
}

public interface IComidaFavorita
{
    void BuscarPrato(IPratoFavorito pratoFavorito, ref bool acertou, ref bool continuaProcura);
}

public class PratoModel : IComidaFavorita
{
    public string Prato { get; set; }

    public List<PratoModel> ListaDePratos { get; set; } = [];

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


public class ImprimePrato : IPratoFavorito
{
    public void ValidarPrato(PratoModel prato, ref bool acertou, ref bool continuaProcura)
    {
        acertou = false;

        Console.WriteLine($"O prato que você pensou é {prato.Prato} ?");

        string? respostaUsuario = Console.ReadLine();

        if (respostaUsuario == "s")
        {
            acertou = true;
            continuaProcura = false;
            var indice = prato.ListaDePratos.Count;

            IPratoFavorito pratoQueGosta = new ImprimePrato();
            while (indice > 0)
            {
                indice--;
                prato.ListaDePratos[indice].BuscarPrato(pratoQueGosta, ref acertou, ref continuaProcura);

                if (acertou)
                {
                    break;
                }

                if (indice == 0)
                {
                    Funcoes.InserirNovoPrato(prato);
                }
            }
        }
        else
        {
            return;
        }
    }
}

public class Funcoes
{
    public static void InserirNovoPrato(PratoModel prato)
    {
        Console.WriteLine("Qual prato você pensou?");
        string? nomePratoNovo = Console.ReadLine();

        if (!string.IsNullOrEmpty(nomePratoNovo))
        {

            Console.WriteLine($@"Digite uma característica do prato {nomePratoNovo}. 
Caso não queira dar uma característica deixe em branco.");
            string? caracteristicaPratoNovo = Console.ReadLine();

            PratoModel pratoNovo = new(string.IsNullOrWhiteSpace(caracteristicaPratoNovo)
                ? nomePratoNovo
                : caracteristicaPratoNovo, [new(nomePratoNovo)]);

            prato.ListaDePratos.Add(pratoNovo);
        }
    }
}