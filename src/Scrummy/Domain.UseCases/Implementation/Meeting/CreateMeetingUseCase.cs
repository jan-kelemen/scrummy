using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Exceptions;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Domain.UseCases.Implementation.Meeting
{
    public class CreateMeetingUseCase : ICreateMeetingUseCase
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProjectRepository _projectRepository;

        public CreateMeetingUseCase(IMeetingRepository meetingRepository, IPersonRepository personRepository, IProjectRepository projectRepository)
        {
            _meetingRepository = meetingRepository;
            _personRepository = personRepository;
            _projectRepository = projectRepository;
        }

        public ConfirmationResponse Execute(CreateMeetingRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var projectExists = _projectRepository.Exists(request.ProjectId);
            if(!projectExists)
                throw new UseCaseException("Project doesn't exist.");

            var personsExist = request.InvolvedPersons.Concat(new [] {request.OrganizedBy}).All(x => _personRepository.Exists(x));
            if(!personsExist)
                throw new UseCaseException("One or more users don't exist.");

            var entity = ToDomainEntity(request);
            var result = _meetingRepository.Create(entity);

            return new ConfirmationResponse("Meeting created successfully.")
            {
                Id = result,
            };
        }

        private Core.Entities.Meeting ToDomainEntity(CreateMeetingRequest request)
        {
            return new Core.Entities.Meeting(_meetingRepository.GenerateNewIdentity(), 
                request.ProjectId, 
                request.Name, 
                request.Time, 
                request.OrganizedBy, 
                request.Description, 
                request.InvolvedPersons);
        }
    }
}
