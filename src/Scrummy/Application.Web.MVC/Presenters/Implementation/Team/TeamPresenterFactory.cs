using System;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Team
{
    internal class TeamPresenterFactory : PresenterFactory, ITeamPresenterFactory
    {
        public TeamPresenterFactory(IRepositoryProvider repositoryProvider) : base(repositoryProvider)
        {
        }

        public ICreateTeamPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new CreateTeamPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IEditTeamPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new EditTeamPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IListTeamsPresenter List(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ListTeamsPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewTeamPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewTeamPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewProjectHistoryPresenter ProjectHistory(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewProjectHistoryPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewMemberHistoryPresenter MemberHistory(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewMemberHistoryPresenter(messageHandler, errorHandler, RepositoryProvider);
    }
}
