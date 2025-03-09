using Inteli.Mantainability.Models;

namespace Inteli.Mantainability.Services
{
    public class TransportadoraBFreteService : IFreteService
    {
        public FreteResult CalcularFrete(decimal distancia, decimal peso, bool entregaExpressa)
        {
            decimal custoBase = distancia * 1.05m;
            decimal custoPeso = peso > 15 ? (peso - 15) * 1.00m : 0;
            decimal custoTotal = custoBase + custoPeso;

            int prazo = distancia switch
            {
                <= 30 => 2,
                <= 100 => 5,
                _ => 7
            };

            if (entregaExpressa)
            {
                custoTotal *= 1.10m;
            }

            return new FreteResult(custoTotal, prazo);
        }
    }

}
