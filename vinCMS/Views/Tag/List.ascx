<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Domain.Entities.Tag>>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>

<div id="tag-block">
    <h2 class="caps">Most Popular Tags</h2>
    <div id="tag-list">
        <ul title="Most Popular Tags" class="quiet">
            <% foreach (var tag in Model)
               { %>
               <li><%: Html.RouteLink(tag.Name, tag.GetTagDetails(), new Dictionary<string, object>{{"class", "quiet"}}) %></li>
            <%
               } %>
        </ul>
    </div>
</div>