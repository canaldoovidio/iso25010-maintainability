using Inteli.Mantainability.Controllers;
using Inteli.Mantainability.Factories;
using Inteli.Mantainability.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inteli.Mantainability.Test
{
    [TestFixture]
    public class CalculadoraDeFreteTests
    {
        [Test]
        public void Calculadora_DeveRetornarFreteParaTodasAsTransportadoras()
        {
            var factories = new List<IFreteServiceFactory>
        {
            new TransportadoraAFactory(),
            new TransportadoraBFactory(),
            new CorreiosFactory()
        };

            var calculadora = new FreteController(factories);

            var request = new FreteRequest
            {
                Distancia = 150,
                Peso = 25,
                EntregaExpressa = false,
                ValorCompra = 600
            };

            var resultado = (OkObjectResult)calculadora.CalcularFrete(request);
            var listaResultados = (List<object>)resultado.Value;

            Assert.That(listaResultados.Count, Is.EqualTo(3));
        }
    }
}
