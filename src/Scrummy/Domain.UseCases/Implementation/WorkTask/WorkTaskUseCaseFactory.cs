using Scrummy.Domain.Repositories;
using Scrummy.Domain.UseCases.Implementation.WorkTask.Comment;
using Scrummy.Domain.UseCases.Interfaces.WorkTask;
using Scrummy.Domain.UseCases.Interfaces.WorkTask.Comment;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask
{
    internal class WorkTaskUseCaseFactory : IWorkTaskUseCaseFactory
    {
        private readonly IRepositoryProvider _repositoryProvider;

        public WorkTaskUseCaseFactory(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

        public ICreateWorkTaskUseCase Create => new CreateWorkTaskUseCase(_repositoryProvider.WorkTask, _repositoryProvider.Project);
        public IEditWorkTaskUseCase Edit => new EditWorkTaskUseCase(_repositoryProvider.WorkTask, _repositoryProvider.Project);
        public IViewWorkTaskUseCase View => new ViewWorkTaskUseCase(_repositoryProvider.WorkTask, _repositoryProvider.Project);
        public IDeleteWorkTaskUseCase Delete => new DeleteWorkTaskUseCase(_repositoryProvider.WorkTask,  _repositoryProvider.Project);
        public IAddCommentUseCase AddComment => new AddCommentUseCase(_repositoryProvider.WorkTask);
        public IEditCommentUseCase EditComment => new EditCommentUseCase(_repositoryProvider.WorkTask);
        public IDeleteCommentUseCase DeleteComment => new DeleteCommentUseCase(_repositoryProvider.WorkTask);
    }
}
