using Inteli.Mantainability.Decorators;
using Inteli.Mantainability.Factories;
using Inteli.Mantainability.Models;
using Inteli.Mantainability.Services;

using Microsoft.AspNetCore.Mvc;

namespace Inteli.Mantainability.Controllers
{
    [ApiController]
    [Route("api/frete")]
    public class FreteController : ControllerBase
    {
        private readonly List<IFreteServiceFactory> _factories;

        public FreteController(IEnumerable<IFreteServiceFactory> factories)
        {
            _factories = factories.ToList();
        }

        [HttpPost("calcular")]
        public IActionResult CalcularFrete([FromBody] FreteRequest request)
        {
            var resultados = new List<FreteCalculoDetalhes>();

            foreach (var factory in _factories)
            {
                var freteService = factory.CriarServicoFrete();

                // Aplica regras específicas para cada transportadora
                if (freteService is TransportadoraAFreteService)
                {
                    freteService = new FreteGratisDecorator(freteService, request.ValorCompra);
                }
                else if (freteService is TransportadoraBFreteService)
                {
                    freteService = new SeguroFreteDecorator(freteService, request.ValorCompra);
                }

                var resultado = freteService.CalcularFrete(request.Distancia, request.Peso, request.EntregaExpressa);

                resultados.Add(new FreteCalculoDetalhes(
                    factory.GetType().Name.Replace("Factory", ""),
                    resultado.Valor,
                    resultado.PrazoEmDias
                ));
            }

            return Ok(resultados);
        }
    }

    public record FreteCalculoDetalhes(string Transportadora, decimal Valor, int PrazoEmDias);
}
