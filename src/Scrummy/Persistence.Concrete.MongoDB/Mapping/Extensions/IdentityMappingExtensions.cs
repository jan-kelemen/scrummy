using MongoDB.Bson;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions
{
    internal static class IdentityMappingExtensions
    {
        public static Identity ToDomainIdentity(this ObjectId id) => Identity.FromString(id.ToString());

        public static ObjectId ToPersistenceIdentity(this Identity id) => ObjectId.Parse(id.ToString());
    }
}
