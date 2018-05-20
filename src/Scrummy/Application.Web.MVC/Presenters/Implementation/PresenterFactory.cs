using System;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation
{
    public abstract class PresenterFactory : IPresenterFactory
    {
        protected PresenterFactory(IRepositoryProvider repositoryProvider)
        {
            RepositoryProvider = repositoryProvider;
        }

        protected IRepositoryProvider RepositoryProvider { get; }

        public IPresenter Presenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new Presenter(messageHandler, errorHandler, RepositoryProvider);
    }
}
