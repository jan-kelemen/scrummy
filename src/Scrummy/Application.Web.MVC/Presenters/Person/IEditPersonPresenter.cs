﻿using Scrummy.Application.Web.MVC.ViewModels.Person;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public interface IEditPersonPresenter
    {
        EditPersonViewModel GetInitialViewModel(string id);
    }
}