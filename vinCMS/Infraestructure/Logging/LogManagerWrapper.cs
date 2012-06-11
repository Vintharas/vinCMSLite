
using NLog;

namespace vinCMS.Infraestructure.Logging
{
    /// <summary>
    /// Wrapper for a Log Manager object
    /// </summary>
    public class LogManagerWrapper : ILogManager
    {

        public LoggerWrapper GetCurrentClassLogger()
        {
            return new LoggerWrapper(LogManager.GetCurrentClassLogger());
        }
        
    }
}

