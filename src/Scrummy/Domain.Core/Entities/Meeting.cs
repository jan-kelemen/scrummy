using System;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities
{
    public class Meeting : Entity<Meeting>
    {
        public class Validation
        {
            public const string NameErrorKey = nameof(Name);
            public const int NameMinLength = 1;
            public const int NameMaxLength = 200;
            public const string NameIsInvalidMessage = "Meeting name is invalid.";

            public const string ProjectErrorKey = nameof(Project);
            public const string ProjectIsInvalid = "Project for which the meeting is held has to be specified.";

            public const string OrganizedByErrorKey = nameof(OrganizedBy);
            public const string OrganizedByIsInvalidMessage = "Person who organizes the meeting has to be specified.";

            public const string InvolvedPersonsErrorKey = nameof(InvolvedPersons);
            public const string InvolvedPersonsAreInvalidMessage = "Persons involved in the meeting are invalid.";

            public static bool ValidateName(string name) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(name, NameMinLength, NameMaxLength);

            public static bool ValidateOrganizedBy(Identity id) => !id.IsBlankIdentity();
        }

        private Identity _projectId;

        private string _name;

        private Identity _organizedBy;

        private List<Identity> _involvedPersons;

        private List<Identity> _documents;

        public Meeting(
            Identity id,
            Identity projectId,
            string name,
            DateTime time,
            TimeSpan duration,
            Identity organizedBy,
            string description,
            string log,
            IEnumerable<Identity> involvedPersons,
            IEnumerable<Identity> documents) : base(id)
        {
            ProjectId = projectId;
            Name = name;
            Time = time;
            OrganizedBy = organizedBy;
            Description = description;
            InvolvedPersons = involvedPersons;
            Duration = duration;
            Log = log;
            Documents = documents;
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

        public DateTime Time { get; set; }

        public TimeSpan Duration { get; set; }

        public Identity OrganizedBy
        {
            get => _organizedBy;
            set => _organizedBy = CheckIdentityIsNotBlank(value, Validation.OrganizedByErrorKey);
        }

        public string Description { get; set; }

        public string Log { get; set; }

        public IEnumerable<Identity> InvolvedPersons
        {
            get => _involvedPersons;
            set => _involvedPersons = CheckInvolvedPersons(value);
        }

        public IEnumerable<Identity> Documents
        {
            get => _documents;
            set => _documents = new List<Identity>(value);
        }

        private List<Identity> CheckInvolvedPersons(IEnumerable<Identity> value)
        {
            var temp = value.ToList();
            if(!SetValidator.ValidateItemsAreUnique(temp))
                throw CreateEntityValidationException(Validation.InvolvedPersonsErrorKey, Validation.InvolvedPersonsAreInvalidMessage);
            return temp;
        }

        private string CheckName(string name)
        {
            if (!Validation.ValidateName(name))
                throw CreateEntityValidationException(Validation.NameErrorKey, Validation.NameIsInvalidMessage);
            return name;
        }
    }
}
