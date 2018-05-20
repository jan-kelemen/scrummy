namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public interface ISprintUseCaseFactory
    {
        ICreateSprintUseCase Create { get; }

        IEditSprintUseCase Edit { get; }

        IViewSprintUseCase View { get; }

        IDeleteSprintUseCase Delete { get; }

        IStartSprintUseCase Start { get; }

        IEndSprintUseCase End { get; }

        IChangeTaskStatusUseCase ChangeTaskStatus { get; }
    }
}
