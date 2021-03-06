﻿using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.UseCases.Boundary.Requests;
using Scrummy.Domain.UseCases.Boundary.Responses;

namespace Scrummy.Domain.UseCases.Interfaces.WorkTask
{
    public class ViewWorkTaskResponse : BaseResponse
    {
        public ViewWorkTaskResponse() : base(null)
        {
        }

        public Identity Id { get; set; }

        public Identity ProjectId { get; set; }

        public string Name { get; set; }

        public WorkTaskType Type { get; set; }

        public int? StoryPoints { get; set; }

        public string Description { get; set; }

        public Identity ParentTask { get; set; }

        public IEnumerable<Identity> ChildTasks { get; set; }

        public IEnumerable<Identity> Comments { get; set; }

        public IEnumerable<string> Steps { get; set; }

        public IEnumerable<Identity> Documents { get; set; }

        public bool CanEdit { get; set; }

        public bool CanEditParent { get; set; }
    }

    public interface IViewWorkTaskUseCase
    {
        ViewWorkTaskResponse Execute(AuthorizedIdRequest request);
    }
}
