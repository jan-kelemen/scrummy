using System;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
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

        public SprintBacklog GetSprintBacklog(Identity sprintIdentity)
        {
            if (sprintIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _sprintCollection.Find(x => x.Id == sprintIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(sprintIdentity); }

            var currentBacklog = entity.CurrentBacklog;

            return new SprintBacklog(
                sprintIdentity,
                entity.PlannedTasks.Select(t => t.ToDomainIdentity()),
                currentBacklog.Tasks.Select(t => new SprintBacklog.WorkTaskWithStatus(
                    t.WorkTaskId.ToDomainIdentity(),
                    t.Status)
                )
            );
        }

        public void UpdatePlannedTasks(SprintBacklog backlog)
        {
            var sprintIdentity = backlog.SprintId;
            if (sprintIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _sprintCollection.Find(x => x.Id == sprintIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(sprintIdentity); }

            var currentBacklog = entity.CurrentBacklog;
            currentBacklog.To = DateTime.UtcNow;

            var updateDefinition = Builders<MSprint>.Update
                .Set(x => x.PlannedTasks, backlog.PlannedTaskIds.Select(x => x.ToPersistenceIdentity()));

            var result = _sprintCollection.UpdateOne(x => x.Id == sprintIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(sprintIdentity); }
        }

        public void UpdateCurrentTasks(SprintBacklog backlog)
        {
            var sprintIdentity = backlog.SprintId;
            if (sprintIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _sprintCollection.Find(x => x.Id == sprintIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(sprintIdentity); }

            var currentBacklog = entity.CurrentBacklog;
            currentBacklog.To = DateTime.UtcNow;

            var updateDefinition = Builders<MSprint>.Update
                .Set(p => p.CurrentBacklog, new MSprint.BacklogHistoryRecord
                {
                    From = DateTime.UtcNow,
                    To = DateTime.MaxValue,
                    Tasks = backlog.Tasks.Select(x => new MSprint.BacklogItem
                    {
                        WorkTaskId = x.WorkTaskId.ToPersistenceIdentity(),
                        Status = x.Status
                    }),
                })
                .Push(p => p.BacklogHistory, currentBacklog);

            var result = _sprintCollection.UpdateOne(x => x.Id == sprintIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(sprintIdentity); }
        }

        public bool CheckIfSprintOverlapsWithOtherSprint(Identity projectIdentity, Tuple<DateTime, DateTime> timeSpan)
        {
            throw new NotImplementedException();
        }
    }
}
