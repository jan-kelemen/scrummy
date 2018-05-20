using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Team
{
    internal class CreateTeamUseCase : ICreateTeamUseCase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IPersonRepository _personRepository;

        public CreateTeamUseCase(ITeamRepository teamRepository, IPersonRepository personRepository)
        {
            _teamRepository = teamRepository;
            _personRepository = personRepository;
        }

        public ConfirmationResponse Execute(CreateTeamRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = ToDomainModel(request);
            var result = _teamRepository.Create(entity);

            return new ConfirmationResponse("Team successfully created.")
            {
                Id = result
            };
        }

        private Core.Entities.Team ToDomainModel(CreateTeamRequest request)
        {
            return new Core.Entities.Team(_teamRepository.GenerateNewIdentity(), request.Name, request.TimeOfDailyScrum,
                CheckIfAllMembersExist(request.Members));
        }

        private IEnumerable<Core.Entities.Team.Member> CheckIfAllMembersExist(IEnumerable<Core.Entities.Team.Member> members)
        {
            return members.Select(x =>
            {
                _personRepository.Read(x.Id);
                return x;
            });
        }
    }
}
