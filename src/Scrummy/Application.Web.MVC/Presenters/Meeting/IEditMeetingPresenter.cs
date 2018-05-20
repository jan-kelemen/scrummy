using Scrummy.Application.Web.MVC.ViewModels.Meeting;

namespace Scrummy.Application.Web.MVC.Presenters.Meeting
{
    public interface IEditMeetingPresenter : IPresenter
    {
        EditMeetingViewModel GetInitialViewModel(string id);
        EditMeetingViewModel Present(EditMeetingViewModel vm);
    }
}