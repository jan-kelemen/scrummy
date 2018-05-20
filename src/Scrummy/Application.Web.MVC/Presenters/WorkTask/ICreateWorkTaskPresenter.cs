using Scrummy.Application.Web.MVC.ViewModels.WorkTask;

namespace Scrummy.Application.Web.MVC.Presenters.WorkTask
{
    public interface ICreateWorkTaskPresenter : IPresenter
    {
        CreateWorkTaskViewModel GetInitialViewModel(string projectId, string type, string parent = null, string child = null);
        CreateWorkTaskViewModel Present(CreateWorkTaskViewModel vm);
    }
}