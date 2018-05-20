using Scrummy.Application.Web.MVC.ViewModels.Team;

namespace Scrummy.Application.Web.MVC.Presenters.Team
{
    public interface ICreateTeamPresenter
    {
        CreateTeamViewModel GetInitialViewModel();
        CreateTeamViewModel Present(CreateTeamViewModel vm);
    }
}