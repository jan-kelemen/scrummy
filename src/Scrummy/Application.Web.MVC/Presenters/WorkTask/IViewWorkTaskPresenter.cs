using Scrummy.Application.Web.MVC.ViewModels.WorkTask;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Application.Web.MVC.Presenters.WorkTask
{
    public interface IViewWorkTaskPresenter : IPresenter
    {
        ViewWorkTaskViewModel Present(ViewWorkTaskResponse response);
    }
}