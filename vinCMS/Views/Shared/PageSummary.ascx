<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.Page>" %>

<%@ Import Namespace="vinCMS.Helpers.Routing" %>
<tr>
    <td class="post-summary-title"><%: Html.RouteLink(Model.Title, Model.GetPageDetails()) %></td>
    <td><%: Model.IsHomePage %></td>
    <td><%: Model.IsNavigablePage %></td>
    <td>
        <span class="post-summary-edit"><%: Html.ActionLink("Edit", "editpage", new { id = Model.ContentID}) %></span> / 
        <span class="post-summary-delete"><%: Html.ActionLink("Delete", "deletepage", new { id = Model.ContentID}) %></span>
    </td>
</tr>