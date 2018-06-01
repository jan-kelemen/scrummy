using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Domain.UseCases.Implementation.Meeting
{
    internal class ViewMeetingUseCase : IViewMeetingUseCase
    {
        private readonly IMeetingRepository _meetingRepository;

        public ViewMeetingUseCase(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public ViewMeetingResponse Execute(ViewMeetingRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _meetingRepository.Read(request.Id);

            return new ViewMeetingResponse
            {
                Id = entity.Id,
                Description = entity.Description,
                InvolvedPersons = entity.InvolvedPersons,
                Name = entity.Name,
                OrganizedBy = entity.OrganizedBy,
                ProjectId = entity.ProjectId,
                Time = entity.Time,
                Duration = entity.Duration,
                Log = entity.Log,
                Documents = entity.Documents,
            };
        }
    }
}
