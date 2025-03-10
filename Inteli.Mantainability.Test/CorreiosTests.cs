using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Inteli.Mantainability.Services;

namespace Inteli.Mantainability.Test
{
    [TestFixture]
    public class CorreiosTests
    {
        [Test]
        public void Correios_DeveCalcularFreteCorretamente()
        {
            var service = new CorreiosFreteService();
            var resultado = service.CalcularFrete(100, 5, false);

            Assert.That(resultado.Valor, Is.EqualTo(120)); // 100 * 1.20
            Assert.That(resultado.PrazoEmDias, Is.EqualTo(5));
        }

        [Test]
        public void Correios_DeveAdicionarCustoPesoAcimaDe10kg()
        {
            var service = new CorreiosFreteService();
            var resultado = service.CalcularFrete(100, 15, false);

            Assert.That(resultado.Valor, Is.EqualTo(122.5m)); // (100 * 1.20) + (5 * 0.50)
            Assert.That(resultado.PrazoEmDias, Is.EqualTo(5));
        }

        [Test]
        public void Correios_DeveReduzirPrazoParaEntregaExpressa()
        {
            var service = new CorreiosFreteService();
            var resultado = service.CalcularFrete(100, 5, true);

            Assert.That(resultado.Valor, Is.EqualTo(144)); // (100 * 1.20) * 1.2
            Assert.That(resultado.PrazoEmDias, Is.EqualTo(3)); // 5 - 2
        }

        [Test]
        public void Correios_DeveAjustarPrazoPorDistancia()
        {
            var service = new CorreiosFreteService();
            
            var resultadoCurto = service.CalcularFrete(50, 5, false);
            Assert.That(resultadoCurto.PrazoEmDias, Is.EqualTo(2));

            var resultadoMedio = service.CalcularFrete(200, 5, false);
            Assert.That(resultadoMedio.PrazoEmDias, Is.EqualTo(5));

            var resultadoLongo = service.CalcularFrete(300, 5, false);
            Assert.That(resultadoLongo.PrazoEmDias, Is.EqualTo(10));
        }
    }
}
