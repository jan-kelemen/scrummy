using System.Collections.Generic;
using System.Linq;

namespace Scrummy.Domain.Core.Validators
{
    public class SetValidator
    {
        public static bool ValidateItemsAreUnique<T>(IEnumerable<T> items)
        {
            var diffChecker = new HashSet<T>();
            return items.All(diffChecker.Add);
        }
    }
}
