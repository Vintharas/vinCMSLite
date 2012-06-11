<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<vinCMS.Models.AdminCategoryViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Admin - Manage Categories
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add a New Category</h2>
    <div>
        <% Html.EnableClientValidation(); %>
        <% using (Html.BeginForm())
           {%>
                <%:Html.AntiForgeryToken("Categories") %>
                <%Html.RenderPartial("Category", Model.NewCategory);%>
                <input class="form-submit positive" type="submit" name="submitButton" value="Add" />
                <p class="alt">
                    Use lower case alphanumeric characters and spaces ([a-z] and [0-9])
                </p>
        <% }%>   
    </div>

    <h2>Manage Existing Categories</h2>
    <div>
        <% Html.RenderPartial("PagedListCategories", Model.PagedListCategories); %>
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
