using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Project
{
    internal class ViewSprintsPresenter : Presenter, IViewSprintsPresenter
    {
        public ViewSprintsPresenter(
            Action<MessageType, 
            string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewSprintsViewModel GetInitialViewModel(string id, string status)
        {
            var num = Enum.Parse<SprintStatus>(status);

            var project = RepositoryProvider.Project.Read(Identity.FromString(id));
            var planned = num == SprintStatus.Planned;

            var sprints = RepositoryProvider.Sprint
                .ReadSprints(project.Id, num)
                .OrderBy(x => x.TimeSpan.Item1);

            return new ViewSprintsViewModel
            {
                Project = project.ToViewModel(),
                Type = planned ? "Planned" : "Completed",
                StartSprintAllowed = planned && RepositoryProvider.Sprint.ReadCurrentSprint(project.Id) == null,
                Sprints = sprints.Select(x => new ViewSprintsViewModel.SprintViewModel
                {
                    Id = x.Id.ToPresentationIdentity(),
                    Text = x.Name,
                    StartDate = x.TimeSpan.Item1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EndDate = x.TimeSpan.Item2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                })
            };
        }
    }
}
