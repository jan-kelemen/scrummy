using Scrummy.Domain.UseCases.Interfaces.WorkTask.Comment;

namespace Scrummy.Domain.UseCases.Interfaces.WorkTask
{
    public interface IWorkTaskUseCaseFactory
    {
        ICreateWorkTaskUseCase Create { get; }

        IEditWorkTaskUseCase Edit { get; }

        IViewWorkTaskUseCase View { get; }

        IDeleteWorkTaskUseCase Delete { get; }

        IAddCommentUseCase AddComment { get; }

        IEditCommentUseCase EditComment { get; }

        IDeleteCommentUseCase DeleteComment { get; }
    }
}
