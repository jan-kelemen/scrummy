using System.Linq;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Interfaces.Person;

namespace Scrummy.Domain.UseCases.Implementation.Person
{
    public class ViewTeamHistoryUseCase : IViewTeamHistoryUseCase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITeamRepository _teamRepository;

        public ViewTeamHistoryUseCase(IPersonRepository personRepository, ITeamRepository teamRepository)
        {
            _personRepository = personRepository;
            _teamRepository = teamRepository;
        }

        public ViewTeamHistoryResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var person = _personRepository.Read(request.Id);
            var teams = _teamRepository.ReadPersonTeamHistory(person.Id);

            return new ViewTeamHistoryResponse
            {
                Person = new NavigationInfo
                {
                    Id = person.Id,
                    Name = person.DisplayName,
                },
                Teams = teams.Records.Select(x => new ViewTeamHistoryResponse.Team
                {
                    Id = x.RecordId.Item1,
                    Roles = x.RecordId.Item2,
                    To = x.To,
                    From = x.From,
                }),
            };
        }
    }
}
