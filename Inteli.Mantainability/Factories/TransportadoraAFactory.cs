using Inteli.Mantainability.Services;

namespace Inteli.Mantainability.Factories
{
    public class TransportadoraAFactory : IFreteServiceFactory
    {
        public IFreteService CriarServicoFrete()
        {
            return new TransportadoraAFreteService();
        }
    }

}
