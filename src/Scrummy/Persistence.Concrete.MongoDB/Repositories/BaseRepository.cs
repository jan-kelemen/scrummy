using System.Collections.Generic;
using MongoDB.Bson;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Exceptions;
using Scrummy.Domain.Repositories.Interfaces;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal abstract class BaseRepository<T> : IRepository<T>
    {
        public Identity GenerateNewIdentity() => Identity.FromString(ObjectId.GenerateNewId().ToString());

        public abstract Identity Create(T entity);
        public abstract T Read(Identity id);
        public abstract void Update(T entity);
        public abstract void Delete(Identity id);
        public abstract IEnumerable<NavigationInfo> ListAll();

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
