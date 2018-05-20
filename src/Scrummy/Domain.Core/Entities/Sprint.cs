using System;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Entities.Enumerations;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities
{
    public class Sprint : Entity<Sprint>
    {
        public class Validation
        {
            public const string NameErrorKey = nameof(Name);
            public const int NameMinLength = 1;
            public const int NameMaxLength = 200;
            public const string NameIsInvalidMessage = "Sprint name is invalid.";

            public const string ProjectErrorKey = nameof(Project);
            public const string ProjectIsInvalid = "Project for which the sprint is organized has to be specified.";

            public const string TimeSpanErrorKey = nameof(TimeSpan);
            public const string TimeSpanIsInvalidMessage = "End date of the sprint must be after the start date";

            public static bool ValidateName(string name) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(name, NameMinLength, NameMaxLength);

            public static bool ValidateTimeSpan(Tuple<DateTime, DateTime> timeSpan) => timeSpan.Item1 < timeSpan.Item2;
        }

        private Identity _projectId;

        private string _name;

        public Sprint(
            Identity id,
            Identity projectId,
            string name, 
            Tuple<DateTime, DateTime> timeSpan, 
            string goal, 
            SprintStatus status) : base(id)
        {
            ProjectId = projectId;
            Name = name;
            TimeSpan = timeSpan;
            Goal = goal;
            Status = status;
        }

        public Identity ProjectId
        {
            get => _projectId;
            set => _projectId = CheckIdentityIsNotBlank(value, Validation.ProjectErrorKey);
        }

        public string Name
        {
            get => _name;
            set => _name = CheckName(value);
        }

        public Tuple<DateTime, DateTime> TimeSpan { get; set; }

        public string Goal { get; set; }

        public SprintStatus Status { get; set; }

        private string CheckName(string name)
        {
            if (!Validation.ValidateName(name))
                throw CreateEntityValidationException(Validation.NameErrorKey, Validation.NameIsInvalidMessage);
            return name;
        }
    }
}
