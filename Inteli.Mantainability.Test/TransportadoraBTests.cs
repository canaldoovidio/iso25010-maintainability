using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inteli.Mantainability.Decorators;
using Inteli.Mantainability.Services;

namespace Inteli.Mantainability.Test
{
    [TestFixture]
    public class TransportadoraBTests
    {
        [Test]
        public void TransportadoraB_DeveCalcularFreteCorretamente()
        {
            var service = new TransportadoraBFreteService();
            var resultado = service.CalcularFrete(200, 20, false);

            Assert.That(resultado.Valor, Is.EqualTo(220)); // 200 * 1.05 + 5
            Assert.That(resultado.PrazoEmDias, Is.EqualTo(5));
        }

        [Test]
        public void TransportadoraB_DeveAplicarSeguroParaComprasAcimaDe500()
        {
            var service = new TransportadoraBFreteService();
            var decorador = new SeguroFreteDecorator(service, 600); // Compra acima de R$ 500

            var resultado = decorador.CalcularFrete(200, 20, false);

            Assert.That(resultado.Valor, Is.EqualTo(240)); // 220 + 20 de seguro
            Assert.That(resultado.PrazoEmDias, Is.EqualTo(5));
        }

        [Test]
        public void TransportadoraB_NaoDeveAplicarSeguroParaComprasAbaixoDe500()
        {
            var service = new TransportadoraBFreteService();
            var decorador = new SeguroFreteDecorator(service, 400); // Compra abaixo de R$ 500

            var resultado = decorador.CalcularFrete(200, 20, false);

            Assert.That(resultado.Valor, Is.EqualTo(220)); // Sem seguro adicional
            Assert.That(resultado.PrazoEmDias, Is.EqualTo(5));
        }
    }

}
