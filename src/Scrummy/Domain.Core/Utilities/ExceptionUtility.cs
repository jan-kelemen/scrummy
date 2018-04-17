using Scrummy.Domain.Core.Entities.Common;
using Scrummy.Domain.Core.Exceptions;

namespace Scrummy.Domain.Core.Utilities
{
    public class ExceptionUtility
    {
        public static EntityValidationException CreateEntityValidationException<T>(Identity identity, string errorKey, string message, params object[] values)
        {
            return new EntityValidationException
            {
                EntityName = nameof(T),
                Identity = identity,
                ErrorKey = errorKey,
                ErrorMessage = string.Format(message, values),
            };
        }

        public static EntityReferenceNullException CreateEntityReferenceNullException<T>(Identity identity, string errorKey)
        {
            return new EntityReferenceNullException
            {
                EntityName = nameof(T),
                Identity = identity,
                ErrorKey = errorKey,
            };
        }
    }
}