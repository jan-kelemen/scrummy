using Scrummy.Domain.Repositories.Interfaces;

namespace Scrummy.Domain.Repositories
{
    public interface IRepositoryProvider
    {
        IPersonRepository Person { get; }

        IProjectRepository Project { get; }

        ITeamRepository Team { get; }

        IMeetingRepository Meeting { get; }

        ISprintRepository Sprint { get; }

        IWorkTaskRepository WorkTask { get; }
    }
}
