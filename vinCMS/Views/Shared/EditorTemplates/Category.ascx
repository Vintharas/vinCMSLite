<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.Category>" %>

<%: Html.EditorFor(x => x.CategoryID) %>
<span class="form-label"><%: Html.LabelFor(x => x.Name) %></span><%: Html.TextBoxFor(x => x.Name, new { @class ="form-control form-short"}) %>
<%: Html.ValidationMessageFor(x => x.Name) %>