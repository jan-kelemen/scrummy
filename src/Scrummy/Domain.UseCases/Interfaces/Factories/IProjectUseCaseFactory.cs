using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Interfaces.Factories
{
    public interface IProjectUseCaseFactory
    {
        ICreateProjectUseCase Create { get; }

        IEditProjectUseCase Edit { get; }

        IViewProjectUseCase View { get; }

        IViewMeetingsUseCase ViewMeetings { get; }

        IViewBacklogUseCase ViewBacklog { get; }

        IManageBacklogUseCase ManageBacklog { get; }

        IPlanBacklogUseCase PlanBacklog { get; }
    }
}
