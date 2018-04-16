using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Domain.UseCases.Interfaces.Entities.Factories
{
    public interface IPersonUseCaseFactory
    {
        ICreatePersonUseCase Create { get; }

        IViewPersonUseCase View { get; }
    }
}
