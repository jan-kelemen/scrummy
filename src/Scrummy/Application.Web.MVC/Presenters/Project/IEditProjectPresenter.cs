using Scrummy.Application.Web.MVC.ViewModels.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public interface IEditProjectPresenter : IPresenter
    {
        EditProjectViewModel GetInitialViewModel(string id);
        EditProjectViewModel Present(EditProjectViewModel vm);
    }
}