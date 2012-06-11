using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace vinCMS.Infraestructure.ErrorHandling
{
    public class RouteNotFoundException : ApplicationException
    {
        private const string MESSAGE = "The selected route was not found.";
        public RouteNotFoundException() : base(MESSAGE) { }
        public RouteNotFoundException(string message) : base(message) { }
        public RouteNotFoundException(string message, Exception inner) : base(message, inner) { }
        public RouteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}