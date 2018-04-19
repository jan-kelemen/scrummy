using System;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;

using MProject = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Project;
using MTeam = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Team;
using Project = Scrummy.Domain.Core.Entities.Project;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly IMongoCollection<MProject> _projectCollection;
        private readonly IMongoCollection<MTeam> _teamCollection;

        public ProjectRepository(IMongoCollection<MProject> projectCollection, IMongoCollection<MTeam> teamCollection)
        {
            _projectCollection = projectCollection;
            _teamCollection = teamCollection;
        }

        public override Identity Create(Project project)
        {
            if (project == null || _projectCollection.Find(x => x.Id == project.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = project.ToPersistenceEntity();
            _projectCollection.InsertOne(entity);
            return project.Id;
        }

        public override Project Read(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _projectCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            var team = _teamCollection.Find(x => x.Id == entity.CurrentTeam.TeamId).FirstOrDefault();
            if (team == null) { throw CreateEntityNotFoundException(entity.CurrentTeam.TeamId.ToDomainIdentity()); }

            return entity.ToDomainEntity(team);
        }

        public override void Update(Project project)
        {
            if(project == null) { throw CreateInvalidEntityException(); }

            var updateDefinition = Builders<MProject>.Update
                .Set(p => p.Name, project.Name);

            var result = _projectCollection.UpdateOne(x => x.Id == project.Id.ToPersistenceIdentity(), updateDefinition);

            if(result.MatchedCount != 1) { throw CreateEntityNotFoundException(project.Id); }
        }

        public void UpdateDefinitionOfDone(Identity projectIdentity, DefinitionOfDone definitionOfDone)
        {
            if (projectIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var updateDefinition = Builders<MProject>.Update
                .Set(p => p.DefinitionOfDoneConditions, definitionOfDone);

            var result = _projectCollection.UpdateOne(x => x.Id == projectIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(projectIdentity); }
        }

        public void UpdateTeam(Identity projectIdentity, Identity teamIdentity)
        {
            if (projectIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _projectCollection.Find(x => x.Id == projectIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(projectIdentity); }

            var currentTeam = entity.CurrentTeam;
            currentTeam.To = DateTime.UtcNow;

            var updateDefinition = Builders<MProject>.Update
                .Set(p => p.CurrentTeam, new TeamHistoryRecord
                {
                    From = DateTime.UtcNow,
                    To = DateTime.MaxValue,
                    TeamId = teamIdentity.ToPersistenceIdentity(),
                })
                .Push(p => p.TeamHistory, currentTeam);

            var result = _projectCollection.UpdateOne(x => x.Id == projectIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(projectIdentity); }
        }

        public ProductBacklog GetProductBacklog(Identity projectIdentity)
        {
            if (projectIdentity.IsBlankIdentity()) { throw CreateInvalidEntityException(); }

            var entity = _projectCollection.Find(x => x.Id == projectIdentity.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(projectIdentity); }

            var currentBacklog = entity.CurrentBacklog;

            return new ProductBacklog(
                projectIdentity,
                currentBacklog.Tasks.Select(t => new ProductBacklog.WorkTaskWithStatus(
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

            var currentBacklog = entity.CurrentBacklog;
            currentBacklog.To = DateTime.UtcNow;

            var updateDefinition = Builders<MProject>.Update
                .Set(p => p.CurrentBacklog, new BacklogHistoryRecord
                {
                    From = DateTime.UtcNow,
                    To = DateTime.MaxValue,
                    Tasks = productBacklog.Select(x => new BacklogItem
                    {
                        WorkTaskId = x.WorkTaskId.ToPersistenceIdentity(),
                        Status = x.Status
                    }),
                })
                .Push(p => p.BacklogHistory, currentBacklog);

            var result = _projectCollection.UpdateOne(x => x.Id == projectIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(projectIdentity); }
        }

        public override void Delete(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _projectCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public bool CheckIfProjectWithNameExists(string name) =>
            _projectCollection.Find(x => x.Name == name).FirstOrDefault() != null;
    }
}
