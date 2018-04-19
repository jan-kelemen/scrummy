using System;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;
using MWorkTask = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.WorkTask;

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
            _workTaskCollection.InsertOne(entity);
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

            var persistenceEntity = entity.ToPersistenceEntity();

            var updateDefinition = Builders<MWorkTask>.Update
                .Set(w => w.Name, persistenceEntity.Name)
                .Set(w => w.StoryPoints, persistenceEntity.StoryPoints)
                .Set(w => w.Description, persistenceEntity.Description)
                .Set(w => w.LinkedTo, persistenceEntity.LinkedTo);

            var updateEntityResult = _workTaskCollection.UpdateOne(x => x.Id == persistenceEntity.Id, updateDefinition);

            if (updateEntityResult.MatchedCount != 1)
                throw CreateEntityNotFoundException(entity.Id);

            var linkUpdateDefinition = Builders<MWorkTask>.Update
                .Push(w => w.LinkedFrom, persistenceEntity.Id);

            _workTaskCollection.UpdateMany(x => persistenceEntity.LinkedTo.Contains(x.Id) && !x.LinkedFrom.Contains(persistenceEntity.Id), linkUpdateDefinition);
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
    }
}
