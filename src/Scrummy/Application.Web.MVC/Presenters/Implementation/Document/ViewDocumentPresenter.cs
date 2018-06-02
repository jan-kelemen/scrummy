using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Presenters.Document;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Document;
using Scrummy.Application.Web.MVC.ViewModels.Utility;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Document;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Document
{
    internal class ViewDocumentPresenter : Presenter, IViewDocumentPresenter
    {
        public ViewDocumentPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewDocumentViewModel Present(ViewDocumentResponse response)
        {
            var project = RepositoryProvider.Project.Read(response.Project);

            return new ViewDocumentViewModel
            {
                Id = response.Id.ToString(),
                Name = response.Name,
                Project = new NavigationViewModel
                {
                    Id = project.Id.ToString(),
                    Text = project.Name,
                },
                Content = response.Content,
                DocumentType = DocumentKindToUserType(response.Kind),
                Links = response.Links.ToArray(),
                Tasks = response.Tasks.Select(x =>
                {
                    var task = RepositoryProvider.WorkTask.Read(x);

                    return new NavigationViewModel
                    {
                        Id = task.Id.ToString(),
                        Text = task.Name,
                    };
                }).ToArray(),
                Meetings = response.Meetings.Select(x =>
                {
                    var meeting = RepositoryProvider.Meeting.Read(x);

                    return new NavigationViewModel
                    {
                        Id = meeting.Id.ToString(),
                        Text = meeting.Name,
                    };
                }).ToArray(),
                Sprints = response.Sprints.Select(x =>
                {
                    var sprint = RepositoryProvider.Sprint.Read(x);

                    return new NavigationViewModel
                    {
                        Id = sprint.Id.ToString(),
                        Text = sprint.Name,
                    };
                }).ToArray(),
            };
        }

        private string DocumentKindToUserType(DocumentKind kind)
        {
            switch (kind)
            {
                case DocumentKind.Project:
                    return "Project";
                case DocumentKind.WorkTask:
                    return "Work task";
                case DocumentKind.Meeting:
                    return "Meeting";
                case DocumentKind.Sprint:
                    return "Sprint";
                case DocumentKind.Common:
                    return "Common";
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}

