using System;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public interface IProjectPresenterFactory : IPresenterFactory
    {
        ICreateProjectPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IEditProjectPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IListProjectsPresenter List(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IManageBacklogPresenter ManageBacklog(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewBacklogPresenter ViewBacklog(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewMeetingsPresenter ViewMeetings(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewSprintsPresenter ViewSprints(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewProjectPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);

    }
}
