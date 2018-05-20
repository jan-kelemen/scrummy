using System;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public class ChangePasswordPresenter : BasePresenter
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
