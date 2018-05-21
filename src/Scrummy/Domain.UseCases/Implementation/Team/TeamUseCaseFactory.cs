using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Team
{
    internal class TeamUseCaseFactory : ITeamUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public TeamUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateTeamUseCase Create => new CreateTeamUseCase(_repositoryProvider.Team, _repositoryProvider.Person);
        public IEditTeamUseCase Edit => new EditTeamUseCase(_repositoryProvider.Team, _repositoryProvider.Person);
        public IViewTeamUseCase View => new ViewTeamUseCase(_repositoryProvider.Team, _repositoryProvider.Project);
        public IDeleteTeamUseCase Delete => new DeleteTeamUseCase(_repositoryProvider.Team);
    }
}
