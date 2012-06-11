<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

            <div id="accordion">
                <h3><a href="#">Blog and Pages</a></h3>
                <ul id="blog-buttons">
                    <li><%: Html.ActionLink("Dashboard Index", "index") %></li>
                    <li><%: Html.ActionLink("Add New Post", "createblogpost")%></li>
                    <li><%: Html.ActionLink("Add New Page", "createpage") %></li>
                    <li><%: Html.ActionLink("Manage Tags", "tags") %></li>
                    <li><%: Html.ActionLink("Manage Categories", "categories") %></li>
                </ul>
                <h3><a href="#">Settings</a></h3>
                <ul id="settings-buttons">
                    <li><%: Html.ActionLink("Manage Media", "media") %></li>
                    <li><%: Html.ActionLink("Preferences", "preferences") %></li>
                    <li><%: Html.ActionLink("Site Settings", "sitesettings") %></li>
                </ul>
                <h3><a href="#">Security</a></h3>
                <ul id="security-buttons">
                    <li><%: Html.ActionLink("Users", "users") %></li>
                    <li><%: Html.ActionLink("Roles", "roles") %></li>    
                </ul>
                <h3><a href="#">User</a></h3>
                <ul id="user-buttons">
                    <li><%: Html.RouteLink("Log out", new { controller="account", action="logout" }) %></li>
                </ul>
            </div>