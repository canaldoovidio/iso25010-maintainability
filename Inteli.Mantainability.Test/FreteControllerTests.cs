using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Inteli.Mantainability.Controllers;
using Inteli.Mantainability.Factories;
using Inteli.Mantainability.Models;
using Inteli.Mantainability.Services;

[TestFixture]
public class FreteControllerTests
{
    private Mock<IFreteServiceFactory> _mockFactoryA;
    private Mock<IFreteServiceFactory> _mockFactoryB;
    private Mock<IFreteServiceFactory> _mockFactoryCorreios;
    private FreteController _controller;

    [SetUp]
    public void Setup()
    {
        _mockFactoryA = new Mock<IFreteServiceFactory>();
        _mockFactoryB = new Mock<IFreteServiceFactory>();
        _mockFactoryCorreios = new Mock<IFreteServiceFactory>();

        var serviceA = new Mock<IFreteService>();
        var serviceB = new Mock<IFreteService>();
        var serviceCorreios = new Mock<IFreteService>();

        serviceA.Setup(s => s.CalcularFrete(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<bool>()))
                .Returns(new FreteResult(50, 3)); // Retorna um frete fixo

        serviceB.Setup(s => s.CalcularFrete(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<bool>()))
                .Returns(new FreteResult(100, 5));

        serviceCorreios.Setup(s => s.CalcularFrete(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<bool>()))
                       .Returns(new FreteResult(80, 7));

        _mockFactoryA.Setup(f => f.CriarServicoFrete()).Returns(serviceA.Object);
        _mockFactoryB.Setup(f => f.CriarServicoFrete()).Returns(serviceB.Object);
        _mockFactoryCorreios.Setup(f => f.CriarServicoFrete()).Returns(serviceCorreios.Object);

        _controller = new FreteController(new List<IFreteServiceFactory>
        {
            _mockFactoryA.Object,
            _mockFactoryB.Object,
            _mockFactoryCorreios.Object
        });
    }

    [Test]
    public void CalcularFrete_DeveRetornarListaDeResultados()
    {
        var request = new FreteRequest
        {
            Distancia = 150,
            Peso = 25,
            EntregaExpressa = false,
            ValorCompra = 600
        };

        var resultado = _controller.CalcularFrete(request) as OkObjectResult;
        Assert.That(resultado, Is.Not.Null);

        var listaResultados = resultado.Value as List<object>;
        Assert.That(listaResultados, Is.Not.Null);
        Assert.That(listaResultados, Has.Count.EqualTo(3)); // Deve ter 3 transportadoras
    }

    [Test]
    public void CalcularFrete_DeveAplicarFreteGratisNaTransportadoraA()
    {
        var request = new FreteRequest
        {
            Distancia = 150,
            Peso = 25,
            EntregaExpressa = false,
            ValorCompra = 350 // Acima de R$ 300 para ativar frete grátis
        };

        var resultado = _controller.CalcularFrete(request) as OkObjectResult;
        var listaResultados = resultado.Value as List<dynamic>;

        var transportadoraA = listaResultados.Find(t => t.Transportadora == "TransportadoraA");

        Assert.That(transportadoraA.Valor, Is.EqualTo(0)); // Deve ser grátis
    }

    [Test]
    public void CalcularFrete_DeveAplicarSeguroNaTransportadoraB()
    {
        var request = new FreteRequest
        {
            Distancia = 200,
            Peso = 30,
            EntregaExpressa = false,
            ValorCompra = 600 // Acima de R$ 500 para ativar seguro
        };

        var resultado = _controller.CalcularFrete(request) as OkObjectResult;
        var listaResultados = resultado.Value as List<dynamic>;

        var transportadoraB = listaResultados.Find(t => t.Transportadora == "TransportadoraB");

        Assert.That(transportadoraB.Valor, Is.EqualTo(120)); // 100 + 20 de seguro
    }
}
