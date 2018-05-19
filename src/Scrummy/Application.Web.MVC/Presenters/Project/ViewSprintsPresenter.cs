using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Project
{
    public class ViewSprintsPresenter : BasePresenter
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
            var project = RepositoryProvider.Project.Read(Identity.FromString(id));

            var sprints = RepositoryProvider.Sprint
                .ReadSprints(project.Id, Enum.Parse<SprintStatus>(status))
                .OrderBy(x => x.TimeSpan.Item1);

            return new ViewSprintsViewModel
            {
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                Type = status,
                Sprints = sprints.Select(x => new ViewSprintsViewModel.SprintViewModel
                {
                    Id = x.Id.ToString(),
                    Text = x.Name,
                    StartDate = x.TimeSpan.Item1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EndDate = x.TimeSpan.Item2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                })
            };
        }
    }
}
