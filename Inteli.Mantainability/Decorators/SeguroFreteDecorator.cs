using Inteli.Mantainability.Models;
using Inteli.Mantainability.Services;

namespace Inteli.Mantainability.Decorators
{
    public class SeguroFreteDecorator : IFreteService
    {
        private readonly IFreteService _freteService;
        private readonly decimal _valorCompra;

        public SeguroFreteDecorator(IFreteService freteService, decimal valorCompra)
        {
            _freteService = freteService;
            _valorCompra = valorCompra;
        }

        public FreteResult CalcularFrete(decimal distancia, decimal peso, bool entregaExpressa)
        {
            var resultado = _freteService.CalcularFrete(distancia, peso, entregaExpressa);

            // Aplica seguro apenas para compras acima de R$ 500
            if (_valorCompra > 500)
            {
                decimal custoComSeguro = resultado.Valor + 20; // Adiciona taxa de seguro fixa de R$ 20
                return new FreteResult(custoComSeguro, resultado.PrazoEmDias);
            }

            return resultado;
        }
    }

}
