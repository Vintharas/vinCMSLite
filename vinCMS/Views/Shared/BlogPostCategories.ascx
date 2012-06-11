<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Domain.Entities.Category>>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>

<% foreach(var category in Model) {%>
<%: Html.RouteLink(category.Name, category.GetCategoryDetails()) %> 
<% } %>