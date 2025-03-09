using Inteli.Mantainability.Models;

namespace Inteli.Mantainability.Services
{
    public class CorreiosFreteService : IFreteService
    {
        public FreteResult CalcularFrete(decimal distancia, decimal peso, bool entregaExpressa)
        {
            decimal custoBase = distancia * 1.20m;
            decimal custoPeso = peso > 10 ? (peso - 10) * 0.50m : 0;
            decimal custoTotal = custoBase + custoPeso;

            int prazo = distancia switch
            {
                <= 50 => 2,
                <= 200 => 5,
                _ => 10
            };

            if (entregaExpressa)
            {
                custoTotal *= 1.2m;
                prazo = Math.Max(1, prazo - 2);
            }

            return new FreteResult(custoTotal, prazo);
        }
    }
}
