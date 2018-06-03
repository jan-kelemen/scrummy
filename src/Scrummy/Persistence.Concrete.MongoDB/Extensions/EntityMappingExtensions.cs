using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;

namespace Scrummy.Persistence.Concrete.MongoDB.Extensions
{
    internal static class EntityMappingExtensions
    {
        public static NavigationInfo ToInfo(this Document entity) => new NavigationInfo
        {
            Id = entity.Id.ToDomainIdentity(),
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this Meeting entity) => new NavigationInfo
        {
            Id = entity.Id.ToDomainIdentity(),
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this Person entity) => new NavigationInfo
        {
            Id = entity.Id.ToDomainIdentity(),
            Name = entity.DisplayName,
        };

        public static NavigationInfo ToInfo(this Project entity) => new NavigationInfo
        {
            Id = entity.Id.ToDomainIdentity(),
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this Sprint entity) => new NavigationInfo
        {
            Id = entity.Id.ToDomainIdentity(),
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this Team entity) => new NavigationInfo
        {
            Id = entity.Id.ToDomainIdentity(),
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this WorkTask entity) => new NavigationInfo
        {
            Id = entity.Id.ToDomainIdentity(),
            Name = entity.Name,
        };
    }
}
