namespace Scrummy.Domain.UseCases.Interfaces.Meeting
{
    public interface IMeetingUseCaseFactory
    {
        ICreateMeetingUseCase Create { get; }

        IEditMeetingUseCase Edit { get; }

        IViewMeetingUseCase View { get; }
    }
}
