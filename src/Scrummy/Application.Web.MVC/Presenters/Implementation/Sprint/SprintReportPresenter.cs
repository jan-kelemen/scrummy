using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Sprint;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Sprint;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Sprint;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Sprint
{
    internal class SprintReportPresenter : Presenter, ISprintReportPresenter
    {
        public SprintReportPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public SprintReportViewModel Present(SprintReportResponse response)
        {
            var project = RepositoryProvider.Project.Read(response.ProjectId);

            return new SprintReportViewModel
            {
                Project = project.ToViewModel(),
                Sprint = response.Sprint.ToViewModel(),
                StartDate = response.TimeSpan.Item1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = response.TimeSpan.Item2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Goal = response.Goal,
                Stories = response.Stories.Select(x => new SprintReportViewModel.Story
                {
                    Id = x.Id.ToPresentationIdentity(),
                    Text = x.Name,
                    StoryPoints = x.StoryPoints ?? 0,
                    CompletedTasks = x.CompletedTasks,
                    TotalTasks = x.TotalTasks,
                    Completed = x.Completed,
                }).ToArray(),
                Records = response.Records.Select(x => new SprintReportViewModel.Record
                {
                    Date = x.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DoneTasks = x.DoneTasks,
                    ToDoTasks = x.ToDoTasks,
                    InProgressTasks = x.InProgressTasks,
                }).ToArray(),
            };
        }
    }
}
