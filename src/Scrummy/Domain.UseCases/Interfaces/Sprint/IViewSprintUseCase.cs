using System;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.Sprint
{
    public class ViewSprintRequest : AuthorizedRequest
    {
        public ViewSprintRequest(string userId) : base(userId)
        {
        }

        public Identity Id { get; set; }

        protected override void ValidateCore()
        {
            if (Id.IsBlankIdentity())
                AddError("", "Idenitity is invalid.");
        }
    }

    public class ViewSprintResponse : BaseResponse
    {
        public class Story
        {
            public Identity Id { get; set; }

            public bool Completed { get; set; }

            public IEnumerable<Tuple<Identity, SprintBacklog.WorkTaskStatus>> Tasks {get;set;}
        }

        public ViewSprintResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public Tuple<DateTime, DateTime> TimeSpan { get; set; }

        public string Goal { get; set; }

        public IEnumerable<Story> Stories { get; set; }

        public IEnumerable<Identity> Documents { get; set; }

        public bool CanDelete { get; set; }
    }

    public interface IViewSprintUseCase
    {
        ViewSprintResponse Execute(ViewSprintRequest request);
    }
}
