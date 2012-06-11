<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<vinCMS.Models.BlogPostDetailModel>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>


        <div id="related-content" class="span-18 last">
            <h2 class="section-header caps">RELATED BLOG POSTS</h2>
            <div class="box span-17 last gray-box">
                <ul>
                    <% if (Model.HasRelatedBlogPosts)
                       {%>
                            <% foreach (var post in Model.RelatedBlogPosts)
                               {%>
                                <li><%:Html.RouteLink(post.Title, post.GetBlogDetails())%></li>
                            <% }%> 
                   <%  }
                       else
                       {%>
                            <li> There are no related posts!</li>
                       <%}%>
               </ul>
            </div>
        </div>