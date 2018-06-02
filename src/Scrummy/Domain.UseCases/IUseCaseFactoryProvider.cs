using Scrummy.Domain.UseCases.Interfaces.Document;
using Scrummy.Domain.UseCases.Interfaces.Meeting;
using Scrummy.Domain.UseCases.Interfaces.Person;
using Scrummy.Domain.UseCases.Interfaces.Project;
using Scrummy.Domain.UseCases.Interfaces.Sprint;
using Scrummy.Domain.UseCases.Interfaces.Team;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases
{
    public interface IUseCaseFactoryProvider
    {
        IPersonUseCaseFactory Person { get; }

        IProjectUseCaseFactory Project { get; }

        ITeamUseCaseFactory Team { get; }

        IMeetingUseCaseFactory Meeting { get; }

        IWorkTaskUseCaseFactory WorkTask { get; }

        ISprintUseCaseFactory Sprint { get; }

        IDocumentUseCaseFactory Document { get; }
    }
}
