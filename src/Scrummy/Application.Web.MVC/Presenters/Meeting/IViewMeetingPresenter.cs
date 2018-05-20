using Scrummy.Application.Web.MVC.ViewModels.Meeting;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Application.Web.MVC.Presenters.Meeting
{
    public interface IViewMeetingPresenter : IPresenter
    {
        ViewMeetingViewModel Present(ViewMeetingResponse response);
    }
}