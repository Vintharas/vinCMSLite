<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList.IPagedList<Domain.Entities.BlogPost>>" %>

<div>
    <table>
    <caption>Blog Posts</caption>
    <thead>
        <tr>
            <th>Publishing Date</th>
            <th>Post Title</th>
            <th>Options</th>
        </tr>
    </thead>
    <%foreach(var post in Model) {%>
        <% Html.RenderPartial("BlogPostSummary", post); %>
    <%} %>
    </table>
</div>
<div>
    <% Html.RenderPartial("Pager", Model); %>
</div>
