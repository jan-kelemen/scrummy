using System;
using System.Runtime.Serialization;

namespace Scrummy.Domain.Core.Exceptions
{
    public class EntityReferenceNullException : EntityException
    {
        public EntityReferenceNullException()
        {
        }

        public EntityReferenceNullException(string message) : base(message)
        {
        }

        public EntityReferenceNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityReferenceNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string ErrorKey { get; set; }

        public string ErrorMessage { get; } = "Reference can't be null.";
    }
}
