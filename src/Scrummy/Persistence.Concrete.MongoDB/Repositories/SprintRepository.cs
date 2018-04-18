using System;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;
using MSprint = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Sprint;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class SprintRepository : BaseRepository<Sprint>, ISprintRepository
    {
        private readonly IMongoCollection<MSprint> _sprintCollection;

        public SprintRepository(IMongoCollection<MSprint> sprintCollection)
        {
            _sprintCollection = sprintCollection;
        }

        public override Identity Create(Sprint sprint)
        {
            if (sprint == null || _sprintCollection.Find(x => x.Id == sprint.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = sprint.ToPersistenceEntity();
            _sprintCollection.InsertOne(entity);
            return sprint.Id;
        }

        public override Sprint Read(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _sprintCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public override void Update(Sprint sprint)
        {
            if (sprint == null) { throw CreateInvalidEntityException(); }

            var entity = sprint.ToPersistenceEntity();
            var result = _sprintCollection.ReplaceOne(x => x.Id == entity.Id, entity);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(sprint.Id); }
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _sprintCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public bool CheckIfSprintOverlapsWithOtherSprint(Identity projectIdentity, Tuple<DateTime, DateTime> timeSpan)
        {
            throw new NotImplementedException();
        }
    }
}
