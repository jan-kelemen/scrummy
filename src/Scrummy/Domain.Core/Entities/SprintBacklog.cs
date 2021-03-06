﻿using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Core.Entities
{
    public class SprintBacklog
    {
        public class Validation
        {
            public const string WorkTaskErrorKey = nameof(WorkTask);
            public const string WorkTaskIsInvalid = "Work task in the sprint backlog has to be specified.";

            public const string BacklogErrorKey = nameof(SprintBacklog);
            public const string SprintBacklogContainsDuplicateItems = "Sprint backlog contains duplicate items.";
        }

        public enum WorkTaskStatus
        {
            ToDo, InProgress, Done
        }

        public class WorkTaskWithStatus
        {
            public WorkTaskWithStatus(Identity workTaskId, Identity parentTaskId, WorkTaskStatus status)
            {
                WorkTaskId = workTaskId;
                ParentTaskId = parentTaskId;
                Status = status;
            }

            public Identity WorkTaskId { get; }

            public Identity ParentTaskId { get; }

            public WorkTaskStatus Status { get; set; }
        }

        private List<Identity> _stories;

        private List<Identity> _completedStories;

        private List<WorkTaskWithStatus> _tasks;

        public SprintBacklog(
            Identity sprintId, 
            IEnumerable<Identity> stories,
            IEnumerable<Identity> completedStories,
            IEnumerable<WorkTaskWithStatus> tasks)
        {
            SprintId = sprintId;
            Stories = new List<Identity>(stories);
            CompletedStories = new List<Identity>(completedStories);
            Tasks = tasks;
        }

        public Identity SprintId { get; }

        public IList<Identity> Stories
        {
            get => _stories;
            set => _stories = new List<Identity>(value);
        }

        public IList<Identity> CompletedStories
        {
            get => _completedStories;
            set => _completedStories = new List<Identity>(value);
        }

        public IEnumerable<WorkTaskWithStatus> Tasks
        {
            get => _tasks;
            set => _tasks = new List<WorkTaskWithStatus>(value);
        }
    }
}
