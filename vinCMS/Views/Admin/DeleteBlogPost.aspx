<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.BlogPost>" %>
<%@ Import Namespace="vinCMS.Helpers.Html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DeleteBlogPost
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete Blog Post</h2>
            <% using(Html.BeginForm())
               {%>
               <%: Html.AntiForgeryToken("DeleteBlogPost") %>
               <p> Are you sure you want to delete this blog post?</p>
               <%: Html.EditorFor(x => x.ContentID) %>
               <p>
                    <a href="<%:Url.Action("cancelblogpostdeletion")%>"><button name="cancel" type="button" class="form-submit negative">Cancel</button></a>
                    <input class="form-submit positive" type="submit" name="submitButton" value="Delete" />
                </p>
            <%
               }%>  
    <h3><%:Model.Title%></h3>
    <div><%= Html.GetSafeHtml(Model.Summary) %></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
