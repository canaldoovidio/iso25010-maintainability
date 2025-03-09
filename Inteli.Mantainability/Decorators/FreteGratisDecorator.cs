using Inteli.Mantainability.Models;
using Inteli.Mantainability.Services;

namespace Inteli.Mantainability.Decorators
{
    public class FreteGratisDecorator : IFreteService
    {
        private readonly IFreteService _freteService;
        private readonly decimal _valorCompra;

        public FreteGratisDecorator(IFreteService freteService, decimal valorCompra)
        {
            _freteService = freteService;
            _valorCompra = valorCompra;
        }

        public FreteResult CalcularFrete(decimal distancia, decimal peso, bool entregaExpressa)
        {
            // Somente aplica frete grátis para compras acima de R$ 300
            if (_valorCompra >= 300)
            {
                return new FreteResult(0, _freteService.CalcularFrete(distancia, peso, entregaExpressa).PrazoEmDias);
            }

            return _freteService.CalcularFrete(distancia, peso, entregaExpressa);
        }
    }

}
