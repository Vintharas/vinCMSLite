<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<vinCMS.Models.AdminViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Admin Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <div id="index-content">
                <h1>Admin Index</h1>
                <div>
                    <h2 class="caps">Blog Posts</h2>
                    <% Html.RenderPartial("PagedBlogPostSummary", Model.PagedListBlogPosts); %>
                </div>
                <div>
                    <h2 class="caps">Post Drafts</h2>
                    <% Html.RenderPartial("ListBlogPostSummary", Model.PostDraftsList); %>
                </div>
                <div>
                    <h2 class="caps">Pages</h2>
                    <% Html.RenderPartial("ListPagesSummary", Model.PagesList); %>
                </div>
            </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
