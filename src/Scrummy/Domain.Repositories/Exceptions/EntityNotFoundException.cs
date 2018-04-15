using System;
using System.Runtime.Serialization;
using Scrummy.Domain.Core.Entities.Common;

namespace Scrummy.Domain.Repositories.Exceptions
{
    public class EntityNotFoundException : RepositoryException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public Identity Identity { get; set; }

        public string EntityName { get; set; }
    }
}
