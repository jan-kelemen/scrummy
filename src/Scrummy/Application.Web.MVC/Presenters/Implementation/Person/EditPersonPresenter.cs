﻿using System;
using Scrummy.Application.Web.MVC.Presenters.Person;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Person;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Person
{
    internal class EditPersonPresenter : Presenter, IEditPersonPresenter
    {
        public EditPersonPresenter(
                        Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EditPersonViewModel GetInitialViewModel(string id)
        {
            var person = RepositoryProvider.Person.Read(Identity.FromString(id));

            return new EditPersonViewModel
            {
                Id = id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DisplayName = person.DisplayName,
                Email = person.Email,
            };
        }
    }
}
