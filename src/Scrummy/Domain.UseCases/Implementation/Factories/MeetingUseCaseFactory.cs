using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.Meeting;
using Scrummy.Domain.UseCases.Interfaces.Factories;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Domain.UseCases.Implementation.Factories
{
    internal class MeetingUseCaseFactory : IMeetingUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public MeetingUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateMeetingUseCase Create => 
            new CreateMeetingUseCase(_repositoryProvider.Meeting, _repositoryProvider.Person, _repositoryProvider.Project);
    }
}
