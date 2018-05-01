using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Interfaces.Factories
{
    public interface IPersonUseCaseFactory
    {
        ICreatePersonUseCase Create { get; }

        IViewPersonUseCase View { get; }

        IViewCurrentWorkUseCase ViewCurrentWork { get; }
    }
}
