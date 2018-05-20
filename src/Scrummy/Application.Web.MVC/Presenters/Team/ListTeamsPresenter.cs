using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Team
{
    public class ListTeamsPresenter : Presenter
    {
        public ListTeamsPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ListTeamsViewModel Present()
        {
            return new ListTeamsViewModel
            {
                Teams = RepositoryProvider.Team.ListAll().Select(x => new NavigationViewModel
                {
                    Id = x.Id.ToString(),
                    Text = x.Name
                })
            };
        }
    }
}
