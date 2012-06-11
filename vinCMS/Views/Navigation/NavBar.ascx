<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<vinCMS.Models.NavBarViewModel>>" %>

        <div class="navigation">
            <%foreach(var navLink in Model)
              {%>
              <span><%: Html.RouteLink(navLink.NavLinkTitle, navLink.NavLinkRoute) %></span>
            <%
              }%>
        </div>