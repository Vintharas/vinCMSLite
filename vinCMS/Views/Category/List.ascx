<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Domain.Entities.Category>>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>

<div id="category-block">
    <h2 class="caps">Categories</h2>
    <div id="category-list">
        <ul title="Categories" class="quiet">
            <% foreach (var category in Model)
               { %>
               <li><%: Html.RouteLink(category.Name, category.GetCategoryDetails(), new Dictionary<string, object>{{"class", "quiet"}}) %></li>
            <%
               } %>
        </ul>
    </div>
</div>