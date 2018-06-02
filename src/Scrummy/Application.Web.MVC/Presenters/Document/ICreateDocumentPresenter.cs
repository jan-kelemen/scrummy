using Scrummy.Application.Web.MVC.ViewModels.Document;

namespace Scrummy.Application.Web.MVC.Presenters.Document
{
    public interface ICreateDocumentPresenter : IPresenter
    {
        CreateDocumentViewModel GetInitialViewModel(string projectId, string type);
        CreateDocumentViewModel Present(CreateDocumentViewModel vm);
    }
}