
List<PratoModel> prato = new List<PratoModel>
        {
            new PratoModel("Massa", new List<PratoModel>
            {
                new PratoModel("Lasanha"),
                new PratoModel("Macarrão")
            }),
            new PratoModel("Bolo de chocolate")
        };

PratoModel primeiroPrato = new("", prato);

bool continuarJogo = true;
bool pararLoop = false;

while (continuarJogo)
{
    PratoModel pratoInicial = primeiroPrato;

    Console.WriteLine("\nPense em um prato que gosta, digite 's' para 'sim' e 'n' para 'não'");
    while (!pararLoop)
    {
        PratoModel pratoValidadoPeloUsuario = ValidarPratoHelper.ValidarPrato(pratoInicial);

        pararLoop = string.IsNullOrEmpty(pratoValidadoPeloUsuario.Prato) && pratoValidadoPeloUsuario.ListaDePratosEhVazia;

        if (pararLoop)
        {
            InserirPratoHelper.InserirNovoPrato(pratoInicial);
        }
        else
        {
            if (pratoValidadoPeloUsuario.ListaDePratosEhVazia)
            {
                Console.WriteLine($"Acertei de novo! O prato que você pensou é {pratoValidadoPeloUsuario.Prato}");
                pararLoop = true;
            }
            else
            {
                pratoInicial = pratoValidadoPeloUsuario;
            }
        }
    }

    Console.WriteLine("Deseja sair? Responda sim ou não");
    string respostaUsuario = Console.ReadLine().ToLower();

    continuarJogo = respostaUsuario != "sim";
    pararLoop = false;
    Console.Clear();
}



public class PratoModel
{
    public string Prato { get; set; } = "";

    public List<PratoModel> ListaDePratos { get; set; } = [];

    public bool ListaDePratosEhVazia => ListaDePratos.Count == 0;

    public PratoModel()
    { }

    public PratoModel(string prato)
    {
        Prato = prato;
    }

    public PratoModel(string prato, List<PratoModel> listaDePratos)
    {
        Prato = prato;
        ListaDePratos = listaDePratos;
    }
}


public class ValidarPratoHelper
{
    public static PratoModel ValidarPrato(PratoModel pratoInicial)
    {
        PratoModel listaVazia = new();

        foreach (PratoModel prato in pratoInicial.ListaDePratos)
        {
            Console.WriteLine($"O prato que você pensou é {prato.Prato} ?");
            string? respostaUsuario = Console.ReadLine().ToLower();

            if (respostaUsuario == "s")
            {
                return prato;
            }
        }

        return listaVazia;
    }
}

public class InserirPratoHelper
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

            bool pratoOuDescricaoJaExiste = ValidaPratoNovo(prato, nomePratoNovo, caracteristicaPratoNovo);
            if (!pratoOuDescricaoJaExiste)
            {
                PratoModel pratoNovo = string.IsNullOrWhiteSpace(caracteristicaPratoNovo)
                ? new(nomePratoNovo)
                : new(caracteristicaPratoNovo, [new(nomePratoNovo)]);

                prato.ListaDePratos.Add(pratoNovo);
            }
        }
    }

    private static bool ValidaPratoNovo(PratoModel prato, string nomePratoNovo, string? caracteristicaPratoNovo)
    {
        return prato.ListaDePratos.Any(p => p.Prato.Equals(nomePratoNovo, StringComparison.CurrentCultureIgnoreCase)
                                         || p.Prato.Equals(caracteristicaPratoNovo, StringComparison.CurrentCultureIgnoreCase));
    }
}