using System;
using System.Runtime.Serialization;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Core.Entities.Exceptions
{
    public class EntityException : Exception
    {
        public EntityException()
        {
        }

        public EntityException(string message) : base(message)
        {
        }

        public EntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Identity EntityIdentity { get; set; }

        public string EntityName { get; set; }

        public string ErrorKey { get; set; }

        public string ErrorMessage { get; set; }
    }
}
