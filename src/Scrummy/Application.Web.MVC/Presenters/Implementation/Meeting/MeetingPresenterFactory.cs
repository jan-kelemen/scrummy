using System;
using Scrummy.Application.Web.MVC.Presenters.Meeting;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Meeting
{
    internal class MeetingPresenterFactory : PresenterFactory, IMeetingPresenterFactory
    {
        public MeetingPresenterFactory(IRepositoryProvider repositoryProvider) : base(repositoryProvider)
        {
        }

        public ICreateMeetingPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new CreateMeetingPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IEditMeetingPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new EditMeetingPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IListMeetingsPresenter List(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ListMeetingsPresenter(messageHandler, errorHandler, RepositoryProvider);

        public IViewMeetingPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler)
            => new ViewMeetingPresenter(messageHandler, errorHandler, RepositoryProvider);
    }
}
