<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.BlogPost>" %>

<%@Import Namespace="vinCMS.Infraestructure" %>

        <%: Html.EditorFor(x => x.ContentID) %>

        <div class="form-label"><%: Html.LabelFor(x => x.Title) %></div>
        <div>
            <%: Html.TextBoxFor(x => x.Title, new { @class = "form-control-title"})%>
            <p><%: Html.ValidationMessageFor(x => x.Title) %></p>
        </div>

        <div class="form-control form-long"><%:Html.EditorFor(x => x.CreationDate) %></div>
        <div class="form-control form-long"><%:Html.EditorFor(x => x.PublishingDate) %></div>
        <div class="form-control form-long"><%:Html.EditorFor(x => x.AuthorID) %></div>
        <div class="form-control form-long"><%:Html.EditorFor(x => x.IsDraft) %></div>
        
        <div>
            <span class="form-label"><%: Html.LabelFor(x => x.Path) %></span>
            <span class="alt">{domain}/{publishing date}/ </span><%: Html.TextBoxFor(x => x.Path, new{ @class = "form-control form-short"}) %>  
            <p><%: Html.ValidationMessageFor(x => x.Path) %></p>   
        </div>

        <div class="form-label"><%: Html.LabelFor(x => x.Summary) %></div>
        <div>
            <%: Html.EditorFor(x => x.Summary)%>
            <p><%: Html.ValidationMessageFor(x => x.Summary)%></p>   
         </div>
        
        <div class="form-label"><%: Html.LabelFor(x => x.BodyContent) %></div>
        <div>
            <%: Html.EditorFor(x => x.BodyContent)%>
            <p><%: Html.ValidationMessageFor(x => x.BodyContent)%></p>    
        </div>
        
        <div class="form-label"><%: Html.LabelFor(x => x.MetaDescription) %></div>
        <div>
            <%: Html.EditorFor(x => x.MetaDescription)%>
            <p><%: Html.ValidationMessageFor(x => x.MetaDescription)%></p>    
        </div>
       
        <div class="form-label"><%: Html.LabelFor(x => x.Categories) %></div>
        <div class="form-category-list">
            <% foreach (var category in (IEnumerable<Domain.Entities.Category>)ViewData[Constants.VIEW_CATEGORYLIST])
               {%>
                <div>
                    <input name="CategoryID" type="checkbox" value="<%:category.CategoryID%>" <%: Model.Categories.Any(x => x.CategoryID == category.CategoryID) ? "checked" : "" %> /> <%: category.Name %>
                </div>
            <%
               }%>
        </div>

        <div class="form-label"><%: Html.LabelFor(x => x.Tags) %></div>
        <div>
            <%: Html.TextArea(Constants.VIEW_TAGLIST, Model.GetStringOfTags(), new { @class="form-control form-small"}) %>
            <p class="alt"> Add the tags to your post as a list of tags separated by commas (f.e. cats, dogs, mice)</p>
            <p class="alt"> Only low-case alphanumeric characters will be registered (i.e. [a-z] [0-9]</p>
        </div>
        
        
        
