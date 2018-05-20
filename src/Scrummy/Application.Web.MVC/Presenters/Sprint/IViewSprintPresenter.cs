using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public interface IViewSprintPresenter : IPresenter
    {
        ViewSprintViewModel Present(ViewSprintResponse response);
    }
}