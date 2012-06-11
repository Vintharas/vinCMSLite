<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Domain.Entities.BlogPost>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Barbarian Meets Coding - Blog Post Preview
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

            <div id="content" class="container">
                <div class="span-18 last">  
                     <p class="caps">Preview</p>
                     <%: Html.DisplayForModel() %>
                     <div>
                        <a href="<%:Url.Action("editblogpost", new{id = Model.ContentID})%>"><button name="edit" type="button" class="form-submit mild">Edit</button></a>
                        <a href="<%:Url.Action("publishblogpost", new{id = Model.ContentID}) %>"><button name="publish" type="button" class="form-submit positive">Publish</button></a>
                    </div>
                </div>
            </div>

</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
            <script src="http://platform.twitter.com/widgets.js" type="text/javascript"></script>
</asp:Content>
