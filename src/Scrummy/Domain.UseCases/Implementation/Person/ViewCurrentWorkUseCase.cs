using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Implementation.Person
{
    internal class ViewCurrentWorkUseCase : IViewCurrentWorkUseCase
    {
        private readonly IProjectRepository _projectRepository;

        private readonly IMeetingRepository _meetingRepository;

        private readonly ITeamRepository _teamRepository;

        public ViewCurrentWorkUseCase(IRepositoryProvider repositoryProvider)
        {
            _projectRepository = repositoryProvider.Project;
            _meetingRepository = repositoryProvider.Meeting;
            _teamRepository = repositoryProvider.Team;
        }

        public ViewCurrentWorkResponse Execute(ViewCurrentWorkRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var currentProjects = GetCurrentProjects(Identity.FromString(request.ForUserId), request.CurrentTime);
            var upcomingMeetings = GetUpcomingMeetings(Identity.FromString(request.ForUserId), request.CurrentTime);

            return new ViewCurrentWorkResponse
            {
                CurrentProjects = currentProjects,
                UpcomingMeetings = upcomingMeetings,
            };
        }

        private IEnumerable<Identity> GetCurrentProjects(Identity personId, DateTime currentTime)
        {
            var currentTeams = _teamRepository
                .GetTeamsOfPersonsAtTimePoint(new[] { personId }, currentTime)
                .Select(x => x.teamId)
                .Distinct();

            var currentProjects = _projectRepository
                .GetProjectsOfTeamsAtTimePoint(currentTeams, currentTime)
                .Select(x => x.projectId)
                .Distinct();

            return currentProjects;
        }

        private IEnumerable<Identity> GetUpcomingMeetings(Identity personId, DateTime currentTime) =>
            _meetingRepository.GetMeetingsOfPersonInTimeRange(personId, currentTime, DateTime.MaxValue);
    }
}
