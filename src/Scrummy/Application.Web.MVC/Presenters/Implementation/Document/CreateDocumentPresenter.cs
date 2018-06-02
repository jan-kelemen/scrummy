using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Document;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Document;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Document
{
    internal class CreateDocumentPresenter : Presenter, ICreateDocumentPresenter
    {
        public CreateDocumentPresenter(
            Action<MessageType, string> messageHandler, 
            Action<string, string> errorHandler, 
            IRepositoryProvider repositoryProvider) 
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public CreateDocumentViewModel GetInitialViewModel(string projectId, string type)
        {
            var project = RepositoryProvider.Project.Read(Identity.FromString(projectId));
            return new CreateDocumentViewModel
            {
                Project = project.ToViewModel(),
                DocumentType = type,
                DocumentTypes = DocumentTypes(),
            };
        }

        public CreateDocumentViewModel Present(CreateDocumentViewModel vm)
        {
            vm.DocumentTypes = DocumentTypes();
            return vm;
        }

        private SelectListItem[] DocumentTypes()
        {
            return new[]
            {
                new SelectListItem { Value = nameof(DocumentKind.Common), Text = "Common" },
                new SelectListItem { Value = nameof(DocumentKind.Project), Text = "Project" },
                new SelectListItem { Value = nameof(DocumentKind.Meeting), Text = "Meeting" },
                new SelectListItem { Value = nameof(DocumentKind.Sprint), Text = "Sprint" },
                new SelectListItem { Value = nameof(DocumentKind.WorkTask), Text = "Work task" },
            };
        }
    }
}
