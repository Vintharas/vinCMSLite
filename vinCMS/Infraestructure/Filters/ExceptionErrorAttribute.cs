using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using vinCMS.Infraestructure.Logging;

namespace vinCMS.Infraestructure.Filters
{
    /// <summary>
    /// Attribute (MVC filter) whose purpose is to handle exceptions
    /// </summary>
    public class ExceptionErrorAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IExceptionFilter
    {

        private const string ERROR_LOGGED_IN_FILTER = "There has been an error that has been captured by the Error Filter";

        /// <summary>
        /// Manages exceptions
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            // Don't interfere if the exception is already handled 
            if (filterContext.ExceptionHandled)
                return;

            // Let the next request know what went wrong
            if (filterContext.Exception is Domain.ErrorHandling.EntityNotFoundException)
                filterContext.Controller.TempData["exception"] =
                    new vinCMS.Infraestructure.ErrorHandling.RouteNotFoundException();
            else
            {
               filterContext.Controller.TempData["exception"] = filterContext.Exception; 
            }
            // Log exception
            LoggerWrapper logger =
                ((ILogManager) MvcApplication.Container.GetService(typeof (ILogManager))).GetCurrentClassLogger();
            logger.ErrorException(ERROR_LOGGED_IN_FILTER, filterContext.Exception);

            // Set up a redirection to my global error handler 
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "error", action = "handleerror" }
                ));

            // Advise subsequent exception filters not to interfere 
            // and stop ASP.NET from producing a "yellow screen of death"
            filterContext.ExceptionHandled = true;

            // Erase any output already generated 
            filterContext.HttpContext.Response.Clear(); 
        }
    }
}