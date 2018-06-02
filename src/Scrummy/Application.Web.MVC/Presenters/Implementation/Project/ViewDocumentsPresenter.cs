using System;
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
    internal class ViewDocumentsPresenter : Presenter, IViewDocumentsPresenter
    {
        public ViewDocumentsPresenter(
            Action<MessageType, string> messageHandler,
            Action<string, string> errorHandler,
            IRepositoryProvider repositoryProvider)
            : base(messageHandler, errorHandler, repositoryProvider)
        {
        }

        public ViewDocumentsViewModel Present(string projectId, string flavor)
        {
            var f = Enum.Parse<Flavor>(flavor);
            var project = RepositoryProvider.Project.Read(Identity.FromString(projectId));
            var pred = FlavorToFilter(f);
            return new ViewDocumentsViewModel
            {
                Project = project.ToViewModel(),
                Flavor = FlavorToUserFlavor(f),
                Documents = RepositoryProvider.Document.ListAll()
                    .Select(x =>
                    {
                        var document = RepositoryProvider.Document.Read(x.Id);

                        return new
                        {
                            document.Id,
                            document.Name,
                            document.Kind,
                        };
                    })
                    .Where(x => pred(x.Kind))
                    .Select(x => new ViewDocumentsViewModel.Document
                    {
                        Id = x.Id.ToPresentationIdentity(),
                        Text = x.Name,
                        Type = DocumentKindToUserType(x.Kind)
                    }).ToArray(),
            };
        }

        private Func<DocumentKind, bool> FlavorToFilter(Flavor flavor)
        {
            switch (flavor)
            {
                case Flavor.Project:
                    return k => k == DocumentKind.Project || k == DocumentKind.Common;
                case Flavor.WorkTask:
                    return k => k == DocumentKind.WorkTask || k == DocumentKind.Common;
                case Flavor.Meeting:
                    return k => k == DocumentKind.Meeting || k == DocumentKind.Common;
                case Flavor.Sprint:
                    return k => k == DocumentKind.Sprint || k == DocumentKind.Common;
                case Flavor.Common:
                    return k => k == DocumentKind.Common;
                case Flavor.All:
                    return k => true;
            }
            throw new ArgumentOutOfRangeException();
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

        private string FlavorToUserFlavor(Flavor kind)
        {
            switch (kind)
            {
                case Flavor.Project:
                    return "Project";
                case Flavor.WorkTask:
                    return "Work task";
                case Flavor.Meeting:
                    return "Meeting";
                case Flavor.Sprint:
                    return "Sprint";
                case Flavor.Common:
                    return "Common";
                case Flavor.All:
                    return "All";
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
