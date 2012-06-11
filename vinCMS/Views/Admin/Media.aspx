<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<vinCMS.Models.AdminMediaViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Admin - Manage Media Files
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add a New Media File</h2>
    <div>
        <% using (Html.BeginForm("media", "admin", FormMethod.Post, new { enctype = "multipart/form-data" }))
           {%>
                <%:Html.AntiForgeryToken("Media") %>
                <div><span class="form-label">New Media File: </span> <input type="file" name="file" class="form-submit mild"/></div>
                <input class="form-submit positive" type="submit" name="submitButton" value="Upload" />
                <p class="alt">
                    Note that, at present, only *.png, *.jpg and *.pdf files are allowed
                </p>
        <% }%>   
    </div>

    <h2>Manage Existing Media Files</h2>
    <div>
        <% Html.RenderPartial("PagedListMedia", Model.PagedListMedia); %>
    </div>
    <div>
    
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
