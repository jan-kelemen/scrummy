using MongoDB.Bson;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class BaseRepository : IRepository
    {
        public Identity GenerateNewIdentity() => Identity.FromString(ObjectId.GenerateNewId().ToString());
    }
}
