using System;
using System.Runtime.Serialization;

namespace Scrummy.Domain.Repositories.Exceptions
{
    public class InvalidEntityException : RepositoryException
    {
        public InvalidEntityException()
        {
        }

        public InvalidEntityException(string message) : base(message)
        {
        }

        public InvalidEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidEntityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
