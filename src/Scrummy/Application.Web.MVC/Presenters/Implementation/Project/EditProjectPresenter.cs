using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Project
{
    internal class EditProjectPresenter : Presenter, IEditProjectPresenter
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
                Description = project.Description,
                SelectedTeamId = project.TeamId.ToPresentationIdentity(),
                Teams = Teams()
            };
        }

        public EditProjectViewModel Present(EditProjectViewModel vm)
        {
            vm.Teams = Teams();
            return vm;
        }

        private SelectListItem[] Teams() => RepositoryProvider.Team.ListAll().Select(x => x.ToSelectListItem()).ToArray();
    }
}
