using Scrummy.Application.Web.MVC.ViewModels.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public enum Flavor
    {
        Project, WorkTask, Meeting, Sprint, Common, All
    }

    public interface IViewDocumentsPresenter
    {
        ViewDocumentsViewModel Present(string projectId, string flavor);
    }
}
