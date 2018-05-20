using Scrummy.Application.Web.MVC.ViewModels.WorkTask;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Application.Web.MVC.Presenters.WorkTask
{
    public interface IViewWorkTaskPresenter
    {
        ViewWorkTaskViewModel Present(ViewWorkTaskResponse response);
    }
}