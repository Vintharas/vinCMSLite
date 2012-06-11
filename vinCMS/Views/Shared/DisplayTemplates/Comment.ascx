<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.Comment>" %>
<%@ Import Namespace="vinCMS.Helpers.Html" %>

<div class="comment-block">
    <div class="comment-author-info">
    &#123; <a href="<%: Model.UserWebsite %>"><%: Model.UserName %></a> &#125; <span class="comment-date"><%: Model.CommentDate.ToString("hh:mm MMMM dd, yyyy")%></span>
    <% if (Page.User.Identity.IsAuthenticated)
       {%>
       <%: Html.RouteLink("Delete Comment", new { controller = "admin", action = "deletecomment", id = Model.CommentID })%>
    <%
       }%>
    </div>
    <div class="comment-content">
        <%=  Html.GetSafeHtml(Model.CommentContent) %> 
    </div>        
</div>
