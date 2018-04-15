using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Exceptions;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;

using MPerson = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Person;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class PersonRepository : BaseRepository, IPersonRepository
    {
        private readonly IMongoCollection<MPerson> _personCollection;

        public PersonRepository(IMongoCollection<MPerson> personCollection)
        {
            _personCollection = personCollection;
        }

        public Identity CreatePerson(Person person)
        {
            if (person == null) { throw CreateInvalidEntityException(); }

            var entity = person.ToPersistenceEntity();
            _personCollection.InsertOne(entity);
            return person.Id;
        }

        public Person ReadPerson(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _personCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if(entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public void UpdatePerson(Person person)
        {
            if (person == null) { throw CreateInvalidEntityException(); }

            var entity = person.ToPersistenceEntity();
            var result = _personCollection.ReplaceOne(x => x.Id == entity.Id, entity);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(person.Id); }
        }

        public void DeletePerson(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _personCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if(result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public bool CheckIfEmailExists(string email) => 
            _personCollection.Find(x => x.Email == email).FirstOrDefault() != null;

        private static EntityNotFoundException CreateEntityNotFoundException(Identity id)
        {
            return new EntityNotFoundException
            {
                Identity = id,
                EntityName = nameof(Person),
            };
        }

        private static InvalidEntityException CreateInvalidEntityException()
        {
            return new InvalidEntityException();
        }
    }
}
