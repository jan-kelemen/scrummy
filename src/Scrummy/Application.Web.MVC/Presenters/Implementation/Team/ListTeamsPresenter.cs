using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Team
{
    internal class ListTeamsPresenter : Presenter, IListTeamsPresenter
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
                Teams = RepositoryProvider.Team.ListAll().Select(x => x.ToViewModel()),
            };
        }
    }
}
