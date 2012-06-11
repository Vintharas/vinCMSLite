<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Domain.Entities.Page>>" %>

<table>
    <caption>Pages</caption>
    <thead>
        <tr>
            <th>Page Title</th>
            <th>Is Homepage</th>
            <th>Is In the Nav. Menu</th>
            <th>Options</th>
        </tr>
    </thead>
<%foreach (var page in Model) {%>
    <% Html.RenderPartial("PageSummary", page); %>
<%} %>
</table>
