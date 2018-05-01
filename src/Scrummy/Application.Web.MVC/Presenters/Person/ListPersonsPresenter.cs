﻿using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces;

namespace Scrummy.Application.Web.MVC.Presenters.Person
{
    public class ListPersonsPresenter : BasePresenter
    {
        public ListPersonsPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ListPersonsViewModel Present()
        {
            return new ListPersonsViewModel
            {
                Persons = RepositoryProvider.Person.ListAll().Select(x => new NavigationViewModel
                {
                    Id = x.Id.ToString(),
                    Text = x.Name
                })
            };
        }
    }
}
