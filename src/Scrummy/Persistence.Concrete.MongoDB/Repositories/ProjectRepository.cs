using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.Repositories.Interfaces.DTO;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;

using MProject = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Project;
using MMeeting = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Meeting;
using MSprint = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Sprint;
using MWorkTask = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.WorkTask;

using Project = Scrummy.Domain.Core.Entities.Project;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly IMongoCollection<MProject> _projectCollection;
        private readonly IMongoCollection<MMeeting> _meetingCollection;
        private readonly IMongoCollection<MSprint> _sprintCollection;
        private readonly IMongoCollection<MWorkTask> _workTaskCollection;

        public ProjectRepository(IMongoCollection<MProject> projectCollection, IMongoCollection<MMeeting> meetingCollection, IMongoCollection<MSprint> sprintCollection, IMongoCollection<MWorkTask> workTaskCollection)
        {
            _projectCollection = projectCollection;
            _meetingCollection = meetingCollection;
            _sprintCollection = sprintCollection;
            _workTaskCollection = workTaskCollection;
        }

        public override Identity Create(Project project)
        {
            if (project == null || _projectCollection.Find(x => x.Id == project.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = project.ToPersistenceEntity();
            entity.TeamHistory = new List<TeamHistoryRecord>();
            entity.BacklogHistory = new List<MProject.BacklogHistoryRecord>();
            _projectCollection.InsertOne(entity);
            return project.Id;
        }

        public override Project Read(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _projectCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public override void Update(Project project)
        {
            if (project == null) { throw CreateInvalidEntityException(); }

            var updateDefinition = Builders<MProject>.Update
                .Set(p => p.Name, project.Name)
                .Set(p => p.DefinitionOfDoneConditions, project.DefinitionOfDone);

            var entity = _projectCollection.Find(x => x.Id == project.Id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(project.Id); }
            var hasTeamChanged = project.TeamId.ToPersistenceIdentity() != entity.CurrentTeam.TeamId;
            if (hasTeamChanged)
            {
                var currentTeam = entity.CurrentTeam;
                currentTeam.To = DateTime.Now;

                var updateTeam = Builders<MProject>.Update
                    .Set(p => p.CurrentTeam, new TeamHistoryRecord
                    {
                        From = DateTime.Now,
                        To = DateTime.MaxValue,
                        TeamId = project.TeamId.ToPersistenceIdentity(),
                    })
                    .Push(p => p.TeamHistory, currentTeam);

                updateDefinition = Builders<MProject>.Update.Combine(updateDefinition, updateTeam);
            }

            var result = _projectCollection.UpdateOne(x => x.Id == project.Id.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(project.Id); }
        }

        public ProductBacklog ReadProductBacklog(Identity projectIdentity)
        {
            if (projectIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _projectCollection.Find(x => x.Id == projectIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(projectIdentity); }

            return new ProductBacklog(
                projectIdentity,
                entity.Backlog.Select(t => new ProductBacklog.WorkTaskWithStatus(
                    t.WorkTaskId.ToDomainIdentity(),
                    t.Status)
                )
            );
        }

        public void UpdateProductBacklog(ProductBacklog productBacklog)
        {
            var projectIdentity = productBacklog.ProjectId;
            if (projectIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _projectCollection.Find(x => x.Id == projectIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(projectIdentity); }

            var historyRecord = new MProject.BacklogHistoryRecord
            {
                DoneTasks = productBacklog.Count(x => x.Status == ProductBacklog.WorkTaskStatus.Done),
                InSprintTasks = productBacklog.Count(x => x.Status == ProductBacklog.WorkTaskStatus.InSprint),
                ReadyTasks = productBacklog.Count(x => x.Status == ProductBacklog.WorkTaskStatus.Ready),
                ToDoTasks = productBacklog.Count(x => x.Status == ProductBacklog.WorkTaskStatus.ToDo),
            };

            var updateDefinition = Builders<MProject>.Update
                .Set(p => p.Backlog, productBacklog.Select(x => new MProject.BacklogItem
                {
                    WorkTaskId = x.WorkTaskId.ToPersistenceIdentity(),
                    Status = x.Status
                }))
                .Push(p => p.BacklogHistory, historyRecord);

            var result = _projectCollection.UpdateOne(x => x.Id == projectIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(projectIdentity); }
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _projectCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());
            _meetingCollection.DeleteMany(x => x.ProjectId == id.ToPersistenceIdentity());
            _sprintCollection.DeleteMany(x => x.ProjectId == id.ToPersistenceIdentity());
            _workTaskCollection.DeleteMany(x => x.ProjectId == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public override bool Exists(Identity id) => _projectCollection.Count(x => x.Id == id.ToPersistenceIdentity()) == 1;

        public override IEnumerable<NavigationInfo> ListAll()
        {
            return _projectCollection.AsQueryable().ToList().Select(x => new NavigationInfo
            {
                Id = x.Id.ToDomainIdentity(),
                Name = x.Name,
            });
        }

        public bool CheckIfProjectWithNameExists(string name) =>
            _projectCollection.Find(x => x.Name == name).FirstOrDefault() != null;

        public IEnumerable<Identity> GetProjectsOfTeamAtTimePoint(Identity teamId, DateTime timePoint)
        {
            var f = Builders<MProject>.Filter.Where(x =>
                x.CurrentTeam.TeamId == teamId.ToPersistenceIdentity() && x.CurrentTeam.From <= timePoint);
            var f2 = Builders<MProject>.Filter.Where(x => x.TeamHistory.Any(y =>
                y.TeamId == teamId.ToPersistenceIdentity() && y.From >= timePoint && y.To <= timePoint));

            return _projectCollection.Find(f | f2).ToEnumerable().Select(x => x.Id.ToDomainIdentity()).Distinct();
        }

        public HistoryDTO<Identity> ReadProjectTeamHistory(Identity projectId)
        {
            if (projectId.IsBlankIdentity()) { throw CreateEntityNotFoundException(projectId); }

            var entity = _projectCollection.Find(x => x.Id == projectId.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(projectId); }

            var history = new[] { entity.CurrentTeam }.Concat(entity.TeamHistory).OrderByDescending(x => x.From);

            return new HistoryDTO<Identity>
            {
                Id = entity.Id.ToDomainIdentity(),
                Records = history.Select(x => new HistoryDTO<Identity>.Record
                {
                    From = x.From,
                    To = x.To,
                    RecordId = x.TeamId.ToDomainIdentity(),
                })
            };
        }

        public HistoryDTO<Identity> ReadTeamProjectHistory(Identity teamId)
        {
            var f = Builders<MProject>.Filter.Where(x =>
                x.CurrentTeam.TeamId == teamId.ToPersistenceIdentity());
            var f2 = Builders<MProject>.Filter.Where(x => x.TeamHistory.Any(y =>
                y.TeamId == teamId.ToPersistenceIdentity()));

            var records = new List<HistoryDTO<Identity>.Record>();

            foreach (var project in _projectCollection.Find(f | f2).ToEnumerable())
            {
                if (project.CurrentTeam.TeamId == teamId.ToPersistenceIdentity())
                {
                    records.Add(new HistoryDTO<Identity>.Record
                    {
                        From = project.CurrentTeam.From,
                        To = project.CurrentTeam.To,
                        RecordId = project.Id.ToDomainIdentity(),
                    });
                }

                records.AddRange(project.TeamHistory.Where(x => x.TeamId == teamId.ToPersistenceIdentity())
                    .Select(record => new HistoryDTO<Identity>.Record
                    {
                        From = record.From,
                        To = record.To,
                        RecordId = project.Id.ToDomainIdentity(),
                    }));
            }

            return new HistoryDTO<Identity>
            {
                Id = teamId,
                Records = records.OrderByDescending(x => x.From),
            };
        }
    }
}
