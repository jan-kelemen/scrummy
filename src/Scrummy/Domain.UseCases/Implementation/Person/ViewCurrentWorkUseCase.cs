using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Implementation.Person
{
    internal class ViewCurrentWorkUseCase : IViewCurrentWorkUseCase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly ITeamRepository _teamRepository;

        public ViewCurrentWorkUseCase(IProjectRepository projectRepository, IMeetingRepository meetingRepository, ITeamRepository teamRepository)
        {
            _projectRepository = projectRepository;
            _meetingRepository = meetingRepository;
            _teamRepository = teamRepository;
        }

        public ViewCurrentWorkResponse Execute(ViewCurrentWorkRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var currentProjects = GetCurrentProjects(request.ForUserId, request.CurrentTime);
            var upcomingMeetings = GetUpcomingMeetings(request.ForUserId, request.CurrentTime);

            return new ViewCurrentWorkResponse
            {
                CurrentProjects = currentProjects,
                UpcomingMeetings = upcomingMeetings,
            };
        }

        private IEnumerable<Identity> GetCurrentProjects(Identity personId, DateTime currentTime)
        {
            var currentTeams = _teamRepository.GetTeamsOfPersonAtTimePoint(personId, currentTime);

            var currentProjects = new List<Identity>();
            foreach (var currentTeam in currentTeams)
            {
                var projects = _projectRepository.GetProjectsOfTeamAtTimePoint(currentTeam, currentTime);
                currentProjects.AddRange(projects);
            }

            return currentProjects;
        }

        private IEnumerable<Identity> GetUpcomingMeetings(Identity personId, DateTime currentTime) =>
            _meetingRepository.GetMeetingsOfPersonInTimeRange(personId, currentTime, DateTime.MaxValue);
    }
}
