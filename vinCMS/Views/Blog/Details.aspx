<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<vinCMS.Models.BlogPostDetailModel>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - <%: Model.BlogPost.Title %>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">

    <meta name="description" content="<%: Model.BlogPost.MetaDescription %>" />
    <meta name="keywords" content="<%: Model.BlogPost.GetStringOfTags() %>" />
    <meta name="author" content="<%: Model.BlogPost.GetAuthorName() %>" />

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
        <div id="detail-content" class="container">
            <div class="span-18 last">
                <% Html.RenderPartial("TempDataMessages"); %>
                <!-- here go helpful messages for the user -->
            </div>
            <div class="span-18 last">
                 <%: Html.DisplayFor(x => x.BlogPost) %>
            </div>
            <div class="span-18 last share">
                 <% Html.RenderPartial("LikeButton", Model.BlogPost); %>
            </div>
            <% Html.RenderPartial("RelatedBlogPosts", Model); %>
            <div id="comment-list" class="span-18 last">
                <h2 class="section-header caps">BLOG COMMENTS</h2>
                <% if (Model.BlogPost.Comments.Count == 0)
                    {%>
                          <p>Nobody has commented yet!</p>
                <% }%>
                <%: Html.DisplayFor(x => x.BlogPost.Comments) %>  
            </div>
            <div id="comment-form" class="span-18 last">
                <% Html.RenderPartial("Comment", Model.NewComment); %>
            </div>
        </div>            
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
            <script src="/public/javascript/MicrosoftAjax.js" type="text/javascript" ></script>
            <script src="/public/javascript/MicrosoftMvcValidation.js" type="text/javascript" ></script>
            <script src="http://platform.twitter.com/widgets.js" type="text/javascript"></script>
</asp:Content>
