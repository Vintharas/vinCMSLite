using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vinCMS.Models;

namespace vinCMS.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Action method that manages 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public ActionResult HandleError(int? statusCode)
        {
            if (statusCode.HasValue)
                return HandleError_StatusCode(statusCode.Value);
            else if (TempData["exception"] != null)
                return HandleError_Exception((Exception)TempData["exception"]);
            else
                return HandleError_StatusCode(500);
        }

        /// <summary>
        /// Method that renders an appropiate view when the error was caused by an exception
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private ActionResult HandleError_Exception(Exception exception)
        {
            if (exception is vinCMS.Infraestructure.ErrorHandling.RouteNotFoundException)
            {
                return View(new ErrorViewModel
                                {
                                    Ex = exception,
                                    StatusError = 404
                                });
            }
            else
            {
                return View(new ErrorViewModel
                                {
                                    Ex = exception,
                                    StatusError = 500
                                });
            }
        }

        /// <summary>
        /// Method that renders an appropiate view when the error is a 403, 404 or 500 error
        /// issued by the system
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private ActionResult HandleError_StatusCode(int statusCode)
        {
            var viewModel = new ErrorViewModel
                                {
                                    StatusError = statusCode
                                };
            return View(viewModel);
        }

    }
}
