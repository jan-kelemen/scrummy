using System;
using System.Linq;
using MongoDB.Driver;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Artifacts;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces.Entities;
using Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities;
using Scrummy.Persistence.Concrete.MongoDB.Mapping.Extensions;

using MProject = Scrummy.Persistence.Concrete.MongoDB.DocumentModel.Entities.Project;
using Project = Scrummy.Domain.Core.Entities.Project;

namespace Scrummy.Persistence.Concrete.MongoDB.Repositories
{
    internal class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private readonly IMongoCollection<MProject> _projectCollection;

        public ProjectRepository(IMongoCollection<MProject> projectCollection)
        {
            _projectCollection = projectCollection;
        }

        public Identity CreateProject(Project project)
        {
            if (project == null || _projectCollection.Find(x => x.Id == project.Id.ToPersistenceIdentity()).FirstOrDefault() != null)
            {
                throw CreateInvalidEntityException();
            }

            var entity = project.ToPersistenceEntity();
            _projectCollection.InsertOne(entity);
            return project.Id;
        }

        public Project ReadProject(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var entity = _projectCollection.Find(x => x.Id == id.ToPersistenceIdentity()).FirstOrDefault();
            if (entity == null) { throw CreateEntityNotFoundException(id); }

            return entity.ToDomainEntity();
        }

        public void UpdateProject(Project project)
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

        public void UpdateTeam(Identity projectIdentity, Team team)
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
                    Members = team.Select(m => new TeamMember
                    {
                        Id = m.Id.ToPersistenceIdentity(),
                        Role = m.Role,
                    }),
                })
                .Push(p => p.TeamHistory, currentTeam);

            var result = _projectCollection.UpdateOne(x => x.Id == projectIdentity.ToPersistenceIdentity(), updateDefinition);

            if (result.MatchedCount != 1) { throw CreateEntityNotFoundException(projectIdentity); }
        }

        public void DeleteProject(Identity id)
        {
            if (id.IsBlankIdentity()) { throw CreateEntityNotFoundException(id); }

            var result = _projectCollection.DeleteOne(x => x.Id == id.ToPersistenceIdentity());

            if (result.DeletedCount != 1) { throw CreateEntityNotFoundException(id); }
        }

        public bool CheckIfProjectWithNameExists(string name) =>
            _projectCollection.Find(x => x.Name == name).FirstOrDefault() != null;
    }
}
