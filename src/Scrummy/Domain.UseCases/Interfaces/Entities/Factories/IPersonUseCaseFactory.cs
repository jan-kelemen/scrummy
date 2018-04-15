using Scrummy.Domain.UseCases.Interfaces.Entities.Person;

namespace Scrummy.Domain.UseCases.Interfaces.Entities.Factories
{
    public interface IPersonUseCaseFactory
    {
        ICreatePersonUseCase CreatePerson { get; }

        IViewPersonUseCase ViewPerson { get; }
    }
}
