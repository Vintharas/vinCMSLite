using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vinCMS.Infraestructure.Logging
{
    /// <summary>
    /// Interface that represents a log manager behavior
    /// </summary>
    public interface ILogManager
    {
        LoggerWrapper GetCurrentClassLogger();
    }
}
