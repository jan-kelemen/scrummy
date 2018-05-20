using System;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters
{
    public interface IPresenterFactory
    {
        IPresenter Presenter(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
    }
}
