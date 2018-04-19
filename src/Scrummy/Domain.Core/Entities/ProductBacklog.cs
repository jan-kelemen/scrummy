using System.Collections;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Core.Entities
{
    public class ProductBacklog : IEnumerable<ProductBacklog.WorkTaskWithStatus>
    {
        public class Validation
        {
            public const string WorkTaskErrorKey = nameof(WorkTask);
            public const string WorkTaskIsInvalid = "Work task in the product backlog has to be specified.";
        }

        public enum WorkTaskStatus
        {
            ToDo, Ready, PlannedInSprint, Impeded, Done
        }

        public class WorkTaskWithStatus
        {
            public WorkTaskWithStatus(Identity workTaskId, WorkTaskStatus status)
            {
                WorkTaskId = workTaskId;
                Status = status;
            }

            public Identity WorkTaskId { get; }

            public WorkTaskStatus Status { get; set; }
        }

        private List<WorkTaskWithStatus> _tasks;

        public ProductBacklog(Identity projectId, IEnumerable<WorkTaskWithStatus> tasks)
        {
            ProjectId = projectId;
            Tasks = tasks;
        }

        public Identity ProjectId { get; }

        public IEnumerable<WorkTaskWithStatus> Tasks
        {
            get => _tasks;
            set => _tasks = new List<WorkTaskWithStatus>(value);
        }

        public IEnumerator<WorkTaskWithStatus> GetEnumerator() => _tasks.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}