using Scrummy.Application.Web.MVC.ViewModels.Meeting;

namespace Scrummy.Application.Web.MVC.Presenters.Meeting
{
    public interface IListMeetingsPresenter
    {
        ListMeetingsViewModel Present(string id);
    }
}