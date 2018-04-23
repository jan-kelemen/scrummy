using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Scrummy.Domain.UseCases.Exceptions.Boundary
{
    public class InvalidRequestException : UseCaseException
    {
        public InvalidRequestException()
        {
        }

        public InvalidRequestException(string message) : base(message)
        {
        }

        public InvalidRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IDictionary<string, string> Errors { get; set; }
    }
}
