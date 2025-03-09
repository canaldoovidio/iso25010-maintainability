using Inteli.Mantainability.Models;

namespace Inteli.Mantainability.Services
{
    public interface IFreteService
    {
        FreteResult CalcularFrete(decimal distancia, decimal peso, bool entregaExpressa);
    }

}
