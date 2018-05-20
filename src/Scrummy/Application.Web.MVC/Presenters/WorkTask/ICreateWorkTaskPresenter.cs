using Scrummy.Application.Web.MVC.ViewModels.WorkTask;

namespace Scrummy.Application.Web.MVC.Presenters.WorkTask
{
    public interface ICreateWorkTaskPresenter
    {
        CreateWorkTaskViewModel GetInitialViewModel(string projectId, string type, string parent = null, string child = null);
        CreateWorkTaskViewModel Present(CreateWorkTaskViewModel vm);
    }
}