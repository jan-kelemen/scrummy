using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;
using System;
using System.Collections.Generic;

namespace Scrummy.Domain.UseCases.Interfaces.Meeting
{
    public class ViewMeetingResponse : BaseResponse
    {
        public ViewMeetingResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public Identity OrganizedBy { get; set; }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public DateTime Time { get; set; }

        public TimeSpan Duration { get; set; }

        public string Description { get; set; }

        public string Log { get; set; }

        public IEnumerable<Identity> InvolvedPersons { get; set; }

        public bool CanDelete = true;

        public IEnumerable<Identity> Documents { get; set; }
    }

    public interface IViewMeetingUseCase
    {
        ViewMeetingResponse Execute(AuthorizedIdRequest request);
    }
}
