using Inteli.Mantainability.Services;

namespace Inteli.Mantainability.Factories
{
    public class TransportadoraBFactory : IFreteServiceFactory
    {
        public IFreteService CriarServicoFrete()
        {
            return new TransportadoraBFreteService();
        }
    }
}
