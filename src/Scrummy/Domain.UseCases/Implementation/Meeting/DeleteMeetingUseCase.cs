using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Domain.UseCases.Implementation.Meeting
{
    internal class DeleteMeetingUseCase : IDeleteMeetingUseCase
    {
        private readonly IMeetingRepository _meetingRepository;

        public DeleteMeetingUseCase(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public ConfirmationResponse Execute(DeleteMeetingRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var meeting = _meetingRepository.Read(request.Id);
            _meetingRepository.Delete(meeting.Id);

            return new ConfirmationResponse($"Meeting {meeting.Name} successfully deleted.")
            {
                Id = meeting.Id,
            };
        }
    }
}
