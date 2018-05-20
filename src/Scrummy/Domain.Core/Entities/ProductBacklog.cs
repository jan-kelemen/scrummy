using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Exceptions;

namespace Scrummy.Domain.Core.Entities
{
    public class ProductBacklog : IEnumerable<ProductBacklog.WorkTaskWithStatus>
    {
        public class Validation
        {
            public const string WorkTaskErrorKey = nameof(WorkTask);
            public const string WorkTaskIsInvalid = "Work task in the product backlog has to be specified.";
            public const string WorkTaskIsAlreadyInBacklog = "Work task is already in the product backlog.";
        }

        public enum WorkTaskStatus
        {
            ToDo, Ready, InSprint, Done
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

        public void AddTaskToBacklog(WorkTaskWithStatus task)
        {
            if (IsTaskInBacklog(task.WorkTaskId))
                throw new EntityException(Validation.WorkTaskIsAlreadyInBacklog)
                {
                    Identity = ProjectId,
                    EntityName = nameof(Project),
                };

            _tasks.Add(task);
        }

        public void UpdateTask(WorkTaskWithStatus task)
        {
            var backlogTask = _tasks.First(x => x.WorkTaskId == task.WorkTaskId);
            backlogTask.Status = task.Status;
        }

        public bool RemoveTaskFromBacklog(Identity task) => _tasks.RemoveAll(x => x.WorkTaskId == task) != 0;

        public bool IsTaskInBacklog(Identity task) => _tasks.Any(x => x.WorkTaskId == task);

        public IEnumerator<WorkTaskWithStatus> GetEnumerator() => _tasks.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}