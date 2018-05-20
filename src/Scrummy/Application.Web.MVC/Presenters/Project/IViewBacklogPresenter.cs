using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public interface IViewBacklogPresenter : IPresenter
    {
        ViewBacklogViewModel Present(ViewBacklogResponse response, ViewBacklogViewModel.BacklogFlavor flavor);
    }
}