using MongoDB.Bson;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Exceptions;
using Scrummy.Domain.Repositories.Interfaces;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class BaseRepository<T> : IRepository
    {
        public Identity GenerateNewIdentity() => Identity.FromString(ObjectId.GenerateNewId().ToString());

        protected static EntityNotFoundException CreateEntityNotFoundException(Identity id)
        {
            return new EntityNotFoundException
            {
                Identity = id,
                EntityName = nameof(T),
            };
        }

        protected static InvalidEntityException CreateInvalidEntityException()
        {
            return new InvalidEntityException();
        }
    }
}
