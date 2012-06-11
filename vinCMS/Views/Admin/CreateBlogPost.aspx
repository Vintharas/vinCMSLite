<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.BlogPost>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Create a New BlogPost
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">  
            <h2>Create a New Blog Post</h2>
            
            <% Html.EnableClientValidation(); %>
            <% using(Html.BeginForm())
               {%>
               <%: Html.EditorForModel() %>
                <%: Html.AntiForgeryToken("CreateBlogPost") %>
                <p>
                    <a href="<%:Url.Action("cancelblogpostcreation")%>"><button name="cancel" type="button" class="form-submit negative">Cancel</button></a>

                    <input class="form-submit mild" type="submit" name="submitButton" value="Preview" />
                    <input class="form-submit positive" type="submit" name="submitButton" value="Publish" />
                </p>
            <% }%>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
            <script src="/public/javascript/MicrosoftAjax.js" type="text/javascript" ></script>
            <script src="/public/javascript/MicrosoftMvcValidation.js" type="text/javascript" ></script>
</asp:Content>
