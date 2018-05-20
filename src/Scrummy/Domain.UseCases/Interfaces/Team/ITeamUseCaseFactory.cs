namespace Scrummy.Domain.UseCases.Interfaces.Team
{
    public interface ITeamUseCaseFactory
    {
        ICreateTeamUseCase Create { get; }

        IEditTeamUseCase Edit { get; }

        IViewTeamUseCase View { get; }

        IDeleteTeamUseCase Team { get; }
    }
}
