using System;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters.Meeting
{
    public interface IMeetingPresenterFactory : IPresenterFactory
    {
        ICreateMeetingPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IEditMeetingPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IListMeetingsPresenter List(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewMeetingPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
    }
}
