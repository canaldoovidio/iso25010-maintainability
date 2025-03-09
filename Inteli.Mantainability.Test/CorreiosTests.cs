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
            var resultado = service.CalcularFrete(150, 12, false);

            Assert.That(resultado.Valor, Is.EqualTo(186)); // 150 * 1.20 + 1.00
            Assert.That(resultado.PrazoEmDias, Is.EqualTo(5));
        }

        [Test]
        public void Correios_DeveReduzirPrazoParaEntregaExpressa()
        {
            var service = new CorreiosFreteService();
            var resultado = service.CalcularFrete(150, 12, true);

            Assert.That(resultado.Valor, Is.EqualTo(223.2)); // 186 * 1.2
            Assert.That(resultado.PrazoEmDias, Is.EqualTo(3)); // Redução de 2 dias
        }
    }

}
