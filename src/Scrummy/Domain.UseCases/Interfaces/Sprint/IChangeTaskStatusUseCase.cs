using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public class ChangeTaskStatusRequest : AuthorizedRequest
    {
        public ChangeTaskStatusRequest(string userId) : base(userId)
        {
        }

        public Identity SprintId { get; set; }

        public Identity TaskId { get; set; }

        public SprintBacklog.WorkTaskStatus Status { get; set; }

        protected override void ValidateCore()
        {
            if (SprintId.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");

            if (TaskId.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");
        }
    }

    public class ChangeTaskStatusResponse : BaseResponse
    {
        public ChangeTaskStatusResponse(string message) : base(message)
        {
        }

        public Identity ProjectId { get; set; }
    }

    public interface IChangeTaskStatusUseCase
    {
        ChangeTaskStatusResponse Execute(ChangeTaskStatusRequest request);
    }
}
