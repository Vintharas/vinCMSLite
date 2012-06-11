<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.Tag>" %>


<%: Html.EditorFor(x => x.TagID) %>
<span class="form-label"><%: Html.LabelFor(x => x.Name) %></span><%: Html.TextBoxFor(x => x.Name, new { @class ="form-control form-short"}) %>
<%: Html.ValidationMessageFor(x => x.Name) %>