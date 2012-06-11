<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Domain.Entities.BlogPost>>" %>

<table>
<caption>Blog Draft Posts</caption>
<thead>
    <tr>
        <th>Creation Date</th>
        <th>Post Title</th>
        <th>Options</th>
    </tr>
</thead>
<%foreach (var post in Model) {%>
    <% Html.RenderPartial("BlogPostSummary", post); %>
<%} %>
</table>
