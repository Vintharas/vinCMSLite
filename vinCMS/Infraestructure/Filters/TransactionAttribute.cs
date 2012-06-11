using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainStorage.Abstracts;
using Ninject;

namespace vinCMS.Infraestructure.Filters
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            MvcApplication.Container.Get<IContext>().SubmitChanges();
        }
    }
}