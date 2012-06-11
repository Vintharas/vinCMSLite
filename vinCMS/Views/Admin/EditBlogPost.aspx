<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.BlogPost>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditBlogPost
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <h2>Edit Blog Post</h2>
            <% Html.EnableClientValidation(); %>
            <% using(Html.BeginForm())
               {%>
               <%: Html.AntiForgeryToken("EditBlogPost") %>
               <%: Html.EditorForModel() %>
               <p>
                    <a href="<%:Url.Action("cancelblogpostedition")%>"><button name="cancel" type="button" class="form-submit negative">Cancel</button></a>
                    <input class="form-submit mild" type="submit" name="submitButton" value="Save" />
                    <input class="form-submit positive" type="submit" name="submitButton" value="Publish" />
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
