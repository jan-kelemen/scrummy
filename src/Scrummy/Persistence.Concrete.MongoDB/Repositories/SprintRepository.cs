using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Persistence.Concrete.MongoDB.Extensions;
using MSprint = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Sprint;
using MProject = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Project;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class SprintRepository : BaseRepository<Sprint>, ISprintRepository
    {
        private readonly IMongoCollection<MSprint> _sprintCollection;
        private readonly IMongoCollection<MProject> _projectCollection;

        public SprintRepository(IMongoCollection<MSprint> sprintCollection, IMongoCollection<MProject> projectCollection)
        {
            _sprintCollection = sprintCollection;
            _projectCollection = projectCollection;
        }

        public override Identity Create(Sprint sprint)
        {
            if (sprint == null || _sprintCollection.Find(x => x.Id == sprint.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = sprint.ToPersistenceEntity();
            entity.Backlog = new MSprint.BacklogItem[0];
            entity.BacklogHistory = new MSprint.BacklogHistoryRecord[0];
            entity.PlannedTasks = new ObjectId[0];
            entity.CompletedTasks = new ObjectId[0];
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

            var updateDefinition = Builders<MSprint>.Update
                .Set(x => x.StartDate, sprint.TimeSpan.Item1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                .Set(x => x.EndDate, sprint.TimeSpan.Item2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                .Set(x => x.Name, sprint.Name)
                .Set(x => x.Goal, sprint.Goal)
                .Set(x => x.Status, sprint.Status)
                .Set(x => x.Documents, sprint.Documents.Select(x => x.ToPersistenceIdentity()));

            var result = _sprintCollection
                .UpdateOne(x => x.Id == sprint.Id.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(sprint.Id); }
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _sprintCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public override bool Exists(Identity id) => _sprintCollection.Count(x => x.Id == id.ToPersistenceIdentity()) == 1;

        public Sprint ReadCurrentSprint(Identity projectId)
        {
            if (projectId.IsBlankIdentity()) { throw CreateEntityNotFoundException(projectId); }

            var entity = _projectCollection.Find(x => x.Id == projectId.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(projectId); }

            var sprint = _sprintCollection
                .Find(x => x.ProjectId == projectId.ToPersistenceIdentity() && x.Status == SprintStatus.InProgress)
                .FirstOrDefault();

            return sprint?.ToDomainEntity();
        }

        public SprintBacklog ReadSprintBacklog(Identity sprintIdentity)
        {
            if (sprintIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _sprintCollection.Find(x => x.Id == sprintIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(sprintIdentity); }

            return new SprintBacklog(
                sprintIdentity,
                entity.PlannedTasks.Select(t => t.ToDomainIdentity()),
                entity.CompletedTasks.Select(t => t.ToDomainIdentity()),
                entity.Backlog.Select(t => new SprintBacklog.WorkTaskWithStatus(
                    t.WorkTaskId.ToDomainIdentity(),
                    t.ParentTaskId.ToDomainIdentity(),
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

            var updateDefinition = Builders<MSprint>.Update
                .Set(x => x.PlannedTasks, backlog.Stories.Select(x => x.ToPersistenceIdentity()));

            var result = _sprintCollection.UpdateOne(x => x.Id == sprintIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(sprintIdentity); }
        }

        public void UpdateCurrentTasks(SprintBacklog backlog)
        {
            var sprintIdentity = backlog.SprintId;
            if (sprintIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _sprintCollection.Find(x => x.Id == sprintIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(sprintIdentity); }

            var historyRecord = new MSprint.BacklogHistoryRecord
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                DoneTasks = backlog.Tasks.Count(x => x.Status == SprintBacklog.WorkTaskStatus.Done),
                InProgressTasks = backlog.Tasks.Count(x => x.Status == SprintBacklog.WorkTaskStatus.InProgress),
                ToDoTasks = backlog.Tasks.Count(x => x.Status == SprintBacklog.WorkTaskStatus.ToDo),
            };

            var updateDefinition = Builders<MSprint>.Update
                .Set(p => p.Backlog, backlog.Tasks.Select(x => new MSprint.BacklogItem
                {
                    WorkTaskId = x.WorkTaskId.ToPersistenceIdentity(),
                    ParentTaskId = x.ParentTaskId.ToPersistenceIdentity(),
                    Status = x.Status
                }))
                .Set(x => x.CompletedTasks, backlog.CompletedStories.Select(x => x.ToPersistenceIdentity()))
                .Push(p => p.BacklogHistory, historyRecord);

            var result = _sprintCollection.UpdateOne(x => x.Id == sprintIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(sprintIdentity); }
        }

        public IEnumerable<Sprint> ReadSprints(Identity projectId, SprintStatus status)
        {
            if (projectId.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var sprints = _sprintCollection
                .Find(x => x.ProjectId == projectId.ToPersistenceIdentity() && x.Status == status)
                .ToEnumerable();

            return sprints.Select(x => x.ToDomainEntity());
        }

        public IEnumerable<SprintBacklog> ReadSprintBacklogs(Identity projectId, SprintStatus status)
        {
            if (projectId.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var sprints = _sprintCollection
                .Find(x => x.ProjectId == projectId.ToPersistenceIdentity() && x.Status == status)
                .ToEnumerable();

            return sprints.Select(x =>
                new SprintBacklog(x.Id.ToDomainIdentity(),
                    x.PlannedTasks.Select(t => t.ToDomainIdentity()),
                    x.CompletedTasks.Select(t => t.ToDomainIdentity()),
                    x.Backlog.Select(t => new SprintBacklog.WorkTaskWithStatus(
                        t.WorkTaskId.ToDomainIdentity(),
                        t.ParentTaskId.ToDomainIdentity(),
                        t.Status))));
        }

        public IEnumerable<SprintBacklogHistoryRecord> ReadHistoryRecords(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _sprintCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.BacklogHistory.Select(x => new SprintBacklogHistoryRecord
            {
                Id = entity.Id.ToDomainIdentity(),
                Date = DateTime.ParseExact(x.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                ToDoTasks = x.ToDoTasks,
                DoneTasks = x.DoneTasks,
                InProgressTasks = x.InProgressTasks,
            });
        }

        public override IEnumerable<NavigationInfo> ListAll() => _sprintCollection.AsQueryable().Select(x => x.ToInfo());
    }
}
