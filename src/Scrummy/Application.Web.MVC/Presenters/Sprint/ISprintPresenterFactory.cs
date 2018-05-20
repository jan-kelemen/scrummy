﻿using System;
using Scrummy.Application.Web.MVC.Utility;

namespace Scrummy.Application.Web.MVC.Presenters.Sprint
{
    public interface ISprintPresenterFactory : IPresenterFactory
    {
        ICreateSprintPresenter Create(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IEditSprintPresenter Edit(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
        IViewSprintPresenter View(Action<MessageType, string> messageHandler, Action<string, string> errorHandler);
    }
}
