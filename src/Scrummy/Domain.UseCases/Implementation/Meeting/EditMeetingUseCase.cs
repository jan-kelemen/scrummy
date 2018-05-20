using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Domain.UseCases.Implementation.Meeting
{
    public class EditMeetingUseCase : IEditMeetingUseCase
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IPersonRepository _personRepository;

        public EditMeetingUseCase(IMeetingRepository meetingRepository, IPersonRepository personRepository)
        {
            _meetingRepository = meetingRepository;
            _personRepository = personRepository;
        }

        public ConfirmationResponse Execute(EditMeetingRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var persons = CheckIfAllInvolvedPersonsExist(request.InvolvedPersons);
            var entity = _meetingRepository.Read(request.Id);

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.InvolvedPersons = request.InvolvedPersons;
            entity.Time = request.Time;

            _meetingRepository.Update(entity);

            return new ConfirmationResponse("Meeting updated successfully.")
            {
                Id = entity.Id,
            };
        }

        private IEnumerable<Core.Entities.Person> CheckIfAllInvolvedPersonsExist(IEnumerable<Identity> identities)
        {
            return identities.Select(x => _personRepository.Read(x));
        }
    }
}
