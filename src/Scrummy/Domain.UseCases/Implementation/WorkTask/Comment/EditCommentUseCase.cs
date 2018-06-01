using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.WorkTask.Comment;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask.Comment
{
    internal class EditCommentUseCase : IEditCommentUseCase
    {
        private readonly IWorkTaskRepository _workTaskRepository;

        public EditCommentUseCase(IWorkTaskRepository workTaskRepository)
        {
            _workTaskRepository = workTaskRepository;
        }

        public ConfirmationResponse Execute(EditCommentRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var task = _workTaskRepository.Read(request.WorkTaskId);
            var comment = _workTaskRepository.ReadComment(task.Id, request.CommentId);
            comment.Content = request.Content;
            _workTaskRepository.UpdateComment(comment);

            return new ConfirmationResponse("Comment updated successfully.")
            {
                Id = task.Id,
            };
        }
    }
}
