namespace Scrummy.Domain.UseCases.Interfaces.Project
{
    public interface IProjectUseCaseFactory
    {
        ICreateProjectUseCase Create { get; }

        IEditProjectUseCase Edit { get; }

        IViewProjectUseCase View { get; }

        IDeleteProjectUseCase Delete { get; }

        IViewMeetingsUseCase ViewMeetings { get; }

        IViewBacklogUseCase ViewBacklog { get; }

        IManageBacklogUseCase ManageBacklog { get; }
        
        IViewTeamHistoryUseCase TeamHistory { get; }
    }
}
