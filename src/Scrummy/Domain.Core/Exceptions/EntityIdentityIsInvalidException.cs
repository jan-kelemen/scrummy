using System;
using System.Runtime.Serialization;

namespace Scrummy.Domain.Core.Exceptions
{
    public class EntityIdentityIsInvalidException : EntityException
    {
        public EntityIdentityIsInvalidException()
        {
        }

        public EntityIdentityIsInvalidException(string message) : base(message)
        {
        }

        public EntityIdentityIsInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityIdentityIsInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string ErrorKey { get; set; }

        public string ErrorMessage { get; } = "Identity is blank.";
    }
}
