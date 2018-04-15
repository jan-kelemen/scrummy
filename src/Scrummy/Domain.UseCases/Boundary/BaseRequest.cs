using System.Collections.Generic;
using System.Linq;

namespace Scrummy.Domain.UseCases.Boundary
{
    public abstract class BaseRequest
    {
        public IDictionary<string, string> Errors { get; private set; }

        public virtual bool Validate()
        {
            Errors = new Dictionary<string, string>();
            ValidateCore();
            return this;
        }

        protected abstract void ValidateCore();

        protected void AddError(string key, string message, params object[] values)
        {
            Errors.Add(key, string.Format(message, values));
        }

        public static implicit operator bool(BaseRequest request) => !request.Errors.Any();
    }
}
