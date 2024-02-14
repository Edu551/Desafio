using NUnit.Framework;
using NUnit.Framework.Legacy;

[TestFixture]
public class JogoGourmetTests
{
    [Test]
    public void ValidarPrato_DeveRetornarPratoCorreto_QuandoUsuarioRespondeSimNoPrimeiroPrato()
    {
        PratoModel pratoInicial = new()
        {
            ListaDePratos = new()
            {
                new PratoModel { Prato = "Lasanha" },
                new PratoModel { Prato = "Macarrão" },
                new PratoModel { Prato = "Bolo de chocolate" }
            }
        };

        Console.SetIn(new StringReader("s"));

        PratoModel resultado = ValidarPratoHelper.ValidarPrato(pratoInicial);

        ClassicAssert.AreEqual("Lasanha", resultado.Prato);

        ClassicAssert.AreNotEqual("Macarrão", resultado.Prato);
        ClassicAssert.AreNotEqual("Bolo de chocolate", resultado.Prato);
    }

    [Test]
    public void ValidarPrato_DeveRetornarPratoCorreto_QuandoUsuarioRespondeSimNoSegundoPrato()
    {
        PratoModel pratoInicial = new()
        {
            ListaDePratos = new()
            {
                new PratoModel { Prato = "Lasanha" },
                new PratoModel { Prato = "Macarrão" },
                new PratoModel { Prato = "Bolo de chocolate" }
            }
        };

        string respostaPratoUm = "n";
        string respostaPratoDois = "s";
        Console.SetIn(new StringReader($"{respostaPratoUm}\n{respostaPratoDois}\n"));

        PratoModel resultado = ValidarPratoHelper.ValidarPrato(pratoInicial);

        ClassicAssert.AreEqual("Macarrão", resultado.Prato);

        ClassicAssert.AreNotEqual("Lasanha", resultado.Prato);
        ClassicAssert.AreNotEqual("Bolo de chocolate", resultado.Prato);
    }

    [Test]
    public void ValidarPrato_DeveRetornarListaVazia_QuandoOJogoNaoAcertaOPrato()
    {
        PratoModel pratoInicial = new ()
        {
            ListaDePratos = new ()
            {
                new PratoModel { Prato = "Lasanha" },
                new PratoModel { Prato = "Macarrão" },
                new PratoModel { Prato = "Bolo de chocolate" }
            }
        };

        Console.SetIn(new StringReader($"n\n n\n n\n"));

        PratoModel resultado = ValidarPratoHelper.ValidarPrato(pratoInicial);

        ClassicAssert.IsEmpty(resultado.ListaDePratos);
        ClassicAssert.AreEqual("", resultado.Prato);

        ClassicAssert.AreNotEqual("Lasanha", resultado.Prato);
        ClassicAssert.AreNotEqual("Macarrão", resultado.Prato);
        ClassicAssert.AreNotEqual("Bolo de chocolate", resultado.Prato);
    }

    [Test]
    public void InserirPratoHelper_InserirNovoPratoSemDescicao_PratoInseridoComSucesso()
    {
        PratoModel prato = new("Massa");
        string input = "Macarrão";
        StringReader nomePratoNovo = new(input);
        Console.SetIn(nomePratoNovo);

        InserirPratoHelper.InserirNovoPrato(prato);

        ClassicAssert.AreEqual(1, prato.ListaDePratos.Count);
        ClassicAssert.AreEqual(0, prato.ListaDePratos[0].ListaDePratos.Count);
        ClassicAssert.AreEqual("Macarrão", prato.ListaDePratos[0].Prato);
    }

    [Test]
    public void InserirPratoHelper_InserirPratoComDescicao_CriaUmaNovaListaComADescricao()
    {
        PratoModel prato = new("Massa");
        string input_1 = "Macarrão";
        string input_2 = "Comprido";
        StringReader nomeEDescricao = new($"{input_1}\n{input_2}\n");
        Console.SetIn(nomeEDescricao);

        InserirPratoHelper.InserirNovoPrato(prato);

        ClassicAssert.AreEqual(1, prato.ListaDePratos[0].ListaDePratos.Count);
    }

    [Test]
    public void InserirPratoHelper_InserirPratoQueJaExiste_SeOPratoExistirNaoDeveSerInseridoNovamente()
    {
        PratoModel prato = new("Massa");
        prato.ListaDePratos.Add(new("Macarrão"));
        string input = "Macarrão";
        StringReader nomePratoNovo = new(input);
        Console.SetIn(nomePratoNovo);

        InserirPratoHelper.InserirNovoPrato(prato);

        ClassicAssert.AreEqual(1, prato.ListaDePratos.Count);
    }

    [Test]
    public void InserirPratoHelper_UsuarioNaoDigitaNomeDoNovoPrato_NaoDeveSerAdicionadoUmNovoPrato()
    {
        PratoModel prato = new("Massa");
        string input = "";
        StringReader nomePratoNovo = new(input);
        Console.SetIn(nomePratoNovo);

        InserirPratoHelper.InserirNovoPrato(prato);

        ClassicAssert.AreEqual(0, prato.ListaDePratos.Count);
    }
}
