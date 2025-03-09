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

        // Criando instâncias reais das transportadoras
        var serviceA = new TransportadoraAFreteService();
        var serviceB = new TransportadoraBFreteService();
        var serviceCorreios = new CorreiosFreteService();

        // Configurando os mocks para retornar instâncias REAIS
        _mockFactoryA.Setup(f => f.CriarServicoFrete()).Returns(serviceA);
        _mockFactoryB.Setup(f => f.CriarServicoFrete()).Returns(serviceB);
        _mockFactoryCorreios.Setup(f => f.CriarServicoFrete()).Returns(serviceCorreios);

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
        Assert.That(resultado, Is.Not.Null, "O controller não retornou um OkObjectResult.");
        Assert.That(resultado.Value, Is.Not.Null, "O valor retornado pelo controller é nulo.");

        var listaResultados = resultado.Value as List<FreteCalculoDetalhes>;
        Assert.That(listaResultados, Is.Not.Null, "A lista de resultados está nula.");
        Assert.That(listaResultados.Count, Is.EqualTo(3), "O número de transportadoras retornado não é o esperado.");
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
        Assert.That(resultado, Is.Not.Null, "O controller não retornou um OkObjectResult.");
        Assert.That(resultado.Value, Is.Not.Null, "O valor retornado pelo controller é nulo.");

        var listaResultados = resultado.Value as List<FreteCalculoDetalhes>;
        Assert.That(listaResultados, Is.Not.Null, "A lista de resultados está nula.");
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
        var listaResultados = resultado.Value as List<FreteCalculoDetalhes>;
    }
}
