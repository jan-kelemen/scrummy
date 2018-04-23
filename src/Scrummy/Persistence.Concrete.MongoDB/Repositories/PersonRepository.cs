using System;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;

using MPerson = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Person;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IMongoCollection<MPerson> _personCollection;

        public PersonRepository(IMongoCollection<MPerson> personCollection)
        {
            _personCollection = personCollection;
        }

        public override Identity Create(Person person)
        {
            if (person == null || _personCollection.Find(x => x.Id == person.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = person.ToPersistenceEntity();
            _personCollection.InsertOne(entity);
            return person.Id;
        }

        public override Person Read(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _personCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if(entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public override void Update(Person person)
        {
            if (person == null) { throw CreateInvalidEntityException(); }

            var entity = person.ToPersistenceEntity();

            var updateDefinition = Builders<MPerson>.Update
                .Set(x => x.FirstName, person.FirstName)
                .Set(x => x.LastName, person.LastName)
                .Set(x => x.DisplayName, person.DisplayName);

            var result = _personCollection.UpdateOne(x => x.Id == entity.Id, updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(person.Id); }
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _personCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if(result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public bool CheckIfEmailExists(string email) => 
            _personCollection.Find(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant()).FirstOrDefault() != null;

        public Person FindByEmailAndPasswordHash(string email, string passwordHash)
        {
            return _personCollection.AsQueryable()
                .FirstOrDefault(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant() && x.PasswordHash == passwordHash)
                ?.ToDomainEntity();
        }
    }
}
