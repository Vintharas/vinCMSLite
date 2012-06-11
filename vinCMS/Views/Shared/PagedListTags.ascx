<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList.IPagedList<Domain.Entities.Tag>>" %>

<div>
    <table>
        <thead>
            <tr>
                <td>Tag Name</td>
                <td>Options</td>
            </tr>
        </thead>
        <% foreach (var tag in Model) {%>
        <tr>
            <td>
                <%: tag.Name%>
            </td>
            <td>
                <span><%: Html.ActionLink("Edit", "edittag", new { id = tag.TagID}) %></span>
                <span><%: Html.ActionLink("Delete", "deletetag", new { id = tag.TagID}) %></span>
            </td>
          </tr>
        <%} %>
    </table>
</div>
<div>
    <% Html.RenderPartial("PagerTags", Model); %>
</div>