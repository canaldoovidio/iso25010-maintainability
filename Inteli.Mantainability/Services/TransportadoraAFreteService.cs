using Inteli.Mantainability.Models;

namespace Inteli.Mantainability.Services
{
    public class TransportadoraAFreteService : IFreteService
    {
        public FreteResult CalcularFrete(decimal distancia, decimal peso, bool entregaExpressa)
        {
            decimal custoBase = distancia * 1.10m + 5;
            decimal custoPeso = peso > 20 ? (peso - 20) * 0.75m : 0;
            decimal custoTotal = custoBase + custoPeso;

            int prazo = distancia switch
            {
                <= 50 => 1,
                <= 150 => 4,
                _ => 8
            };

            if (entregaExpressa)
            {
                custoTotal *= 1.15m;
                prazo = Math.Max(1, prazo - 1);
            }

            return new FreteResult(custoTotal, prazo);
        }
    }

}
