using Scrummy.Application.Web.MVC.ViewModels.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public interface IChangePasswordPresenter
    {
        ChangePasswordViewModel GetInitialViewModel(string id);
    }
}