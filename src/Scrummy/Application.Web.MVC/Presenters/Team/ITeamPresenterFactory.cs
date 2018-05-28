using System;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters.Team
{
    public interface ITeamPresenterFactory : IPresenterFactory
    {
        ICreateTeamPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IEditTeamPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IListTeamsPresenter List(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewTeamPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewProjectHistoryPresenter ProjectHistory(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewMemberHistoryPresenter MemberHistory(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
    }
}
