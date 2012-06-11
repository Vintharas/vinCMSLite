<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.BlogPost>" %>
<%@ Import Namespace="vinCMS.Helpers.Routing" %>

<iframe src="http://www.facebook.com/plugins/like.php?href=<%: Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + Url.RouteUrl(Model.GetBlogDetails()) %>&amp;layout=standard&amp;show_faces=true&amp;width=450&amp;action=like&amp;colorscheme=light&amp;height=80" 
scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:450px; height:80px;" allowTransparency="true"></iframe>