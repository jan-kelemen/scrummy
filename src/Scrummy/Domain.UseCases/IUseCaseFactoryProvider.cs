using Scrummy.Domain.UseCases.Interfaces.Factories;

namespace Scrummy.Domain.UseCases
{
    public interface IUseCaseFactoryProvider
    {
        IPersonUseCaseFactory Person { get; }

        IProjectUseCaseFactory Project { get; }
    }
}
