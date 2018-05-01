using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Interfaces.Factories
{
    public interface ITeamUseCaseFactory
    {
        ICreateTeamUseCase Create { get; }

        IViewTeamUseCase View { get; }
    }
}
