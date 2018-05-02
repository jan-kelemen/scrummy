using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
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

        public override Identity Create(Team team)
        {
            if (team == null || _teamCollection.Find(x => x.Id == team.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = team.ToPersistenceEntity();
            entity.MembersHistory = new List<MembersHistoryRecord>();
            _teamCollection.InsertOne(entity);
            return team.Id;
        }

        public override Team Read(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _teamCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public override void Update(Team team)
        {
            if (team == null) { throw CreateInvalidEntityException(); }

            var updateDefinition = Builders<MTeam>.Update
                .Set(p => p.Name, team.Name)
                .Set(p => p.TimeOfDailyScrum, team.TimeOfDailyScrum);

            var entity = _teamCollection.Find(x => x.Id == team.Id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(team.Id); }
            var domainEntity = entity.ToDomainEntity();

            var haveTeamMembersChanged = !(team.Members.Count() == domainEntity.Members.Count() && 
                                           team.Members.All(domainEntity.Members.Contains));
            if (haveTeamMembersChanged)
            {
                var currentTeam = entity.CurrentMembers;
                currentTeam.To = DateTime.UtcNow;

                var teamUpdateDefinition = Builders<MTeam>.Update
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

                updateDefinition = Builders<MTeam>.Update.Combine(updateDefinition, teamUpdateDefinition);
            }

            var result = _teamCollection.UpdateOne(x => x.Id == team.Id.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(team.Id); }
        }

        public IEnumerable<Identity> GetTeamsOfPersonAtTimePoint(Identity personId, DateTime timePoint)
        {
            var f = Builders<MTeam>.Filter.Where(x =>
                x.CurrentMembers.Members.Any(y => y.Id == personId.ToPersistenceIdentity()) && x.CurrentMembers.From <= timePoint);
            var f2 = Builders<MTeam>.Filter.Where(x => x.MembersHistory.Any(y =>
                y.Members.Any(z => z.Id == personId.ToPersistenceIdentity()) && y.From >= timePoint && y.To <= timePoint));

            return _teamCollection.Find(f | f2).ToEnumerable().Select(x => x.Id.ToDomainIdentity()).Distinct();
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _teamCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public override IEnumerable<NavigationInfo> ListAll()
        {
            return _teamCollection.AsQueryable().ToList().Select(x => new NavigationInfo
            {
                Id = x.Id.ToDomainIdentity(),
                Name = x.Name,
            });
        }
    }
}
