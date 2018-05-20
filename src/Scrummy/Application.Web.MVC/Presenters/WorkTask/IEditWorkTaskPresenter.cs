using Scrummy.Application.Web.MVC.ViewModels.WorkTask;

namespace Scrummy.Application.Web.MVC.Presenters.WorkTask
{
    public interface IEditWorkTaskPresenter
    {
        EditWorkTaskViewModel GetInitialViewModel(string id);
        EditWorkTaskViewModel Present(EditWorkTaskViewModel vm);
    }
}