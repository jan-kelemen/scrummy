using System.Collections;
using System.Collections.Generic;
using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Validators.Entities;

namespace Scrummy.Domain.Core.Entities
{
    public class DefinitionOfDone : IEnumerable<string>
    {
        private List<string> _conditions;

        public DefinitionOfDone(IEnumerable<string> conditions)
        {
            Conditions = conditions;
        }

        public Identity ProjectIdentity { get; internal set; } = Identity.BlankIdentity;

        public IEnumerable<string> Conditions
        {
            get => _conditions;
            set
            {
                var temp = new List<string>(value);
                ProjectValidator.CheckDefinitionOfDoneConditions(ProjectIdentity, temp);
                _conditions = temp;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _conditions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
