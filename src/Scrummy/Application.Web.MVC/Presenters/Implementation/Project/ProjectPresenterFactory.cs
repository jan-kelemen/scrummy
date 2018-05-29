using System;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Project
{
    internal class ProjectPresenterFactory : PresenterFactory, IProjectPresenterFactory
    {
        public ProjectPresenterFactory(IRepositoryProvider repositoryProvider) : base(repositoryProvider)
        {
        }

        public ICreateProjectPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new CreateProjectPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IEditProjectPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new EditProjectPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IListProjectsPresenter List(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ListProjectsPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IManageBacklogPresenter ManageBacklog(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ManageBacklogPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewBacklogPresenter ViewBacklog(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewBacklogPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewMeetingsPresenter ViewMeetings(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewMeetingsPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewSprintsPresenter ViewSprints(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewSprintsPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewProjectPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewProjectPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewTeamHistoryPresenter TeamHistory(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewTeamHistoryPresenter(messageHandler, errorHandler, RepositoryProvider);
    }
}
