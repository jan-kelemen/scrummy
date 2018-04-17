using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Utilities;

namespace Scrummy.Domain.Core.Entities
{
    public class DefinitionOfDone : IEnumerable<string>
    {
        private List<string> _conditions;

        public DefinitionOfDone(IEnumerable<string> conditions)
        {
            Conditions = conditions;
        }

        public Identity ProjectId { get; internal set; } = Identity.BlankIdentity;

        public IEnumerable<string> Conditions
        {
            get => _conditions;
            set => _conditions = CheckDefinitionOfDoneConditions(value);
        }

        private List<string> CheckDefinitionOfDoneConditions(IEnumerable<string> conditions)
        {
            var temp = conditions.ToList();
            if (!temp.Any())
                throw ExceptionUtility.CreateEntityValidationException<Project>(ProjectId, Project.Validation.DefinitionOfDoneErrorKey, Project.Validation.DefinitionOfDoneIsInvalid);

            if (temp.Any(condition => !Project.Validation.ValidateDefinitionOfDoneCondition(condition)))
                throw ExceptionUtility.CreateEntityValidationException<Project>(ProjectId, Project.Validation.DefinitionOfDoneErrorKey, Project.Validation.DefinitionOfDoneConditionIsInvalid);

            return temp;
        }

        public IEnumerator<string> GetEnumerator() => _conditions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
