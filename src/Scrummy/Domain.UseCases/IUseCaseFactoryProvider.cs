using Scrummy.Domain.UseCases.Interfaces.Factories;

namespace Scrummy.Domain.UseCases
{
    public interface IUseCaseFactoryProvider
    {
        IPersonUseCaseFactory Person { get; }

        IProjectUseCaseFactory Project { get; }

        ITeamUseCaseFactory Team { get; }

        IMeetingUseCaseFactory Meeting { get; }

        IWorkTaskUseCaseFactory WorkTask { get; }

        ISprintUseCaseFactory Sprint { get; }
    }
}
