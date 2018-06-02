using System;
using System.Linq;
using Scrummy.Application.Web.MVC.Extensions.Entities;
using Scrummy.Application.Web.MVC.Presenters.Document;
using Scrummy.Application.Web.MVC.Utility;
using Scrummy.Application.Web.MVC.ViewModels.Document;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories;

namespace Scrummy.Application.Web.MVC.Presenters.Implementation.Document
{
    internal class EditDocumentPresenter : Presenter, IEditDocumentPresenter
    {
        public EditDocumentPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public EditDocumentViewModel GetInitialViewModel(string id)
        {
            var document = RepositoryProvider.Document.Read(Identity.FromString(id));
            var project = RepositoryProvider.Project.Read(document.Project);

            return new EditDocumentViewModel
            {
                Id = document.Id.ToPresentationIdentity(),
                Name = document.Name,
                Project = project.ToViewModel(),
                Content = document.Content,
                Links = document.Links.ToArray(),
                DocumentType = DocumentKindToUserType(document.Kind),
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
