<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList.IPagedList<Domain.Entities.Media>>" %>

<div>
    <table>
        <caption>Media Files</caption>
        <thead>
            <tr>
                <th>File Name</th>
                <th>Upload Date</th>
                <th>Full Path</th>
                <th>Options</th>
            </tr>
        </thead>
        <% foreach (var media in Model) {%>
        <tr>
            <td>
                <a href="<%:Server.MapPath(media.Path)%>" title="<%:media.FileName%>"><%:media.FileName%></a>
            </td>
            <td>
                <%: media.UploadDate.HasValue ? media.UploadDate.Value.ToString("dd/mm/yy") : string.Empty %>
            </td>
            <td>
                <%: media.Path %>
            </td>
            <td>
                <span><%: Html.ActionLink("Delete", "deletemedia", new { id = media.MediaID}) %></span>
            </td>
          </tr>
        <%} %>
    </table>
</div>
<div>
    <% Html.RenderPartial("PagerMedia", Model); %>
</div>