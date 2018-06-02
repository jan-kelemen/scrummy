using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Sprint
{
    internal class ViewSprintPresenter : Presenter, IViewSprintPresenter
    {
        public ViewSprintPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewSprintViewModel Present(ViewSprintResponse response)
        {
            var project = RepositoryProvider.Project.Read(response.ProjectId);


            return new ViewSprintViewModel
            {
                Id = response.Id.ToString(),
                Name = response.Name,
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                Goal = response.Goal,
                StartDate = response.TimeSpan.Item1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = response.TimeSpan.Item2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Stories = response.Stories.Select(x =>
                {
                    var story = RepositoryProvider.WorkTask.Read(x.Id);

                    var tasks = x.Tasks
                        .Select(y =>
                        {
                            var task = RepositoryProvider.WorkTask.Read(y.Item1);

                            return new
                            {
                                ViewModel = new NavigationViewModel
                                {
                                    Id = task.Id.ToString(),
                                    Text = task.Name,
                                },
                                Status = y.Item2,
                            };
                        })
                        .GroupBy(y => y.Status.ToString())
                        .ToDictionary(groupedTask => groupedTask.Key, groupedTask => groupedTask.Select(y => y.ViewModel));

                    var dict = new Dictionary<string, IList<NavigationViewModel>>();
                    foreach (var task in tasks)
                        dict.Add(task.Key, task.Value.ToList());

                    return new ViewSprintViewModel.StoryViewModel
                    {
                        Id = story.Id.ToString(),
                        Text = story.Name,
                        StoryPoints = story.StoryPoints?.ToString(),
                        Completed = x.Completed ? "Yes" : "No",
                        Tasks = dict,
                    };
                }),
                CanDelete = response.CanDelete,
                Documents = response.Documents.Select(x =>
                {
                    var document = RepositoryProvider.Document.Read(x);

                    return new NavigationViewModel
                    {
                        Id = document.Id.ToString(),
                        Text = document.Name,
                    };
                }),
                IsReportAvailable = response.IsReportAvailable,
            };
        }
    }
}
