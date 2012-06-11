<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.Comment>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>

<% Html.EnableClientValidation(); %>
<h2 class="section-header caps">Add A New Comment</h2>
<% using ( Html.BeginForm()) {%>
    <%: Html.EditorForModel() %>
    <input type="submit" name="submitButton" value="Add Comment!" class="form-submit positive" />
<%} %>
