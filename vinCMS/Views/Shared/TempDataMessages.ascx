<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<% if (TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] != null)
    {%>
    <div class="message message-info"><%: TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] %></div>
<%
    }%>
<% if (TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE_ERROR] != null)
   {%>
   <div class="message message-error"><%: TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE_ERROR] %></div>
<%
   }%>