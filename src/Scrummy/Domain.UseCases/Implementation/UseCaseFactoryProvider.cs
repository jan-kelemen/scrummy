using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Meeting;
using Scrummy.Domain.UseCases.Implementation.Person;
using Scrummy.Domain.UseCases.Implementation.Project;
using Scrummy.Domain.UseCases.Implementation.Sprint;
using Scrummy.Domain.UseCases.Implementation.Team;
using Scrummy.Domain.UseCases.Implementation.WorkTask;
using Scrummy.Domain.UseCases.Interfaces.Meeting;
using Scrummy.Domain.UseCases.Interfaces.Person;
using Scrummy.Domain.UseCases.Interfaces.Project;
using Scrummy.Domain.UseCases.Interfaces.Sprint;
using Scrummy.Domain.UseCases.Interfaces.Team;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases.Implementation
{
    internal class UseCaseFactoryProvider : IUseCaseFactoryProvider
    {
        public UseCaseFactoryProvider(IRepositoryProvider repositoryProvider)
        {
            Person = new PersonUseCaseFactory(repositoryProvider);
            Project = new ProjectUseCaseFactory(repositoryProvider);
            Team = new TeamUseCaseFactory(repositoryProvider);
            Meeting = new MeetingUseCaseFactory(repositoryProvider);
            WorkTask = new WorkTaskUseCaseFactory(repositoryProvider);
            Sprint = new SprintUseCaseFactory(repositoryProvider);
        }

        public IPersonUseCaseFactory Person { get; }
        public IProjectUseCaseFactory Project { get; }
        public ITeamUseCaseFactory Team { get; }
        public IMeetingUseCaseFactory Meeting { get; }
        public IWorkTaskUseCaseFactory WorkTask { get; }
        public ISprintUseCaseFactory Sprint { get; }
    }
}
