using System;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Implementation.Sprint;
using Scrummy.Application.Web.MVC.Presenters.Project;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Project;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Project
{
    internal class ViewProjectPresenter : Presenter, IViewProjectPresenter
    {
        private readonly ViewSprintPresenter _viewSprintPresenter;
        private readonly SprintReportPresenter _sprintReportPresenter;

        public ViewProjectPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
            _viewSprintPresenter = new ViewSprintPresenter(MessageHandler, ErrorHandler, RepositoryProvider);
            _sprintReportPresenter = new SprintReportPresenter(MessageHandler, ErrorHandler, RepositoryProvider);
        }

        public ViewProjectViewModel Present(ViewProjectResponse response)
        {
            var team = RepositoryProvider.Team.Read(response.TeamId);
            return new ViewProjectViewModel
            {
                Id = response.Id.ToPresentationIdentity(),
                Name = response.Name,
                Team = team.ToViewModel(),
                Description = response.Description,
                DefinitionOfDone = response.DefinitionOfDone,
                Sprint = response.Sprint != null ? _viewSprintPresenter.Present(response.Sprint) : null,
                Report = response.Report != null ? _sprintReportPresenter.Present(response.Report) : null,
                CanDelete = response.CanDelete,
            };
        }
    }
}
