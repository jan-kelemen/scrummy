using System.Linq;
using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Team
{
    internal class ViewMemberHistoryUseCase : IViewMemberHistoryUseCase
    {
        private readonly ITeamRepository _teamRepository;

        public ViewMemberHistoryUseCase(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public ViewMemberHistoryResponse Execute(AuthorizedIdRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var team = _teamRepository.Read(request.Id);
            var history = _teamRepository.ReadTeamPersonHistory(team.Id);

            return new ViewMemberHistoryResponse
            {
                Team = new NavigationInfo
                {
                    Id = team.Id,
                    Name = team.Name,
                },
                Members = history.Records.Select(x => new ViewMemberHistoryResponse.TeamMembers(
                    x.RecordId.Select(y => new ViewMemberHistoryResponse.Member
                    {
                        Id = y.Item1,
                        Role = y.Item2,
                    }))
                {
                    To = x.To,
                    From = x.From,
                }),
            };
        }
    }
}
