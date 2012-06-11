<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.Page>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Admin - Edit Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Page</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
      {%>
      <%: Html.AntiForgeryToken("EditPage") %>
      <%: Html.EditorForModel() %>
      <span>
            <a href="<%:Url.Action("cancelpageedition")%>"><button name="cancel" type="button" class="form-submit negative">Cancel</button></a>
            <input class="form-submit positive" type="submit" name="submitButton" value="Save" />
      </span>                
    <%
      }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
            <script src="/public/javascript/MicrosoftAjax.js" type="text/javascript" ></script>
            <script src="/public/javascript/MicrosoftMvcValidation.js" type="text/javascript" ></script>
</asp:Content>

