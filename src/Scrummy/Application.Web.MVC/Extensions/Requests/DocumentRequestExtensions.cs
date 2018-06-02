using System;
using Scrummy.Application.Web.MVC.ViewModels.Document;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Interfaces.Document;

namespace Scrummy.Application.Web.MVC.Extensions.Requests
{
    public static class DocumentRequestExtensions
    {
        public static CreateDocumentRequest ToRequest(this CreateDocumentViewModel vm, string userId)
        {
            return new CreateDocumentRequest(userId)
            {
                ProjectId = Identity.FromString(vm.Project.Id),
                Name = vm.Name,
                Content = vm.Content,
                Kind = Enum.Parse<DocumentKind>(vm.DocumentType),
                Links = vm.Links,
            };
        }

        public static EditDocumentRequest ToRequest(this EditDocumentViewModel vm, string userId)
        {
            return new EditDocumentRequest(userId)
            {
                Id = Identity.FromString(vm.Id),
                Name = vm.Name,
                Content = vm.Content,
                Links = vm.Links,
            };
        }
    }
}
