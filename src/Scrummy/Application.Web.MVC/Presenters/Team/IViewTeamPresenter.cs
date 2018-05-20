using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Presenters.Team
{
    public interface IViewTeamPresenter : IPresenter
    {
        ViewTeamViewModel Present(ViewTeamResponse response);
    }
}