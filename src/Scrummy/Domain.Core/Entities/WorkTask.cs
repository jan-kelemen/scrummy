using System;
using System.Collections.Generic;
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

            public const string DescriptionErrorKey = nameof(Description);
            public const int DescriptionMinLength = 1;
            public const string DescriptionIsInvalidMessage = "Description is invalid.";

            public const string StoryPointsErrorKey = nameof(StoryPoints);
            public const string StoryPointsAreInvalidMessage = "Story points can't be negative.";

            public const string LinkErrorKey = "Link";
            public const string LinkTargetIsInvalidMessage = "This task can't be linked to {0}.";
            public const string LinkTargetIsAlreadyLinkedMessage = "This task is already linked to specified task.";

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

            public static bool ValidateDescription(string description) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(description, DescriptionMinLength);

            public static bool ValidateTaskCanLink(WorkTaskType sourceType, WorkTaskType linkedTaskType)
            {
                switch (sourceType)
                {
                    case WorkTaskType.Epic:
                        return true;
                    case WorkTaskType.UserStory:
                        return linkedTaskType == WorkTaskType.Task || linkedTaskType == WorkTaskType.Defect;
                    case WorkTaskType.Task:
                        return false;
                    case WorkTaskType.Defect:
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(@sourceType), @sourceType, null);
                }
            }

            public static bool ValidateCommentContent(string content) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(content, CommentContentMinLength);
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
        private string _description;
        private readonly List<Identity> _linkedFrom;
        private readonly List<Identity> _linkedTo;
        private readonly List<Identity> _comments;

        public WorkTask(
            Identity id, 
            WorkTaskType type, 
            string name, 
            int? storyPoints, 
            IEnumerable<Identity> linkedFrom, 
            IEnumerable<Identity> linkedTo, 
            IEnumerable<Identity> comments) : base(id)
        {
            Type = type;
            Name = name;
            StoryPoints = storyPoints;
            _linkedFrom = new List<Identity>(linkedFrom);
            _linkedTo = new List<Identity>(linkedTo);
            _comments = new List<Identity>(comments);
        }

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

        public string Description
        {
            get => _description;
            set => _description = CheckDescription(value);
        }

        public IEnumerable<Identity> LinkedFrom => _linkedFrom;

        public IEnumerable<Identity> LinkedTo => _linkedTo;

        public IEnumerable<Identity> Comments => _comments;

        public void Link(WorkTask other)
        {
            if (!Validation.ValidateTaskCanLink(Type, CheckReferenceNotNull(other, Validation.LinkErrorKey).Type))
                throw CreateEntityValidationException(Validation.LinkErrorKey, Validation.LinkTargetIsInvalidMessage, other.Type);

            if(_linkedTo.Contains(other.Id))
                throw CreateEntityValidationException(Validation.LinkErrorKey, Validation.LinkTargetIsAlreadyLinkedMessage);

            _linkedTo.Add(other.Id);
            other._linkedFrom.Add(Id);
        }

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

        private string CheckDescription(string description)
        {
            if (!Validation.ValidateDescription(description))
                throw CreateEntityValidationException(Validation.DescriptionErrorKey, Validation.DescriptionIsInvalidMessage);

            return description;
        }
    }
}
