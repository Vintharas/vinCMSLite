<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.Media>" %>

<div>
    <h2><%: Model.FileName %></h2>
    <p>Path: <%: Model.Path %></p>
    <p>Size: <%: Model.Size %></p>
    <p>Uploaded on: <%: Model.UploadDate.HasValue ? Model.UploadDate.Value.ToString("dd MMMM yyyy") : "not recorded" %></p>
</div>