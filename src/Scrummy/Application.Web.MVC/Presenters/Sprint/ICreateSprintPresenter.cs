using Scrummy.Application.Web.MVC.ViewModels.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public interface ICreateSprintPresenter
    {
        CreateSprintViewModel GetInitialViewModel(string projectId);
        CreateSprintViewModel Present(CreateSprintViewModel vm);
    }
}