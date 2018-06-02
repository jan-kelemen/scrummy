using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public interface ISprintReportPresenter : IPresenter
    {
        SprintReportViewModel Present(SprintReportResponse response);
    }
}
