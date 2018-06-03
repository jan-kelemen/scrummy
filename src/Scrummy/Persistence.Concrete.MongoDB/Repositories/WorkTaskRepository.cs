using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Extensions;
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

            var childTasks = workTask.ChildTasks.Select(x => x.ToPersistenceIdentity());
            var linkUpdateDefinition = Builders<MWorkTask>.Update
                .Set(w => w.ParentTask, entity.Id);
            _workTaskCollection.UpdateMany(x => childTasks.Contains(x.Id), linkUpdateDefinition);

            return workTask.Id;
        }

        public override WorkTask Read(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _workTaskCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            var childTasks = _workTaskCollection.Find(x => x.ParentTask == entity.Id)
                .ToEnumerable().Select(x => x.Id);

            return entity.ToDomainEntity(childTasks);
        }

        public override void Update(WorkTask entity)
        {
            if (entity == null)
                throw CreateInvalidEntityException();

            var databaseEntity = _workTaskCollection.Find(x => x.Id == entity.Id.ToPersistenceIdentity()).FirstOrDefault();
            if (databaseEntity == null) { throw CreateEntityNotFoundException(entity.Id); }

            var persistenceEntity = entity.ToPersistenceEntity();

            var updateDefinition = Builders<MWorkTask>.Update
                .Set(w => w.Name, persistenceEntity.Name)
                .Set(w => w.StoryPoints, persistenceEntity.StoryPoints)
                .Set(w => w.Description, persistenceEntity.Description)
                .Set(w => w.ParentTask, persistenceEntity.ParentTask)
                .Set(w => w.Steps, persistenceEntity.Steps)
                .Set(x => x.Documents, persistenceEntity.Documents);

            var updateEntityResult = _workTaskCollection.UpdateOne(x => x.Id == persistenceEntity.Id, updateDefinition);

            if (updateEntityResult.MatchedCount != 1)
                throw CreateEntityNotFoundException(entity.Id);

            var childTasks = entity.ChildTasks.Select(x => x.ToPersistenceIdentity());
            var childUpdateDefinition = Builders<MWorkTask>.Update
                .Set(x => x.ParentTask, ObjectId.Empty);

            _workTaskCollection.UpdateMany(x => x.ParentTask == entity.Id.ToPersistenceIdentity() && !childTasks.Contains(x.Id), childUpdateDefinition);

            var linkUpdateDefinition = Builders<MWorkTask>.Update
                .Set(w => w.ParentTask, persistenceEntity.Id);

            _workTaskCollection.UpdateMany(x => childTasks.Contains(x.Id), linkUpdateDefinition);
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }
      
            var result = _workTaskCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            var updateFilter = Builders<MWorkTask>.Update.Set(x => x.ParentTask, ObjectId.Empty);
            _workTaskCollection.UpdateMany(x => x.ParentTask == id.ToPersistenceIdentity(), updateFilter);

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public override bool Exists(Identity id) => _workTaskCollection.Count(x => x.Id == id.ToPersistenceIdentity()) == 1;

        public Identity AddComment(WorkTask.Comment comment)
        {
            if (comment == null || _workTaskCollection.Find(x => x.Id == comment.WorkTaskId.ToPersistenceIdentity()).FirstOrDefault() == null)
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

        public override IEnumerable<NavigationInfo> ListAll() => _workTaskCollection.AsQueryable().Select(x => x.ToInfo());
    }
}
