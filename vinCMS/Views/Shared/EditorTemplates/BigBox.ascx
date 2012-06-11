<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%: Html.TextArea("", new { @class = "form-control form-long" })%>