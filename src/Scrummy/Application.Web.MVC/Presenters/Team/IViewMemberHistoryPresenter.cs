using Scrummy.Application.Web.MVC.ViewModels.Team;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Application.Web.MVC.Presenters.Team
{
    public interface IViewMemberHistoryPresenter : IPresenter
    {
        ViewMemberHistoryViewModel Present(ViewMemberHistoryResponse response);
    }
}