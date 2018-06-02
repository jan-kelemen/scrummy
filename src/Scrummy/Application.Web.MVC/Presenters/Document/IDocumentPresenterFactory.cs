using System;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters.Document
{
    public interface IDocumentPresenterFactory : IPresenterFactory
    {
        ICreateDocumentPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IEditDocumentPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewDocumentPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
    }
}
