<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList.IPagedList<Domain.Entities.Category>>" %>

<div>
    <table>
        <thead>
            <tr>
                <td>Category Name</td>
                <td>Options</td>
            </tr>
        </thead>
        <% foreach (var category in Model) {%>
        <tr>
            <td>
                <%:category.Name%>
            </td>
            <td>
                <span><%: Html.ActionLink("Edit", "editcategory", new { id = category.CategoryID}) %></span>
                <span><%: Html.ActionLink("Delete", "deletecategory", new { id = category.CategoryID}) %></span>
            </td>
          </tr>
        <%} %>
    </table>
</div>
<div>
    <% Html.RenderPartial("PagerCategories", Model); %>
</div>
