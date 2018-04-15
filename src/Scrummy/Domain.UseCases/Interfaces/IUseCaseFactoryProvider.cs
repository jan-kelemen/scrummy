using Scrummy.Domain.UseCases.Interfaces.Entities.Factories;

namespace Scrummy.Domain.UseCases.Interfaces
{
    public interface IUseCaseFactoryProvider
    {
        IPersonUseCaseFactory PersonUseCaseFactory { get; }
    }
}
