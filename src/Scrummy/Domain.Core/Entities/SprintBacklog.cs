using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Core.Entities
{
    public class SprintBacklog
    {
        public class Validation
        {
            public const string WorkTaskErrorKey = nameof(WorkTask);
            public const string WorkTaskIsInvalid = "Work task in the sprint backlog has to be specified.";
        }

        public enum WorkTaskStatus
        {
            ToDo, InProgress, Done
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

        private List<Identity> _plannedTasks;

        private List<WorkTaskWithStatus> _tasks;

        public SprintBacklog(
            Identity sprintId, 
            IEnumerable<Identity> plannedTasks, 
            IEnumerable<WorkTaskWithStatus> tasks)
        {
            SprintId = sprintId;
            PlannedTaskIds = plannedTasks;
            Tasks = tasks;
        }

        public Identity SprintId { get; }

        public IEnumerable<Identity> PlannedTaskIds
        {
            get => _plannedTasks;
            private set => _plannedTasks = new List<Identity>(value);
        }

        public IEnumerable<WorkTaskWithStatus> Tasks
        {
            get => _tasks;
            set => _tasks = new List<WorkTaskWithStatus>(value);
        }
    }
}
