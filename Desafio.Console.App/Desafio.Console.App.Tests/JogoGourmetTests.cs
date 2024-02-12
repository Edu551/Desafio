using NUnit.Framework;
using NUnit.Framework.Legacy;

[TestFixture]
public class JogoGourmetTests
{
    [Test]
    public void ValidarPratoHelper_JogoDescobreOPratoCerto_AcertouOPrato()
    {
        PratoModel prato = new ("Bolo de Chocolate");
        ValidarPratoHelper validarPratoHelper = new ();
        string input = "s";
        StringReader respostaUsuario = new (input);

        Console.SetIn(respostaUsuario);

        bool acertou = false;
        bool continuaProcura = true;
        validarPratoHelper.ValidarPrato(prato, ref acertou, ref continuaProcura);

        ClassicAssert.IsTrue(acertou);
        ClassicAssert.IsFalse(continuaProcura);
    }

    [Test]
    public void InserirPratoHelper_InserirNovoPratoSemDescicao_PratoInseridoComSucesso()
    {
        PratoModel prato = new ("Massa");
        string input = "Macarrão";
        StringReader nomePratoNovo = new (input);
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
        PratoModel prato = new ("Massa");
        prato.ListaDePratos.Add(new ("Macarrão"));
        string input = "Macarrão";
        StringReader nomePratoNovo = new (input);
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
