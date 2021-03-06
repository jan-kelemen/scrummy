﻿using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Sprint
{
    internal class CreateSprintPresenter : Presenter, ICreateSprintPresenter
    {
        public CreateSprintPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public CreateSprintViewModel GetInitialViewModel(string projectId)
        {
            var id = Identity.FromString(projectId);
            var project = RepositoryProvider.Project.Read(id);

            return new CreateSprintViewModel
            {
                Project = project.ToViewModel(),
                StartDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Stories = Stories(id),
                Documents = Documents(id),
            };
        }

        public CreateSprintViewModel Present(CreateSprintViewModel vm)
        {
            vm.Stories = Stories(Identity.FromString(vm.Project.Id));
            vm.Documents = Documents(Identity.FromString(vm.Project.Id));
            return vm;
        }

        private SelectListItem[] Stories(Identity projectId)
        {
            var backlog = RepositoryProvider.Project.ReadProductBacklog(projectId);
            var readyStories = backlog
                .Where(x => x.Status == ProductBacklog.WorkTaskStatus.Ready)
                .Where(x => RepositoryProvider.WorkTask.Read(x.WorkTaskId).Type == WorkTaskType.UserStory)
                .Select(x => RepositoryProvider.WorkTask.Read(x.WorkTaskId).ToSelectListItem());
            return readyStories.ToArray();
        }

        private SelectListItem[] Documents(Identity projectId)
        {
            var common = RepositoryProvider.Document.ListByKind(projectId, DocumentKind.Common);
            var sprint = RepositoryProvider.Document.ListByKind(projectId, DocumentKind.Sprint);

            return sprint.Concat(common).Select(x => x.ToSelectListItem()).ToArray();
        }
    }
}
