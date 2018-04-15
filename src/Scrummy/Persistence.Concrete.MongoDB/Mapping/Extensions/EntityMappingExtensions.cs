using Scrummy.Domain.Core.Entities;
using MPerson = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Person;

// ReSharper disable ArgumentsStyleOther
// ReSharper disable ArgumentsStyleNamedExpression

namespace Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions
{
    internal static class EntityMappingExtensions
    {
        public static Person ToDomainEntity(this MPerson person)
        {
            return new Person(
                id: person.Id.ToDomainIdentity(),
                firstName: person.FirstName,
                lastName: person.LastName,
                displayName: person.DisplayName,
                email: person.Email);
        }

        public static MPerson ToPersistenceEntity(this Person person)
        {
            return new MPerson
            {
                Id = person.Id.ToPersistenceIdentity(),
                FirstName = person.FirstName,
                LastName = person.LastName,
                DisplayName = person.DisplayName,
                Email = person.Email,
            };
        }
    }
}
