﻿using System;
using System.Linq;
using Scrummy.Domain.Core.Entities;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using MPerson = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Person;
using MProject = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Project;
using MMeeting = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Meeting;
using Person = Scrummy.Domain.Core.Entities.Person;
using Project = Scrummy.Domain.Core.Entities.Project;
using Meeting = Scrummy.Domain.Core.Entities.Meeting;

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
                email: person.Email);
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

        public static Project ToDomainEntity(this MProject project)
        {
            return new Project(
                id: project.Id.ToDomainIdentity(),
                name: project.Name,
                definitionOfDone: new DefinitionOfDone(project.DefinitionOfDoneConditions),
                team: new Team(project.CurrentTeam.Members.Select(
                    m => new Team.Member(m.Id.ToDomainIdentity(), m.Role)))
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
                    Members = project.Team.Select(m => new TeamMember
                    {
                        Id = m.Id.ToPersistenceIdentity(),
                        Role = m.Role,
                    })
                }
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
                involvedPersons: meeting.InvolvedPersons.Select(m => m.ToDomainIdentity()));
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
    }
}
