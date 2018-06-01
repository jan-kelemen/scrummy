using System;
using Scrummy.Application.Web.MVC.Presenters.WorkTask.Comment;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters.WorkTask
{
    public interface IWorkTaskPresenterFactory : IPresenterFactory
    {
        ICreateWorkTaskPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IEditWorkTaskPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewWorkTaskPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IEditCommentPresenter EditComment(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
    }
}
