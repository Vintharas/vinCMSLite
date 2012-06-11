<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.Page>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Create a New Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
            <h2>Create a New Page</h2>
            
            <% Html.EnableClientValidation(); %>
            <% using(Html.BeginForm())
               {%>
               <%: Html.AntiForgeryToken("CreatePage") %>
               <%: Html.EditorForModel() %>
               <p>
                    <a href="<%:Url.Action("cancelpagecreation")%>"><button name="cancel" type="button" class="form-submit negative">Cancel</button></a>
                    <input class="form-submit positive" type="submit" name="submitButton" value="Publish" />
                </p>
            <% }%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
            <script src="/public/javascript/MicrosoftAjax.js" type="text/javascript" ></script>
            <script src="/public/javascript/MicrosoftMvcValidation.js" type="text/javascript" ></script>
</asp:Content>
