﻿using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Domain.UseCases.Implementation.Meeting
{
    internal class MeetingUseCaseFactory : IMeetingUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public MeetingUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateMeetingUseCase Create => new CreateMeetingUseCase(_repositoryProvider.Meeting, _repositoryProvider.Person, _repositoryProvider.Project);
        public IEditMeetingUseCase Edit => new EditMeetingUseCase(_repositoryProvider.Meeting, _repositoryProvider.Person);
        public IViewMeetingUseCase View => new ViewMeetingUseCase(_repositoryProvider.Meeting);
        public IDeleteMeetingUseCase Delete => new DeleteMeetingUseCase(_repositoryProvider.Meeting);
    }
}
