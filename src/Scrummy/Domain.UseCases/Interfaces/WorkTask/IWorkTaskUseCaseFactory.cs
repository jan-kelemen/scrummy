namespace Scrummy.Domain.UseCases.Interfaces.WorkTask
{
    public interface IWorkTaskUseCaseFactory
    {
        ICreateWorkTaskUseCase Create { get; }

        IEditWorkTaskUseCase Edit { get; }

        IViewWorkTaskUseCase View { get; }
    }
}
