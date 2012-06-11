using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Security.Application;

namespace vinCMS.Helpers.Html
{
    public static class HtmlHelpersExtensions
    {
        public static string GetSafeHtml(this HtmlHelper helper, string input)
        {
            return Microsoft.Security.Application.Sanitizer.GetSafeHtmlFragment(input);
        }
    }
}