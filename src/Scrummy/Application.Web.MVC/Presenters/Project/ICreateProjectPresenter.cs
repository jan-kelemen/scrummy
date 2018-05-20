using Scrummy.Application.Web.MVC.ViewModels.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public interface ICreateProjectPresenter : IPresenter
    {
        CreateProjectViewModel GetInitialViewModel();
        CreateProjectViewModel Present(CreateProjectViewModel vm);
    }
}