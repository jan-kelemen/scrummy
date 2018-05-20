using Scrummy.Application.Web.MVC.Presenters.Implementation.Meeting;
using Scrummy.Application.Web.MVC.Presenters.Implementation.Person;
using Scrummy.Application.Web.MVC.Presenters.Implementation.Project;
using Scrummy.Application.Web.MVC.Presenters.Implementation.Sprint;
using Scrummy.Application.Web.MVC.Presenters.Implementation.Team;
using Scrummy.Application.Web.MVC.Presenters.Implementation.WorkTask;
using Scrummy.Application.Web.MVC.Presenters.Meeting;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Presenters.WorkTask;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation
{
    internal class PresenterFactoryProvider : IPresenterFactoryProvider
    {
        public PresenterFactoryProvider(IRepositoryProvider repositoryProvider)
        {
            Meeting = new MeetingPresenterFactory(repositoryProvider);
            Person = new PersonPresenterFactory(repositoryProvider);
            Project = new ProjectPresenterFactory(repositoryProvider);
            Sprint = new SprintPresenterFactory(repositoryProvider);
            Team = new TeamPresenterFactory(repositoryProvider);
            WorkTask = new WorkTaskPresenterFactory(repositoryProvider);
        }

        public IMeetingPresenterFactory Meeting { get; }
        public IPersonPresenterFactory Person { get; }
        public IProjectPresenterFactory Project { get; }
        public ISprintPresenterFactory Sprint { get; }
        public ITeamPresenterFactory Team { get; }
        public IWorkTaskPresenterFactory WorkTask { get; }
    }
}
