using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vinCMS.Infraestructure.Logging
{
    /// <summary>
    /// Interface that represents the behavior of a logger object
    /// </summary>
    interface ILogger
    {
        void Trace(string message);
        void TraceException(string message, Exception exception);
        void Debug(string message);
        void DebugException(string message, Exception exception);
        void Info(string message);
        void InfoException(string message, Exception exception);
        void Warn(string message);
        void WarnException(string message, Exception exception);
        void Error(string message);
        void ErrorException(string message, Exception exception);
        void Fatal(string message); 
        void FatalException(string message, Exception exception);
    }
}
