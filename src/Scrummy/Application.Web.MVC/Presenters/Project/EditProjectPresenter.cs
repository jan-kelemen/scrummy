﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class EditProjectPresenter : Presenter
    {
        public EditProjectPresenter(
            Action<MessageType,
            string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EditProjectViewModel GetInitialViewModel(string id)
        {
            var project = RepositoryProvider.Project.Read(Identity.FromString(id));

            return new EditProjectViewModel
            {
                Id = id,
                DefinitionOfDone = new List<string>(project.DefinitionOfDone),
                Name = project.Name,
                SelectedTeamId = project.TeamId.ToString(),
                Teams = Teams()
            };
        }

        public EditProjectViewModel Present(EditProjectViewModel vm)
        {
            vm.Teams = Teams();
            return vm;
        }

        private SelectListItem[] Teams()
        {
            return RepositoryProvider.Team.ListAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToArray();
        }
    }
}
