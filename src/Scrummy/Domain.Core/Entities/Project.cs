using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators;

namespace Scrummy.Domain.Core.Entities
{
    public class Project : Entity<Project>
    {
        public static class Validation
        {
            public const string NameErrorKey = nameof(Name);
            public const int NameMinLength = 1;
            public const int NameMaxLength = 200;
            public const string NameIsInvalidMessage = "Project name is invalid.";

            public const string DefinitionOfDoneErrorKey = nameof(DefinitionOfDone);
            public const int DefinitionOfDoneItemMinLength = 1;
            public const int DefinitionOfDoneItemMaxLength = 400;
            public const string DefinitionOfDoneConditionIsInvalid = "Condition in Definition of done is invalid.";
            public const string DefinitionOfDoneIsInvalid = "Definition of done has to have at least one condition.";

            public const string TeamErrorKey = nameof(Team);

            public static bool ValidateName(string name) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(name, NameMinLength, NameMaxLength);

            public static bool ValidateDefinitionOfDoneConditions(IEnumerable<string> conditions)
            {
                var temp = conditions.ToArray();
                return temp.Length != 0 && temp.All(ValidateDefinitionOfDoneCondition);
            }

            public static bool ValidateDefinitionOfDoneCondition(string condition) =>
                TextValidator.ValidateThatContentIsBetweenSpecifiedLength(condition, DefinitionOfDoneItemMinLength, DefinitionOfDoneItemMaxLength);
        }

        private string _name;

        private DefinitionOfDone _definitionOfDone;

        private Team _team;

        public Project(
            Identity id, 
            string name, 
            DefinitionOfDone definitionOfDone, 
            Team team) : base(id)
        {
            Name = name;
            DefinitionOfDone = definitionOfDone;
            Team = team;
        }

        public string Name
        {
            get => _name;
            set => _name = CheckName(value);
        }

        public DefinitionOfDone DefinitionOfDone
        {
            get => _definitionOfDone;
            set
            {
                _definitionOfDone = CheckReferenceNotNull(value, Validation.DefinitionOfDoneErrorKey);
                _definitionOfDone.ProjectId = Id;
            }
        }

        public Team Team
        {
            get => _team;
            set => _team = CheckReferenceNotNull(value, Validation.TeamErrorKey);
        }

        private string CheckName(string name)
        {
            if (!Validation.ValidateName(name))
                throw CreateEntityValidationException(Validation.NameErrorKey, Validation.NameIsInvalidMessage);
            return name;
        }
    }
}
