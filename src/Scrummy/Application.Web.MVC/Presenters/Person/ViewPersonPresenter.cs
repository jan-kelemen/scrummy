using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public class ViewPersonPresenter : Presenter
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
                IsSameAsPersonWhoRequested = response.IsSameAsPersonWhoRequested,
                CurrentTeams = response.CurrentTeams.Select(x =>
                {
                    var p = RepositoryProvider.Team.Read(x);

                    return new NavigationViewModel
                    {
                        Id = p.Id.ToString(),
                        Text = p.Name,
                    };
                }),
            };
        }
    }
}
