using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Interfaces.Factories
{
    public interface IPersonUseCaseFactory
    {
        ICreatePersonUseCase Create { get; }

        IViewPersonUseCase View { get; }

        IEditPersonUseCase Edit { get; }

        IViewCurrentWorkUseCase ViewCurrentWork { get; }
    }
}
