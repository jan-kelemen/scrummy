using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Presenters.Team
{
    public interface IViewProjectHistoryPresenter : IPresenter
    {
        ViewProjectHistoryViewModel Present(ViewProjectHistoryResponse response);
    }
}