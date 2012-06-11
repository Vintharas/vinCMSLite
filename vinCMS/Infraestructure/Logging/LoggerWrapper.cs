using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace vinCMS.Infraestructure.Logging
{
    /// <summary>
    /// Class that represents a wrapper for a Nlog.Logger object
    /// </summary>
    public class LoggerWrapper : ILogger
    {
        private readonly Logger _logger;
        private Logger WrappedLogger { get { return _logger; } }

        /// <summary>
        /// Class constructor. Initializes Logger object
        /// </summary>
        public LoggerWrapper(Logger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method that wraps the Trace logger method
        /// </summary>
        /// <param name="message"></param>
        public void Trace(string message)
        {
            WrappedLogger.Trace(message);
        }

        /// <summary>
        /// Method that wraps the TraceException logger method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void TraceException(string message, Exception exception)
        {
            WrappedLogger.TraceException(message, exception);
        }

        /// <summary>
        /// Method that wraps the Debug logger method
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            WrappedLogger.Debug(message);
        }

        /// <summary>
        /// Method that wraps the Debug Exception logger method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void DebugException(string message, Exception exception)
        {
            WrappedLogger.DebugException(message, exception);
        }

        /// <summary>
        /// Method that wraps the Info logger method
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            WrappedLogger.Debug(message);
        }

        /// <summary>
        /// Method that wraps the infoException logger method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void InfoException(string message, Exception exception)
        {
            WrappedLogger.InfoException(message, exception);
        }

        /// <summary>
        /// Method that wraps the warn logger method
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            WrappedLogger.Warn(message);
        }

        /// <summary>
        /// Method that wraps the warnException logger method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void WarnException(string message, Exception exception)
        {
           WrappedLogger.WarnException(message, exception);
        }

        /// <summary>
        /// Method that wraps the error logger method
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            WrappedLogger.Error(message);
        }

        /// <summary>
        /// Method that wraps the ErrorException logger method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void ErrorException(string message, Exception exception)
        {
            WrappedLogger.ErrorException(message, exception);
        }

        /// <summary>
        /// Method that wraps the fatal logger method
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string message)
        {
            WrappedLogger.Fatal(message);
        }

        /// <summary>
        /// Method that wraps the FatalException logger method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void FatalException(string message, Exception exception)
        {
            WrappedLogger.FatalException(message, exception);
        }
    }
}