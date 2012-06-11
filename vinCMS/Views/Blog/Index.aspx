<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<vinCMS.Models.BlogPostViewModel>" %>

<%@ Import Namespace="vinCMS.Infraestructure" %>
<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - <%: Model.Title %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

        <div class="container">
            <div id="content" class="span-12 colborder">
                    <span class="go-to-link"><a href="#footer">bottom</a></span>
                    <% Html.RenderPartial("TempDataMessages"); %>
                    <% Html.RenderPartial("BlogPosts", Model.BlogPostPagedList); %>
                    <% Html.RenderPartial("Pager", Model.BlogPostPagedList); %>
                    <span class="go-to-link"><a href="#header">top</a></span>
            </div>
            <div id="sidebar" class="span-5 last">
                <% Html.RenderPartial("AuthorBio");%>
                <% Html.RenderAction("List", "Category"); %>
                <% Html.RenderAction("List", "Tag"); %>
            </div>
        </div>

</asp:Content>
