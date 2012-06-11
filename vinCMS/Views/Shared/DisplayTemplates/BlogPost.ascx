<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Domain.Entities.BlogPost>" %>
<%@ Import Namespace="vinCMS.Helpers.Html" %>

                <div class="details-post-title span-18 last">
                    <div class="span-15">
                        <h1><%: Model.Title %></h1>
                    </div>
                    <div id="tweet" class="span-3 last">
                        <% Html.RenderPartial("TweetButton", Model); %>
                    </div>
                </div>
                <div class="span-18 last">
                    <div class="details-post-summary">
                        <%= Html.GetSafeHtml(Model.Summary) %>
                    </div>
                    <div class="details-post-signature">
                        <p><%: "By " + Model.User.UserName + ". " + Model.PublishingDate.ToString("hh:mm, MMMM dd, yyyy")%></p>
                    </div>
                    <div class="details-post-bodycontent">
                        <%= Html.GetSafeHtml(Model.BodyContent) %>
                    </div>        
                </div>



