using System;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Person
{
    internal class ChangePasswordPresenter : Presenter, IChangePasswordPresenter
    {
        public ChangePasswordPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ChangePasswordViewModel GetInitialViewModel(string id)
        {
            return new ChangePasswordViewModel
            {
                Id = id
            };
        }
    }
}
