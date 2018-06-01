using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.WorkTask.Comment
{
    public class AddCommentRequest : AuthorizedRequest
    {
        public AddCommentRequest(string userId) : base(userId)
        {
        }

        public Identity WorkTaskId { get; set; }

        public Identity AuthorId { get; set; }

        public string Content { get; set; }

        protected override void ValidateCore()
        {
            if (WorkTaskId.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");

            if (AuthorId.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");

            if (!TextValidator.ValidateThatContentIsBetweenSpecifiedLength(Content, 1))
                AddError("", "Content of the comment is invalid.");
        }
    }

    public interface IAddCommentUseCase
    {
        ConfirmationResponse Execute(AddCommentRequest request);
    }
}
