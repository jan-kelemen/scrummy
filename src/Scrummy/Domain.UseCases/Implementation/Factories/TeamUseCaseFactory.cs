using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Team;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Team;

namespace Scrummy.Domain.UseCases.Implementation.Factories
{
    internal class TeamUseCaseFactory : ITeamUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public TeamUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateTeamUseCase Create => new CreateTeamUseCase(_repositoryProvider.Team, _repositoryProvider.Person);
    }
}
