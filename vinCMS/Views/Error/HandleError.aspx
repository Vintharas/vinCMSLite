<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<vinCMS.Models.ErrorViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Error!
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="content container">
        <div class="span-18 last image-error">
            <img alt="error" src="<%: "/public/images/error" + Model.StatusError.ToString() + ".png" %>" />
        </div>
        <% if (Page.User.Identity.IsAuthenticated)
           {%>
            <div class="admin-info span-18 last alt">
                <p>
                 Secret error Info: <%: Model.Ex != null? Model.Ex.Message : "No exception was thrown..." %>
                </p>
            </div>
        <%
           }%>
    </div>
    

</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
