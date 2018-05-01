using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;
using MMeeting = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Meeting;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
    {
        private readonly IMongoCollection<MMeeting> _meetingCollection;

        public MeetingRepository(IMongoCollection<MMeeting> meetingCollection)
        {
            _meetingCollection = meetingCollection;
        }

        public override Identity Create(Meeting meeting)
        {
            if (meeting == null || _meetingCollection.Find(x => x.Id == meeting.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = meeting.ToPersistenceEntity();
            _meetingCollection.InsertOne(entity);
            return meeting.Id;
        }

        public override Meeting Read(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _meetingCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public override void Update(Meeting meeting)
        {
            if (meeting == null) { throw CreateInvalidEntityException(); }

            var entity = meeting.ToPersistenceEntity();
            var result = _meetingCollection.ReplaceOne(x => x.Id == entity.Id, entity);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(meeting.Id); }
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _meetingCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public override IEnumerable<NavigationInfo> ListAll()
        {
            return _meetingCollection.AsQueryable().ToList().Select(x => new NavigationInfo
            {
                Id = x.Id.ToDomainIdentity(),
                Name = x.Name,
            });
        }


        public IEnumerable<Identity> GetMeetingsOfPersonInTimeRange(Identity personId, DateTime fromTime, DateTime toTime)
        {
            var idFilter = Builders<MMeeting>.Filter.
                Where(x => x.InvolvedPersons.Contains(personId.ToPersistenceIdentity()));

            var timeFilter = Builders<MMeeting>.Filter
                .Where(x => fromTime >= x.Time && x.Time <= toTime);

            return _meetingCollection.Find(idFilter & timeFilter).ToEnumerable().Select(x => x.Id.ToDomainIdentity());
        }
    }
}
