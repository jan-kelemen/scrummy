using Scrummy.Domain.UseCases.Interfaces.Entities.Factories;

namespace Scrummy.Domain.UseCases
{
    public interface IUseCaseFactoryProvider
    {
        IPersonUseCaseFactory Person { get; }
    }
}
