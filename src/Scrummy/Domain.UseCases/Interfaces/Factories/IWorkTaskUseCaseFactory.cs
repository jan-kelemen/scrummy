using Scrummy.Domain.UseCases.Interfaces.WorkTask;

namespace Scrummy.Domain.UseCases.Interfaces.Factories
{
    public interface IWorkTaskUseCaseFactory
    {
        ICreateWorkTaskUseCase Create { get; }

        IEditWorkTaskUseCase Edit { get; }

        IViewWorkTaskUseCase View { get; }
    }
}
