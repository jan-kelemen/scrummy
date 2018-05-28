using System;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Sprint
{
    internal class SprintPresenterFactory : PresenterFactory, ISprintPresenterFactory
    {
        public SprintPresenterFactory(IRepositoryProvider repositoryProvider) : base(repositoryProvider)
        {
        }

        public ICreateSprintPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new CreateSprintPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IEditSprintPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new EditSprintPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewSprintPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewSprintPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IEndSprintPresenter End(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new EndSprintPresenter(messageHandler, errorHandler, RepositoryProvider);
    }
}
