using Scrummy.Application.Web.MVC.ViewModels.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public interface IEditSprintPresenter
    {
        EditSprintViewModel GetInitialViewModel(string sprintId);
        EditSprintViewModel Present(EditSprintViewModel vm);
    }
}