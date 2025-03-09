using Inteli.Mantainability.Decorators;
using Inteli.Mantainability.Services;

using NUnit.Framework;

using NUnit.Framework;

[TestFixture]
public class TransportadoraATests
{
    [Test]
    public void TransportadoraA_DeveCalcularFreteCorretamente()
    {
        var service = new TransportadoraAFreteService();
        var resultado = service.CalcularFrete(100, 15, false);

        Assert.That(resultado.Valor, Is.EqualTo(115)); // 100 * 1.10 + 5 + 3.75
        Assert.That(resultado.PrazoEmDias, Is.EqualTo(4));
    }

    [Test]
    public void TransportadoraA_DeveAplicarFreteGratisParaComprasAcimaDe300()
    {
        var service = new TransportadoraAFreteService();
        var decorador = new FreteGratisDecorator(service, 350); // Compra acima de R$ 300

        var resultado = decorador.CalcularFrete(100, 15, false);

        Assert.That(resultado.Valor, Is.EqualTo(0));
        Assert.That(resultado.PrazoEmDias, Is.EqualTo(4));
    }

    [Test]
    public void TransportadoraA_NaoDeveAplicarFreteGratisParaComprasAbaixoDe300()
    {
        var service = new TransportadoraAFreteService();
        var decorador = new FreteGratisDecorator(service, 250); // Compra abaixo de R$ 300

        var resultado = decorador.CalcularFrete(100, 15, false);

        Assert.That(resultado.Valor, Is.GreaterThan(0));
        Assert.That(resultado.PrazoEmDias, Is.EqualTo(4));
    }
}

