using System;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Person
{
    internal class PersonPresenterFactory : PresenterFactory, IPersonPresenterFactory
    {
        public PersonPresenterFactory(IRepositoryProvider repositoryProvider) : base(repositoryProvider)
        {
        }

        public IChangePasswordPresenter ChangePassword(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ChangePasswordPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IEditPersonPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new EditPersonPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IListPersonsPresenter List(Action<MessageType, string> messageHandler, Action<string, string> errorHandler) 
            => new ListPersonsPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewCurrentWorkPresenter ViewCurrentWork(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewCurrentWorkPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewPersonPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewPersonPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewTeamHistoryPresenter TeamHistory(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewTeamHistoryPresenter(messageHandler, errorHandler, RepositoryProvider);
    }
}
