using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace vinCMS.Models
{
    public class NavBarViewModel
    {
        public string NavLinkTitle { get; set; }
        public RouteValueDictionary NavLinkRoute { get; set; }
        public bool IsSelected { get; set; }
    }
}