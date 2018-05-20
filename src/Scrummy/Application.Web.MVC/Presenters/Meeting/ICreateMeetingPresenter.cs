using Scrummy.Application.Web.MVC.ViewModels.Meeting;

namespace Scrummy.Application.Web.MVC.Presenters.Meeting
{
    public interface ICreateMeetingPresenter : IPresenter
    {
        CreateMeetingViewModel GetInitialViewModel(string projectId, string personId);
        CreateMeetingViewModel Present(CreateMeetingViewModel vm);
    }
}