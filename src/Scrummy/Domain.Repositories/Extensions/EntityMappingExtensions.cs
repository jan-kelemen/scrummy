using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Repositories.Interfaces.DTO;

namespace Scrummy.Domain.Repositories.Extensions
{
    public static class EntityMappingExtensions
    {
        public static NavigationInfo ToInfo(this Document entity) => new NavigationInfo
        {
            Id = entity.Id,
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this Meeting entity) => new NavigationInfo
        {
            Id = entity.Id,
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this Person entity) => new NavigationInfo
        {
            Id = entity.Id,
            Name = entity.DisplayName,
        };

        public static NavigationInfo ToInfo(this Project entity) => new NavigationInfo
        {
            Id = entity.Id,
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this Sprint entity) => new NavigationInfo
        {
            Id = entity.Id,
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this Team entity) => new NavigationInfo
        {
            Id = entity.Id,
            Name = entity.Name,
        };

        public static NavigationInfo ToInfo(this WorkTask entity) => new NavigationInfo
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
