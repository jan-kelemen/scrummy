using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Team
{
    internal class EditTeamUseCase : IEditTeamUseCase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IPersonRepository _personRepository;

        public EditTeamUseCase(ITeamRepository teamRepository, IPersonRepository personRepository)
        {
            _teamRepository = teamRepository;
            _personRepository = personRepository;
        }


        public ConfirmationResponse Execute(EditTeamRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var entity = _teamRepository.Read(request.Id);
            entity.Name = request.Name;
            entity.Members = CheckIfAllMembersExist(request.Members);
            entity.TimeOfDailyScrum = request.TimeOfDailyScrum;

            _teamRepository.Update(entity);

            return new ConfirmationResponse("Team updated successfully.")
            {
                Id = entity.Id,
            };
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
