using System;
using System.Runtime.Serialization;

namespace Scrummy.Domain.Core.Entities.Exceptions
{
    public class EntityValidationException : EntityException
    {
        public EntityValidationException()
        {
        }

        public EntityValidationException(string message) : base(message)
        {
        }

        public EntityValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string ErrorKey { get; set; }

        public string ErrorMessage { get; set; }
    }
}
