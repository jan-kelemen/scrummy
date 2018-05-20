using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public interface IViewCurrentWorkPresenter : IPresenter
    {
        ViewCurrentWorkViewModel Present(ViewCurrentWorkResponse response);
    }
}