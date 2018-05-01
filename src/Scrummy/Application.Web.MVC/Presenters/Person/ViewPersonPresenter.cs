using System;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public class ViewPersonPresenter : BasePresenter
    {
        public ViewPersonPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewPersonViewModel Present(ViewPersonResponse response)
        {
            return new ViewPersonViewModel
            {
                Id = response.Id.ToString(),
                DisplayName = response.DisplayName,
                Email = response.Email,
                FirstName = response.FirstName,
                LastName = response.LastName,
                IsSameAsPersonWhoRequested = response.IsSameAsPersonWhoRequested
            };
        }
    }
}
