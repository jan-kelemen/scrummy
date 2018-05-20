using System;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public interface IPersonPresenterFactory : IPresenterFactory
    {
        IChangePasswordPresenter ChangePassword(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IEditPersonPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IListPersonsPresenter List(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewCurrentWorkPresenter ViewCurrentWork(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewPersonPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
    }
}
