<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.Page>" %>
<%@ Import Namespace="vinCMS.Helpers.Html" %>

                <%if (!Model.IsHomePage) {%>
                    <span class="go-to-link"><a href="#footer">bottom</a></span>
                <%} %>
                    <%= Model.BodyContent %>
                <%if (!Model.IsHomePage) {%>
                    <span class="go-to-link"><a href="#header">top</a></span>
                <%} %>