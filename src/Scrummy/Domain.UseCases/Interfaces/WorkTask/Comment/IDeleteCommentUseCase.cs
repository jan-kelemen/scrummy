using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.WorkTask.Comment
{
    public class DeleteCommentRequest : AuthorizedRequest
    {
        public DeleteCommentRequest(string userId) : base(userId)
        {
        }

        public Identity WorkTaskId { get; set; }

        public Identity CommentId { get; set; }

        protected override void ValidateCore()
        {
            if (WorkTaskId.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");

            if (CommentId.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");
        }
    }
    public interface IDeleteCommentUseCase
    {
        ConfirmationResponse Execute(DeleteCommentRequest request);
    }
}
