using System;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;
using MTeam = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Team;
using Team = Scrummy.Domain.Core.Entities.Team;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        private readonly IMongoCollection<MTeam> _teamCollection;

        public TeamRepository(IMongoCollection<MTeam> teamCollection)
        {
            _teamCollection = teamCollection;
        }

        public Identity CreateTeam(Team team)
        {
            if (team == null || _teamCollection.Find(x => x.Id == team.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = team.ToPersistenceEntity();
            _teamCollection.InsertOne(entity);
            return team.Id;
        }

        public Team ReadTeam(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _teamCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public void UpdateTeam(Team meeting)
        {
            if (meeting == null) { throw CreateInvalidEntityException(); }

            var updateDefinition = Builders<MTeam>.Update
                .Set(p => p.Name, meeting.Name)
                .Set(p => p.TimeOfDailyScrum, meeting.TimeOfDailyScrum);

            var result = _teamCollection.UpdateOne(x => x.Id == meeting.Id.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(meeting.Id); }
        }

        public void UpdateMembers(Team team)
        {
            if (team.Id.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _teamCollection.Find(x => x.Id == team.Id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(team.Id); }

            var currentTeam = entity.CurrentMembers;
            currentTeam.To = DateTime.UtcNow;

            var updateDefinition = Builders<MTeam>.Update
                .Set(p => p.CurrentMembers, new MembersHistoryRecord
                {
                    From = DateTime.UtcNow,
                    To = DateTime.MaxValue,
                    Members = team.Members.Select(m => new MTeam.Member
                    {
                        Id = m.Id.ToPersistenceIdentity(),
                        Role = m.Role,
                    })
                })
                .Push(p => p.MembersHistory, currentTeam);

            var result = _teamCollection.UpdateOne(x => x.Id == team.Id.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(team.Id); }
        }

        public void DeleteTeam(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _teamCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }
    }
}
