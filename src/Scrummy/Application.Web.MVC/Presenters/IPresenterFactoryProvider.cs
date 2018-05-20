using Scrummy.Application.Web.MVC.Presenters.Meeting;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Presenters.Team;
using Scrummy.Application.Web.MVC.Presenters.WorkTask;

namespace Scrummy.Application.Web.MVC.Presenters
{
    public interface IPresenterFactoryProvider
    {
        IMeetingPresenterFactory Meeting { get; }
        IPersonPresenterFactory Person { get; }
        IProjectPresenterFactory Project { get; }
        ISprintPresenterFactory Sprint { get; }
        ITeamPresenterFactory Team { get; }
        IWorkTaskPresenterFactory WorkTask { get; }
    }
}
