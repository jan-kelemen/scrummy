using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities
{
    public class WorkTask : Entity<WorkTask>
    {
        public class Validation
        {
            public const string NameErrorKey = nameof(Name);
            public const int NameMinLength = 1;
            public const int NameMaxLength = 200;
            public const string NameIsInvalidMessage = "Task name is invalid.";

            public const string StoryPointsErrorKey = nameof(StoryPoints);
            public const string StoryPointsAreInvalidMessage = "Story points can't be negative.";

            public const string LinkErrorKey = "Link";
            public const string LinkTargetIsInvalidMessage = "This task can't be linked to {0}.";
            public const string LinkTargetIsAlreadyLinkedMessage = "This task is already linked to specified task.";
            public const string LinksAreDuplicatedMessage = "This task contains duplicated linked tasks.";

            public const string AuthorIdErrorKey = nameof(Comment.AuthorId);
            public const string AuthorIdIsInvalidMessage = "Author of the comment must be specified.";

            public const string WorkTaskIdErrorKey = nameof(Comment.WorkTaskId);
            public const string WorkTaskIdIsInvalidMessage = "Work task of the comment must be specified.";

            public const string CommentContentErrorKey = nameof(Comment.Content);
            public const int CommentContentMinLength = 1;
            public const string CommentContentIsInvalidMessage = "Comment content is invalid.";

            public static bool ValidateName(string name) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(name, NameMinLength, NameMaxLength);

            public static bool ValidateStoryPoints(int? storyPoints) => !storyPoints.HasValue || storyPoints.Value >= 0;

            public static bool ValidateTaskCanLink(WorkTaskType sourceType, WorkTaskType linkedTaskType)
            {
                switch (sourceType)
                {
                    case WorkTaskType.Epic:
                        return linkedTaskType == WorkTaskType.UserStory;
                    case WorkTaskType.UserStory:
                        return linkedTaskType == WorkTaskType.Task || linkedTaskType == WorkTaskType.Defect;
                    case WorkTaskType.Task:
                    case WorkTaskType.Defect:
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(@sourceType), @sourceType, null);
                }
            }

            public static bool ValidateCommentContent(string content) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(content, CommentContentMinLength);

            public static bool ValidateChildTasks(IEnumerable<Identity> childTasks)
            {
                var ct = childTasks.ToArray();
                var dct = ct.Distinct().ToArray();

                return dct.Length == ct.Length;
            }
        }

        public class Comment : Entity<Comment>
        {
            private Identity _authorId;
            private Identity _workTaskId;
            private string _content;


            public Comment(
                Identity id, 
                Identity authorId, 
                Identity workTaskId, 
                string content) : base(id)
            {
                AuthorId = authorId;
                WorkTaskId = workTaskId;
                Content = content;
            }

            public Identity AuthorId
            {
                get => _authorId;
                private set => _authorId = CheckIdentityIsNotBlank(value, null);
            }

            public Identity WorkTaskId
            {
                get => _workTaskId;
                private set => _workTaskId = CheckIdentityIsNotBlank(value, null);
            }

            public string Content
            {
                get => _content;
                set => _content = CheckContent(value);
            }

            private string CheckContent(string content)
            {
                if(!Validation.ValidateCommentContent(content)) 
                    throw CreateEntityValidationException(Validation.CommentContentErrorKey, Validation.CommentContentIsInvalidMessage);
                return content;
            }
        }

        private string _name;
        private int? _storyPoints;
        private List<Identity> _childTasks;
        private readonly List<Identity> _comments;
        private List<string> _steps;
        private List<Identity> _documents;

        public WorkTask(
            Identity id, 
            Identity projectId,
            WorkTaskType type, 
            string name, 
            int? storyPoints,
            string description,
            Identity parentTask,
            IEnumerable<Identity> childTasks,
            IEnumerable<Identity> comments,
            IEnumerable<string> steps,
            IEnumerable<Identity> documents) : base(id)
        {
            ProjectId = projectId;
            Type = type;
            Name = name;
            StoryPoints = storyPoints;
            Description = description;
            ParentTask = parentTask;
            ChildTasks = childTasks;
            _comments = new List<Identity>(comments);
            Steps = steps;
            Documents = documents;
        }

        public Identity ProjectId { get; }

        public WorkTaskType Type { get; }

        public string Name
        {
            get => _name;
            set => _name = CheckName(value);
        }

        public int? StoryPoints
        {
            get => _storyPoints;
            set => _storyPoints = CheckStoryPoints(value);
        }

        public string Description { get; set; }

        public Identity ParentTask { get; set; }

        public IEnumerable<Identity> ChildTasks
        {
            get => _childTasks;
            set => _childTasks = CheckChildTasks(value);
        }

        public IEnumerable<Identity> Comments => _comments;

        public IEnumerable<string> Steps
        {
            get => _steps;
            set => _steps = new List<string>(value);
        }

        public IEnumerable<Identity> Documents
        {
            get => _documents;
            set => _documents = new List<Identity>(value);
        }

        public void AddChildTask(WorkTask other)
        {
            if (!Validation.ValidateTaskCanLink(Type, CheckReferenceNotNull(other, Validation.LinkErrorKey).Type))
                throw CreateEntityValidationException(Validation.LinkErrorKey, Validation.LinkTargetIsInvalidMessage, other.Type);

            if(IsChild(other.Id))
                throw CreateEntityValidationException(Validation.LinkErrorKey, Validation.LinkTargetIsAlreadyLinkedMessage);

            _childTasks.Add(other.Id);
        }

        public bool IsChild(Identity childId) => _childTasks.Contains(childId);

        private string CheckName(string name)
        {
            if (!Validation.ValidateName(name))
                throw CreateEntityValidationException(Validation.NameErrorKey, Validation.NameIsInvalidMessage);
            return name;
        }

        private int? CheckStoryPoints(int? storyPoints)
        {
            if(!Validation.ValidateStoryPoints(storyPoints))
                throw CreateEntityValidationException(Validation.StoryPointsErrorKey, Validation.StoryPointsAreInvalidMessage);

            return storyPoints;
        }

        private List<Identity> CheckChildTasks(IEnumerable<Identity> childTasks)
        {
            var temp = childTasks.ToList();

            if (!Validation.ValidateChildTasks(temp))
                throw CreateEntityValidationException(Validation.LinkErrorKey, Validation.LinksAreDuplicatedMessage);

            return temp;
        }
    }
}
