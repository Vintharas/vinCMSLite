using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Domain.ErrorHandling
{
    public class EntityNotFoundException : ApplicationException
    {
        private const string MESSAGE = "The object was not found within the database.";
        public EntityNotFoundException() :base(MESSAGE) {}
        public EntityNotFoundException(string message) : base(message) {}
        public EntityNotFoundException(string message, Exception inner) : base(message, inner){}
        public EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}