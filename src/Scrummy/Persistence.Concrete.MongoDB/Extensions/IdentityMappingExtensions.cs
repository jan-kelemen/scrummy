using MongoDB.Bson;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Persistence.Concrete.MongoDB.Extensions
{
    public static class IdentityMappingExtensions
    {
        public static Identity ToDomainIdentity(this ObjectId id) => id == ObjectId.Empty ? Identity.BlankIdentity : Identity.FromString(id.ToString());

        public static ObjectId ToPersistenceIdentity(this Identity id) => id.IsBlankIdentity() ? ObjectId.Empty : ObjectId.Parse(id.ToString());
    }
}
