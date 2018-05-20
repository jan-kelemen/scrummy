using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class CreateProjectPresenter : BasePresenter
    {
        public CreateProjectPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public CreateProjectViewModel GetInitialViewModel()
        {
            return new CreateProjectViewModel
            {
                DefinitionOfDone = new List<string>(),
                Teams = Teams(),
            };
        }

        public CreateProjectViewModel Present(CreateProjectViewModel vm)
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
