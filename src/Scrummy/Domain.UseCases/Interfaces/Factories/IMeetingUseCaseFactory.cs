using Scrummy.Domain.UseCases.Interfaces.Meeting;

namespace Scrummy.Domain.UseCases.Interfaces.Factories
{
    public interface IMeetingUseCaseFactory
    {
        ICreateMeetingUseCase Create { get; }

        IEditMeetingUseCase Edit { get; }

        IViewMeetingUseCase View { get; }
    }
}
