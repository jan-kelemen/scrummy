using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Domain.UseCases.Interfaces.Factories
{
    public interface ISprintUseCaseFactory
    {
        ICreateSprintUseCase Create { get; }

        IEditSprintUseCase Edit { get; }

        IViewSprintUseCase View { get; }

        IStartSprintUseCase Start { get; }

        IEndSprintUseCase End { get; }

        IChangeTaskStatusUseCase ChangeTaskStatus { get; }
    }
}
