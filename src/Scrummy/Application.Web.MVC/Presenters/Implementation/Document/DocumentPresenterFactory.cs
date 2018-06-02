using System;
using Scrummy.Application.Web.MVC.Presenters.Document;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Document
{
    internal class DocumentPresenterFactory : PresenterFactory, IDocumentPresenterFactory
    {
        public DocumentPresenterFactory(IRepositoryProvider repositoryProvider) : base(repositoryProvider)
        {
        }

        public ICreateDocumentPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new CreateDocumentPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IEditDocumentPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new EditDocumentPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewDocumentPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewDocumentPresenter(messageHandler, errorHandler, RepositoryProvider);
    }
}
