<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList.IPagedList<Domain.Entities.BlogPost>>" %>
<%@ Import Namespace="vinCMS.Helpers.Html" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>

<div class="blog-postlist">
           <% if (Model.Count == 0)
              {%>
              <div>
                    <p> Sorry. There are no blog posts under the selected criteria</p>
              </div>
             
           <%
              }%>
           <% foreach (var blogPost in Model.ToList())
           {%>
           <div>
                <h2><%: Html.RouteLink(blogPost.Title, blogPost.GetBlogDetails()) %></h2>
                <div class="post-signature"> 
                    <span><%: blogPost.PublishingDate.ToString("MMMM dd, yyyy") %></span>. By <span><%: blogPost.User.UserName %></span>
                </div>
                <div class="post-content">
                    <%= Html.GetSafeHtml(blogPost.Summary) ?? Html.GetSafeHtml(blogPost.BodyContent) %>
                </div>
                <div class="post-tags"> 
                    Tags: <% Html.RenderPartial("BlogPostTags", blogPost.Tags.ToList()); %>
                </div>
                <div class="post-categories"> 
                    Categories: <% Html.RenderPartial("BlogPostCategories", blogPost.Categories.ToList()); %>
                </div>
                <div id="post-actions" class="span-12 last">
                    <div class="read-more span-5">
                        <%: Html.RouteLink("Continue Reading", blogPost.GetBlogDetails()) %>
                    </div>
                    <div class="comment span-6 last">
                        <span>
                          <%if (blogPost.Comments.Count == 0)
                          {%>
                            &#123; <a href="<%: Url.RouteUrl(blogPost.GetBlogDetails()) + "#comment-list" %>" title="Add a comment!">Add a Comment</a> &#125;
                          <%}
                          else if (blogPost.Comments.Count == 1)
                          {%>
                            &#123; <a href="<%: Url.RouteUrl(blogPost.GetBlogDetails()) + "#comment-list" %>" title="Add a comment!">1 Comment</a> &#125;
                          <%}
                          else
                          {%>
                            &#123; <a href="<%: Url.RouteUrl(blogPost.GetBlogDetails()) + "#comment-list" %>" title="Add a comment!"><%: blogPost.Comments.Count + " Comments" %></a> &#125;
                          <%
                          }%>
                        </span>
                    </div>
                </div>
            </div>
        <%
           }%>
</div>