<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<vinCMS.Models.AdminTagViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Admin - Manage Tags
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add a New Tag</h2>
    <div>
        <% Html.EnableClientValidation(); %>
        <% using (Html.BeginForm())
           {%>
                <%:Html.AntiForgeryToken("Tags") %>
                <%Html.RenderPartial("Tag", Model.NewTag);%>
                <input class="form-submit positive" type="submit" name="submitButton" value="Add" />
                <p class="alt">
                    Use lower case alphanumeric characters and spaces ([a-z] and [0-9])
                </p>
        <% }%>   
    </div>

    <h2>Manage Existing Tags</h2>
    <div>
        <% Html.RenderPartial("PagedListTags", Model.PagedListTags); %>
    </div>
    <div>
    
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
            <script src="/public/javascript/MicrosoftAjax.js" type="text/javascript" ></script>
            <script src="/public/javascript/MicrosoftMvcValidation.js" type="text/javascript" ></script>
</asp:Content>