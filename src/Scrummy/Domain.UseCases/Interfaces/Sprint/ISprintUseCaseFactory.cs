namespace Scrummy.Domain.UseCases.Interfaces.Sprint
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
