using Scrummy.Application.Web.MVC.ViewModels.Team;

namespace Scrummy.Application.Web.MVC.Presenters.Team
{
    public interface IEditTeamPresenter : IPresenter
    {
        EditTeamViewModel GetInitialViewModel(string id);
        EditTeamViewModel Present(EditTeamViewModel vm);
    }
}