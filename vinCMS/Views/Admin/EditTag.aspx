<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.Tag>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Admin - Edit Tag
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit Tag</h2>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm())
      {%>
      <%: Html.AntiForgeryToken("EditTag") %>
      <%: Html.EditorForModel() %>
      <span>
            <a href="<%:Url.Action("canceltagedition")%>"><button name="cancel" type="button" class="form-submit negative">Cancel</button></a>
            <input class="form-submit positive" type="submit" name="submitButton" value="Save" />
      </span>                
      <p class="alt">
            Use lower case alphanumeric characters and spaces ([a-z] and [0-9])
      </p>
    <%
      }%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
            <script src="/public/javascript/MicrosoftAjax.js" type="text/javascript" ></script>
            <script src="/public/javascript/MicrosoftMvcValidation.js" type="text/javascript" ></script>
</asp:Content>

