using System;
using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using MPerson = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Person;
using MProject = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Project;
using MTeam = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Team;
using MMeeting = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Meeting;
using MSprint = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Sprint;
using Person = Scrummy.Domain.Core.Entities.Person;
using Project = Scrummy.Domain.Core.Entities.Project;
using Team = Scrummy.Domain.Core.Entities.Team;
using Meeting = Scrummy.Domain.Core.Entities.Meeting;
using Sprint = Scrummy.Domain.Core.Entities.Sprint;

// ReSharper disable ArgumentsStyleOther
// ReSharper disable ArgumentsStyleNamedExpression

namespace Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions
{
    internal static class EntityMappingExtensions
    {
        public static Person ToDomainEntity(this MPerson person)
        {
            return new Person(
                id: person.Id.ToDomainIdentity(),
                firstName: person.FirstName,
                lastName: person.LastName,
                displayName: person.DisplayName,
                email: person.Email
            );
        }

        public static MPerson ToPersistenceEntity(this Person person)
        {
            return new MPerson
            {
                Id = person.Id.ToPersistenceIdentity(),
                FirstName = person.FirstName,
                LastName = person.LastName,
                DisplayName = person.DisplayName,
                Email = person.Email,
            };
        }

        public static Project ToDomainEntity(this MProject project, MTeam team)
        {
            return new Project(
                id: project.Id.ToDomainIdentity(),
                name: project.Name,
                definitionOfDone: new DefinitionOfDone(project.DefinitionOfDoneConditions),
                team: team.ToDomainEntity()
            );
        }

        public static MProject ToPersistenceEntity(this Project project)
        {
            return new MProject
            {
                Id = project.Id.ToPersistenceIdentity(),
                Name = project.Name,
                DefinitionOfDoneConditions = project.DefinitionOfDone,
                CurrentTeam = new TeamHistoryRecord
                {
                    From = DateTime.UtcNow,
                    To = DateTime.MaxValue,
                    TeamId = project.Team.Id.ToPersistenceIdentity(),
                }
            };
        }

        public static Team ToDomainEntity(this MTeam team)
        {
            return new Team(
                id: team.Id.ToDomainIdentity(),
                name: team.Name,
                timeOfDailyScrum: team.TimeOfDailyScrum,
                members: team.CurrentMembers.Members.Select(m => new Team.Member(m.Id.ToDomainIdentity(), m.Role))
            );
        }

        public static MTeam ToPersistenceEntity(this Team team)
        {
            return new MTeam
            {
                Id = team.Id.ToPersistenceIdentity(),
                Name = team.Name,
                TimeOfDailyScrum = team.TimeOfDailyScrum,
                CurrentMembers = new MembersHistoryRecord
                {
                    From = DateTime.Now,
                    To = DateTime.MaxValue,
                    Members = team.Members.Select(m => new MTeam.Member
                    {
                        Id = m.Id.ToPersistenceIdentity(),
                        Role = m.Role,
                    })
                },
            };
        }

        public static Meeting ToDomainEntity(this MMeeting meeting)
        {
            return new Meeting(
                id: meeting.Id.ToDomainIdentity(),
                projectId: meeting.ProjectId.ToDomainIdentity(),
                name: meeting.Name,
                time: meeting.Time,
                organizedBy: meeting.OrganizedBy.ToDomainIdentity(),
                description: meeting.Description,
                involvedPersons: meeting.InvolvedPersons.Select(m => m.ToDomainIdentity())
            );
        }

        public static MMeeting ToPersistenceEntity(this Meeting meeting)
        {
            return new MMeeting
            {
                Id = meeting.Id.ToPersistenceIdentity(),
                Name = meeting.Name,
                ProjectId = meeting.ProjectId.ToPersistenceIdentity(),
                Description = meeting.Description,
                OrganizedBy = meeting.OrganizedBy.ToPersistenceIdentity(),
                Time = meeting.Time,
                InvolvedPersons = meeting.InvolvedPersons.Select(x => x.ToPersistenceIdentity()),
            };
        }

        public static Sprint ToDomainEntity(this MSprint sprint)
        {
            return new Sprint(
                id: sprint.Id.ToDomainIdentity(),
                projectId: sprint.ProjectId.ToDomainIdentity(),
                name: sprint.Name,
                timeSpan: new Tuple<DateTime, DateTime>(sprint.StartDate, sprint.EndDate),
                goal: sprint.Goal
            );
        }

        public static MSprint ToPersistenceEntity(this Sprint sprint)
        {
            return new MSprint
            {
                Id = sprint.Id.ToPersistenceIdentity(),
                ProjectId = sprint.ProjectId.ToPersistenceIdentity(),
                Name = sprint.Name,
                StartDate = sprint.TimeSpan.Item1,
                EndDate = sprint.TimeSpan.Item2,
                Goal = sprint.Goal,
            };
        }
    }
}
