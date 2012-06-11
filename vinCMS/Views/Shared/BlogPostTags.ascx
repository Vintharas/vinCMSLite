<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Domain.Entities.Tag>>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>
<% foreach (var tag in Model)
   {%>
  <%: Html.RouteLink(tag.Name, tag.GetTagDetails()) %> 
<%  } %>