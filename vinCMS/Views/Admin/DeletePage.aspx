<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.Page>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Admin - Delete Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete the selected Page</h2>
            <% using(Html.BeginForm())
               {%>
               <%: Html.AntiForgeryToken("DeletePage") %>
               <p> Are you sure you want to delete the page &quot;<%: Model.Title %>&quot;?</p>
               <%: Html.EditorFor(x => x.ContentID) %>
               <p>
                    <a href="<%:Url.Action("cancelpagedeletion")%>"><button name="cancel" type="button" class="form-submit negative">Cancel</button></a>
                    <input class="form-submit positive" type="submit" name="submitButton" value="Delete" />
                </p>
            <%
               }%>  
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>