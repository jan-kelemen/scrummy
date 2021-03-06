﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MongoDB.Bson;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using MPerson = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Person;
using MProject = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Project;
using MTeam = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Team;
using MMeeting = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Meeting;
using MSprint = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Sprint;
using MWorkTask = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.WorkTask;
using MComment = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Comment;
using MDocument = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Document;
using Person = Scrummy.Domain.Core.Entities.Person;
using Project = Scrummy.Domain.Core.Entities.Project;
using Team = Scrummy.Domain.Core.Entities.Team;
using Meeting = Scrummy.Domain.Core.Entities.Meeting;
using Sprint = Scrummy.Domain.Core.Entities.Sprint;
using WorkTask = Scrummy.Domain.Core.Entities.WorkTask;
using Comment = Scrummy.Domain.Core.Entities.WorkTask.Comment;
using Document = Scrummy.Domain.Core.Entities.Document;

// ReSharper disable ArgumentsStyleOther
// ReSharper disable ArgumentsStyleNamedExpression

namespace Scrummy.Persistence.Concrete.MongoDB.Extensions
{
    internal static class RepositoryMappingExtensions
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
                PasswordHash = person.PasswordHash,
            };
        }

        public static Project ToDomainEntity(this MProject project, IEnumerable<ObjectId> documents)
        {
            return new Project(
                id: project.Id.ToDomainIdentity(),
                name: project.Name,
                description: project.Description,
                definitionOfDone: new DefinitionOfDone(project.DefinitionOfDoneConditions),
                teamId: project.CurrentTeam.TeamId.ToDomainIdentity(),
                documents: documents.Select(x => x.ToDomainIdentity())
            );
        }

        public static MProject ToPersistenceEntity(this Project project)
        {
            return new MProject
            {
                Id = project.Id.ToPersistenceIdentity(),
                Name = project.Name,
                Description = project.Description,
                DefinitionOfDoneConditions = project.DefinitionOfDone,
                CurrentTeam = new TeamHistoryRecord
                {
                    From = DateTime.MinValue,
                    To = DateTime.MaxValue,
                    TeamId = project.TeamId.ToPersistenceIdentity(),
                },
                Backlog = new MProject.BacklogItem[0]
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
                time: DateTime.ParseExact(meeting.Time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                duration: meeting.Duration,
                organizedBy: meeting.OrganizedBy.ToDomainIdentity(),
                description: meeting.Description,
                log: meeting.Log,
                involvedPersons: meeting.InvolvedPersons.Select(m => m.ToDomainIdentity()),
                documents: meeting.Documents.Select(x => x.ToDomainIdentity())
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
                Time = meeting.Time.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Duration = meeting.Duration,
                InvolvedPersons = meeting.InvolvedPersons.Select(x => x.ToPersistenceIdentity()),
                Log = meeting.Log,
                Documents = meeting.Documents.Select(x => x.ToPersistenceIdentity())
            };
        }

        public static Sprint ToDomainEntity(this MSprint sprint)
        {
            return new Sprint(
                id: sprint.Id.ToDomainIdentity(),
                projectId: sprint.ProjectId.ToDomainIdentity(),
                name: sprint.Name,
                timeSpan: new Tuple<DateTime, DateTime>(
                    DateTime.ParseExact(sprint.StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture), 
                    DateTime.ParseExact(sprint.EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    ),
                goal: sprint.Goal,
                status: sprint.Status,
                documents: sprint.Documents.Select(x => x.ToDomainIdentity())
            );
        }

        public static MSprint ToPersistenceEntity(this Sprint sprint)
        {
            return new MSprint
            {
                Id = sprint.Id.ToPersistenceIdentity(),
                ProjectId = sprint.ProjectId.ToPersistenceIdentity(),
                Name = sprint.Name,
                StartDate = sprint.TimeSpan.Item1.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                EndDate = sprint.TimeSpan.Item2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Goal = sprint.Goal,
                Status = sprint.Status,
                Documents = sprint.Documents.Select(x => x.ToPersistenceIdentity())
            };
        }

        public static WorkTask ToDomainEntity(this MWorkTask task, IEnumerable<ObjectId> childTasks)
        {
            return new WorkTask(
                id: task.Id.ToDomainIdentity(),
                projectId: task.ProjectId.ToDomainIdentity(),
                type: task.Type,
                name: task.Name,
                storyPoints: task.StoryPoints,
                description: task.Description,
                childTasks: childTasks.Select(i => i.ToDomainIdentity()),
                parentTask: task.ParentTask.ToDomainIdentity(),
                comments: task.Comments.Select(i => i.Id.ToDomainIdentity()),
                steps: task.Steps,
                documents: task.Documents.Select(x => x.ToDomainIdentity())
            );
        }

        public static MWorkTask ToPersistenceEntity(this WorkTask task)
        {
            return new MWorkTask
            {
                Id = task.Id.ToPersistenceIdentity(),
                ProjectId = task.ProjectId.ToPersistenceIdentity(),
                Name = task.Name,
                Type = task.Type,
                StoryPoints = task.StoryPoints,
                Description = task.Description,
                ParentTask = task.ParentTask.ToPersistenceIdentity(),
                Steps = task.Steps,
                Documents = task.Documents.Select(x => x.ToPersistenceIdentity())
            };
        }

        public static Comment ToDomainEntity(this MComment comment, ObjectId workTaskId)
        {
            return new Comment(
                id: comment.Id.ToDomainIdentity(),
                authorId: comment.AuthorId.ToDomainIdentity(),
                workTaskId: workTaskId.ToDomainIdentity(),
                content: comment.Content
            );
        }

        public static MComment ToPersistenceEntity(this Comment comment)
        {
            return new MComment
            {
                Id = comment.Id.ToPersistenceIdentity(),
                AuthorId = comment.AuthorId.ToPersistenceIdentity(),
                Content = comment.Content,
            };
        }

        public static Document ToDomainEntity(this MDocument document)
        {
            return new Document(
                id: document.Id.ToDomainIdentity(),
                kind: document.Kind,
                name: document.Name,
                projectId: document.ProjectId.ToDomainIdentity(),
                links: document.Links,
                content: document.Content);
        }

        public static MDocument ToPersistenceEntity(this Document document)
        {
            return new MDocument
            {
                Id = document.Id.ToPersistenceIdentity(),
                Name = document.Name,
                ProjectId = document.Project.ToPersistenceIdentity(),
                Content = document.Content,
                Kind = document.Kind,
                Links = document.Links,
            };
        }
    }
}
