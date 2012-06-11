<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.BlogPost>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>


   <a href="http://twitter.com/share" class="twitter-share-button"
      data-url="<%: Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + Url.RouteUrl(Model.GetBlogDetails()) %>"
      data-via="vintharas"
      data-text="<%: Model.Title %>"
      data-related="vintharas"
      data-count="vertical"
      counturl="<%: Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + Url.RouteUrl(Model.GetBlogDetails()) %>">Tweet</a>