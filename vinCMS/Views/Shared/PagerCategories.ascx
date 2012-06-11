<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PagedList.IPagedList<Domain.Entities.Category>>" %>
<%@ Import Namespace="vinCMS.Models" %>

<div class="pager">
       <span><% if (Model.HasPreviousPage)
                { %>
                  <%:Html.ActionLink("Newer Entries", "Index", new {page = Model.GetPreviousPageIndex()}) %>
                <%} %></span>

       <span><%if (Model.HasNextPage)
               {%></span>
               <%:Html.ActionLink("Older Entries", "Index", new {page = Model.GetNextPageIndex()}) %>
       <%
               }%>
</div>
