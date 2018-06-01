using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces.DTO;

namespace Scrummy.Domain.Repositories.Interfaces
{
    public interface IDocumentRepository : IRepository<Document>
    {
        DocumentWithReferences ReadWithReferences(Identity id);

        IEnumerable<NavigationInfo> ListByKind(Identity projectId, DocumentKind kind);
    }
}
