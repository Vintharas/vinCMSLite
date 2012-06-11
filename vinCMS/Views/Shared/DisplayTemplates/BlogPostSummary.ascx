<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.BlogPost>" %>

<%@ Import Namespace="vinCMS.Helpers.Routing" %>
<tr>
    <td class="post-summary-date"><%: Model.PublishingDate == new DateTime() ? Model.CreationDate.ToString("dd MMMM yyyy, hh:mm") : Model.PublishingDate.ToString("dd MMMM yyyy, hh:mm") %></td>
    <td class="post-summary-title"><%: Html.RouteLink(Model.Title, Model.GetBlogDetails()) %></td>
    <td>
        <span class="post-summary-edit"><%: Html.ActionLink("Edit", "editblogpost", new { id = Model.ContentID}) %></span> /
        <span class="post-summary-delete"><%: Html.ActionLink("Delete", "deleteblogpost", new { id = Model.ContentID}) %></span>
        <% if (Model.IsDraft)
           {%>
                 / <span class="post-summary-publish"><%: Html.ActionLink("Publish", "publishblogpost", new{ id = Model.ContentID}) %></span>
           <%
           }%>
    </td>
</tr>
