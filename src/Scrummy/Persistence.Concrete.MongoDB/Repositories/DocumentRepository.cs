using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Persistence.Concrete.MongoDB.Extensions;
using MDocument = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Document;
using MSprint = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Sprint;
using MMeeting = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Meeting;
using MWorkTask = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.WorkTask;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        private readonly IMongoCollection<MDocument> _documentCollection;
        private readonly IMongoCollection<MSprint> _sprintCollection;
        private readonly IMongoCollection<MMeeting> _meetingCollection;
        private readonly IMongoCollection<MWorkTask> _workTaskCollection;

        public DocumentRepository(
            IMongoCollection<MDocument> documentCollection, 
            IMongoCollection<MSprint> sprintCollection, 
            IMongoCollection<MMeeting> meetingCollection, 
            IMongoCollection<MWorkTask> workTaskCollection)
        {
            _documentCollection = documentCollection;
            _sprintCollection = sprintCollection;
            _meetingCollection = meetingCollection;
            _workTaskCollection = workTaskCollection;
        }

        public override Identity Create(Document document)
        {
            if (document == null || _documentCollection.Find(x => x.Id == document.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = document.ToPersistenceEntity();
            _documentCollection.InsertOne(entity);
            return document.Id;
        }

        public override Document Read(Identity id)
        {
            if (id.IsBlankIdentity())
            {
                throw CreateEntityNotFoundException(id);
            }

            var entity = _documentCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null)
            {
                throw CreateEntityNotFoundException(id);
            }

            return entity.ToDomainEntity();
        }

        public override void Update(Document document)
        {
            if (document == null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = document.ToPersistenceEntity();
            var result = _documentCollection.ReplaceOne(x => x.Id == entity.Id, entity);

            if (result.MatchedCount != 1)
            {
                throw CreateEntityNotFoundException(document.Id);
            }
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity())
            {
                throw CreateEntityNotFoundException(id);
            }

            var sprintUpdate = Builders<MSprint>.Update
                .Pull(x => x.Documents, id.ToPersistenceIdentity());
            _sprintCollection.UpdateMany(x => true, sprintUpdate);

            var meetingUpdate = Builders<MMeeting>.Update
                .Pull(x => x.Documents, id.ToPersistenceIdentity());
            _meetingCollection.UpdateMany(x => true, meetingUpdate);

            var workTaskUpdate = Builders<MWorkTask>.Update
                .Pull(x => x.Documents, id.ToPersistenceIdentity());
            _workTaskCollection.UpdateMany(x => true, workTaskUpdate);

            var result = _documentCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1)
            {
                throw CreateEntityNotFoundException(id);
            }
        }

        public override bool Exists(Identity id) => _documentCollection.Count(x => x.Id == id.ToPersistenceIdentity()) == 1;

        public override IEnumerable<NavigationInfo> ListAll() => _documentCollection.AsQueryable().Select(x => x.ToInfo());

        public DocumentWithReferences ReadWithReferences(Identity id)
        {
            var doc = Read(id);

            return new DocumentWithReferences
            {
                Document = doc,
                Meetings = _meetingCollection
                    .Find(x => x.Documents.Contains(doc.Id.ToPersistenceIdentity()))
                    .ToEnumerable()
                    .Select(x => x.Id.ToDomainIdentity()),
                Tasks = _workTaskCollection
                    .Find(x => x.Documents.Contains(doc.Id.ToPersistenceIdentity()))
                    .ToEnumerable()
                    .Select(x => x.Id.ToDomainIdentity()),
                Sprints = _sprintCollection
                    .Find(x => x.Documents.Contains(doc.Id.ToPersistenceIdentity()))
                    .ToEnumerable()
                    .Select(x => x.Id.ToDomainIdentity()),
            };
        }

        public IEnumerable<NavigationInfo> ListByKind(Identity projectId, DocumentKind kind)
        {
            var projectFilter = Builders<MDocument>.Filter.Where(x => x.ProjectId == projectId.ToPersistenceIdentity());
            var kindFilter = Builders<MDocument>.Filter.Where(x => x.Kind == kind);

            return _documentCollection.Find(projectFilter & kindFilter).ToEnumerable().Select(x => x.ToInfo());
        }
    }
}
