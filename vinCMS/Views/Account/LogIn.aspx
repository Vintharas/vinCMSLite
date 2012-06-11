<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<vinCMS.Models.UserViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Log In
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
          <div id="content" class="container">
            <div id="login-form" class="span-17 last">
                <h1>Log in the Website!</h1>
                <p class="alt">
                    Type in your username and password to access the dark admin side of the website
                </p>
                <% Html.EnableClientValidation(); %>
                <% using (Html.BeginForm())
                    {%>
                    <%: Html.AntiForgeryToken("LogIn") %>
                    <div>
                        <span class="form-label"><%: Html.LabelFor(x => x.UserName) %></span>
                        <%: Html.TextBoxFor(x => x.UserName, new { @class = "form-control form-short"})%>
                        <%: Html.ValidationMessageFor(x => x.UserName) %>
                    </div>
                    <div>
                        <span class="form-label"><%: Html.LabelFor(x => x.Password) %></span>
                        <%: Html.PasswordFor(x => x.Password, new { @class = "form-control form-short" })%>
                        <%: Html.ValidationMessageFor(x => x.Password) %>
                    </div>
                    <div>
                        <a href="<%:Url.Action("cancellogin")%>"><button name="cancel" type="button" class="form-submit negative">Cancel</button></a>
                        <input type="submit" name="submitButton" value="Log In" class="form-submit positive" />
                    </div>
                <%}%>
                <% Html.RenderPartial("TempDataMessages"); %>
            <p class="alt">
                Note that if you are not using SSL your username and password will travel the internet happily in plain text. So it would be wise not to log in when using open wireless networks ;)
            </p>
            </div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
            <script src="/public/javascript/MicrosoftAjax.js" type="text/javascript" ></script>
            <script src="/public/javascript/MicrosoftMvcValidation.js" type="text/javascript" ></script>
</asp:Content>
