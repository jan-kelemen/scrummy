using Scrummy.Application.Web.MVC.ViewModels.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public interface IEndSprintPresenter : IPresenter
    {
        EndSprintViewModel GetInitialViewModel(string sprintId);
        EndSprintViewModel Present(EndSprintViewModel vm);
    }
}
