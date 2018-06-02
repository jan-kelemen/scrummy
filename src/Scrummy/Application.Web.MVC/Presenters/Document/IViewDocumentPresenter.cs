using Scrummy.Application.Web.MVC.ViewModels.Document;
using Scrummy.Domain.UseCases.Interfaces.Document;

namespace Scrummy.Application.Web.MVC.Presenters.Document
{
    public interface IViewDocumentPresenter : IPresenter
    {
        ViewDocumentViewModel Present(ViewDocumentResponse response);
    }
}