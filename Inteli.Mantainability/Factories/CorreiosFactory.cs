using Inteli.Mantainability.Services;

namespace Inteli.Mantainability.Factories
{
    public class CorreiosFactory : IFreteServiceFactory
    {
        public IFreteService CriarServicoFrete()
        {
            return new CorreiosFreteService();
        }
    }
}
