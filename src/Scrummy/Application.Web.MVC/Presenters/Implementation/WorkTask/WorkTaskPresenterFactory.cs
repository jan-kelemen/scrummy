using System;
using Scrummy.Application.Web.MVC.Presenters.WorkTask;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.WorkTask
{
    internal class WorkTaskPresenterFactory : PresenterFactory, IWorkTaskPresenterFactory
    {
        public WorkTaskPresenterFactory(IRepositoryProvider repositoryProvider) : base(repositoryProvider)
        {
        }

        public ICreateWorkTaskPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new CreateWorkTaskPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IEditWorkTaskPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new EditWorkTaskPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewWorkTaskPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewWorkTaskPresenter(messageHandler, errorHandler, RepositoryProvider);
    }
}
