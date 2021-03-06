﻿using Scrummy.Application.Web.MVC.ViewModels.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public interface IViewSprintsPresenter : IPresenter
    {
        ViewSprintsViewModel GetInitialViewModel(string id, string status);
    }
}