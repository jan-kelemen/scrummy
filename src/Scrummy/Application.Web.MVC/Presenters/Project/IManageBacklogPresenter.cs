using Scrummy.Application.Web.MVC.ViewModels.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public interface IManageBacklogPresenter
    {
        ManageBacklogViewModel GetInitialViewModel(string id);
    }
}