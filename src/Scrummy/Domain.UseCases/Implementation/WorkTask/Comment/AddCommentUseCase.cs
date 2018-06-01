using Scrummy.Domain.Repositories.Interfaces;
using Scrummy.Domain.UseCases.Boundary.Extensions;
using Scrummy.Domain.UseCases.Boundary.Responses;
using Scrummy.Domain.UseCases.Interfaces.WorkTask.Comment;

namespace Scrummy.Domain.UseCases.Implementation.WorkTask.Comment
{
    internal class AddCommentUseCase : IAddCommentUseCase
    {
        private readonly IWorkTaskRepository _workTaskRepository;

        public AddCommentUseCase(IWorkTaskRepository workTaskRepository)
        {
            _workTaskRepository = workTaskRepository;
        }

        public ConfirmationResponse Execute(AddCommentRequest request)
        {
            request.ThrowExceptionIfInvalid();

            var task = _workTaskRepository.Read(request.WorkTaskId);

            var comment = new Core.Entities.WorkTask.Comment(
                _workTaskRepository.GenerateNewIdentity(), 
                request.AuthorId, 
                task.Id, 
                request.Content);

            _workTaskRepository.AddComment(comment);

            return new ConfirmationResponse("Comment added successfully.")
            {
                Id = task.Id,
            };
        }
    }
}
