<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.Page>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - <%: Model.Title %>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="description" content="<%: Model.MetaDescription %>" />
    <meta name="keywords" content="<%: Model.GetStringOfTags() %>" />
    <meta name="author" content="<%: Model.GetAuthorName() %>" />
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
        <div id="content" class="container">
            <div class="span-18 last">
                <% Html.RenderPartial("TempDataMessages"); %>
            </div>
            <div id="generic-page-content" class="span-18 last">
                 <%: Html.DisplayForModel() %>
            </div>
        </div>            
</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
