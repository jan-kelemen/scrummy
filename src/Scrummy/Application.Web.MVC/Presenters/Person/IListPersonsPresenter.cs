using Scrummy.Application.Web.MVC.ViewModels.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public interface IListPersonsPresenter : IPresenter
    {
        ListPersonsViewModel Present();
    }
}