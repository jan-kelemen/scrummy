using Scrummy.Application.Web.MVC.ViewModels.Document;

namespace Scrummy.Application.Web.MVC.Presenters.Document
{
    public interface IEditDocumentPresenter : IPresenter
    {
        EditDocumentViewModel GetInitialViewModel(string id);
    }
}