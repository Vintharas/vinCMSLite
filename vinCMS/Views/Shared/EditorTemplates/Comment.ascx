<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.Comment>" %>

<div class="comment-author-details">
   <span class="form-label"><%: Html.LabelFor(x => x.UserName) %></span> <%:Html.TextBoxFor(x =>x.UserName, new{@class="form-control form-short"}) %>
   <span class="form-label"><%: Html.LabelFor(x => x.UserWebsite) %></span>
   <span><%:Html.TextBoxFor(x =>x.UserWebsite, new{@class="form-control form-user-website"}) %></span>
    <p><%: Html.ValidationMessageFor(x => x.UserName) %></p>
    <p><%: Html.ValidationMessageFor(x => x.UserWebsite) %></p>
</div>
<div>
   <div class="form-label"><%: Html.LabelFor(x => x.CommentContent) %></div>
   <%: Html.EditorFor(x => x.CommentContent) %>
   <p><%: Html.ValidationMessageFor(x => x.CommentContent) %></p>
</div>