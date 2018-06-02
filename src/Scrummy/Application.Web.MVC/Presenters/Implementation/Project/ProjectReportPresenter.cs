using System;
using System.Globalization;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Project
{
    internal class ProjectReportPresenter : Presenter, IProjectReportPresenter
    {
        public ProjectReportPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ProjectReportViewModel Present(ProjectReportResponse response)
        {
            return new ProjectReportViewModel
            {
                Project =  response.Project.ToViewModel(),
                Records = response.Records.Select((x, i) => new ProjectReportViewModel.Record
                {
                    Date = x.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DoneTasks = x.DoneTasks,
                    ReadyTasks = x.DoneTasks + x.ReadyTasks,
                    InSprintTasks = x.DoneTasks + x.ReadyTasks + x.InSprintTasks,
                    ToDoTasks = x.DoneTasks + x.ReadyTasks + x.InSprintTasks + x.ToDoTasks

                }).ToArray(),
                Sprints = response.Sprints.OrderBy(x => x.EndDate).Select(x => new ProjectReportViewModel.Sprint
                {
                    Id = x.Id.ToPresentationIdentity(),
                    Text = x.Name,
                    EndDate = x.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    CompletedStories = x.CompletedStories,
                    CompletedValue = x.CompletedValue,
                    PlannedStories = x.PlannedStories,
                    PlannedValue = x.PlannedValue,
                }).ToArray(),
            };
        }
    }
}
