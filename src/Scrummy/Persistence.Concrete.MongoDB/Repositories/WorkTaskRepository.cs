using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;
using MWorkTask = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.WorkTask;
using WorkTask = Scrummy.Domain.Core.Entities.WorkTask;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class WorkTaskRepository : BaseRepository<WorkTask>, IWorkTaskRepository
    {
        private readonly IMongoCollection<MWorkTask> _workTaskCollection;

        public WorkTaskRepository(IMongoCollection<MWorkTask> workTaskCollection)
        {
            _workTaskCollection = workTaskCollection;
        }

        public override Identity Create(WorkTask workTask)
        {
            if (workTask == null || _workTaskCollection.Find(x => x.Id == workTask.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = workTask.ToPersistenceEntity();
            entity.Comments = new Comment[0];
            _workTaskCollection.InsertOne(entity);

            var linkUpdateDefinition = Builders<MWorkTask>.Update
                .Set(w => w.ParentTask, entity.Id);
            _workTaskCollection.UpdateMany(x => entity.ChildTasks.Contains(x.Id), linkUpdateDefinition);

            if(!workTask.ParentTask.IsBlankIdentity())
            {
                var childUpdateDefinition = Builders<MWorkTask>.Update
                    .Push(w => w.ChildTasks, entity.Id);
                _workTaskCollection.UpdateOne(x => x.Id == entity.ParentTask, childUpdateDefinition);
            }

            return workTask.Id;
        }

        public override WorkTask Read(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _workTaskCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public override void Update(WorkTask entity)
        {
            if (entity == null)
                throw CreateInvalidEntityException();

            var databaseEntity = _workTaskCollection.Find(x => x.Id == entity.Id.ToPersistenceIdentity()).FirstOrDefault();
            if (databaseEntity == null) { throw CreateEntityNotFoundException(entity.Id); }

            var persistenceEntity = entity.ToPersistenceEntity();

            var hasParentTaskChanged = databaseEntity.ParentTask != persistenceEntity.ParentTask;

            var updateDefinition = Builders<MWorkTask>.Update
                .Set(w => w.Name, persistenceEntity.Name)
                .Set(w => w.StoryPoints, persistenceEntity.StoryPoints)
                .Set(w => w.Description, persistenceEntity.Description)
                .Set(w => w.ChildTasks, persistenceEntity.ChildTasks);

            var updateEntityResult = _workTaskCollection.UpdateOne(x => x.Id == persistenceEntity.Id, updateDefinition);

            if (updateEntityResult.MatchedCount != 1)
                throw CreateEntityNotFoundException(entity.Id);

            var linkUpdateDefinition = Builders<MWorkTask>.Update
                .Set(w => w.ParentTask, persistenceEntity.Id);

            _workTaskCollection.UpdateMany(x => persistenceEntity.ChildTasks.Contains(x.Id), linkUpdateDefinition);

            if (hasParentTaskChanged && databaseEntity.ParentTask != ObjectId.Empty)
            {
                var parentUpdateDefinition = Builders<MWorkTask>.Update
                    .Pull(x => x.ChildTasks, persistenceEntity.Id);

                _workTaskCollection.UpdateOne(x => x.Id == databaseEntity.ParentTask, parentUpdateDefinition);
            }

            var removedChildren = databaseEntity.ChildTasks.Except(persistenceEntity.ChildTasks);
            var childUpdateDefinition = Builders<MWorkTask>.Update
                .Set(x => x.ParentTask, ObjectId.Empty);

            _workTaskCollection.UpdateMany(x => removedChildren.Contains(x.Id) && x.ParentTask == databaseEntity.Id, childUpdateDefinition);
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _workTaskCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public Identity AddComment(WorkTask.Comment comment)
        {
            if (comment == null || _workTaskCollection.Find(x => x.Id == comment.WorkTaskId.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = comment.ToPersistenceEntity();
            var updateDefinition = Builders<MWorkTask>.Update
                .Push(x => x.Comments, entity);

            var result = _workTaskCollection.UpdateOne(x => x.Id == comment.WorkTaskId.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1)
                throw CreateEntityNotFoundException(comment.WorkTaskId);

            return comment.Id;
        }

        public WorkTask.Comment ReadComment(Identity workTaskIdentity, Identity commentIdentity)
        {
            if (workTaskIdentity.IsBlankIdentity()) { throw CreateEntityNotFoundException(workTaskIdentity); }
            if (commentIdentity.IsBlankIdentity()) { throw CreateEntityNotFoundException(commentIdentity); }

            var workTask = _workTaskCollection.Find(x => x.Id == workTaskIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (workTask == null)
                throw CreateEntityNotFoundException(workTaskIdentity);

            var comment = workTask.Comments.FirstOrDefault(x => x.Id == commentIdentity.ToPersistenceIdentity());
            if (comment == null)
                throw CreateEntityNotFoundException(commentIdentity);

            return comment.ToDomainEntity(workTaskIdentity.ToPersistenceIdentity());
        }

        public IEnumerable<WorkTask.Comment> ReadCommentsOfTask(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var workTask = _workTaskCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (workTask == null)
                throw CreateEntityNotFoundException(id);

            return workTask.Comments.Select(x => x.ToDomainEntity(workTask.Id));
        }

        public void UpdateComment(WorkTask.Comment comment)
        {
            var workTask = _workTaskCollection.Find(x => x.Id == comment.WorkTaskId.ToPersistenceIdentity()).FirstOrDefault();
            if (workTask == null)
                throw CreateEntityNotFoundException(comment.WorkTaskId);

            var entity = workTask.Comments.FirstOrDefault(x => x.Id == comment.Id.ToPersistenceIdentity());
            if (entity == null)
                throw CreateEntityNotFoundException(comment.Id);

            entity.Content = comment.Content;

            var result = _workTaskCollection.ReplaceOne(x => x.Id == workTask.Id, workTask);

            if (result.MatchedCount != 1)
                throw CreateEntityNotFoundException(comment.WorkTaskId);
        }

        public void DeleteComment(Identity workTaskIdentity, Identity commentIdentity)
        {
            if (workTaskIdentity.IsBlankIdentity()) { throw CreateEntityNotFoundException(workTaskIdentity); }
            if (commentIdentity.IsBlankIdentity()) { throw CreateEntityNotFoundException(commentIdentity); }

            var updateDefinition = Builders<MWorkTask>.Update
                .PullFilter(x => x.Comments, c => c.Id == commentIdentity.ToPersistenceIdentity());

            _workTaskCollection.FindOneAndUpdate(x => x.Id == workTaskIdentity.ToPersistenceIdentity(),
                updateDefinition);
        }

        public override IEnumerable<NavigationInfo> ListAll()
        {
            return _workTaskCollection.AsQueryable().ToList().Select(x => new NavigationInfo
            {
                Id = x.Id.ToDomainIdentity(),
                Name = x.Name,
            });
        }
    }
}
