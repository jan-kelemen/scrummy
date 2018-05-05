using System;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Project;

namespace Scrummy.Domain.UseCases.Implementation.Project
{
    internal class ViewMeetingsUseCase : IViewMeetingsUseCase
    {
        private readonly IMeetingRepository _meetingRepository;

        public ViewMeetingsUseCase(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public ViewMeetingsResponse Execute(ViewMeetingsRequest request)
        {
            request.ThrowExceptionIfInvalid();

            return new ViewMeetingsResponse
            {
                ProjectId = request.ProjectId,
                Meetings = _meetingRepository.GetMeetingsOfProjectInTimeRange(request.ProjectId, request.CurrentTime,
                    DateTime.MaxValue),
            };
        }
    }
}
