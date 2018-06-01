using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.WorkTask.Comment;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask.Comment
{
    internal class DeleteCommentUseCase : IDeleteCommentUseCase
    {
        private readonly IWorkTaskRepository _workTaskRepository;

        public DeleteCommentUseCase(IWorkTaskRepository workTaskRepository)
        {
            _workTaskRepository = workTaskRepository;
        }

        public ConfirmationResponse Execute(DeleteCommentRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var task = _workTaskRepository.Read(request.WorkTaskId);
            _workTaskRepository.DeleteComment(task.Id, request.CommentId);

            return new ConfirmationResponse("Comment deleted successfully.")
            {
                Id = task.Id,
            };
        }
    }
}
