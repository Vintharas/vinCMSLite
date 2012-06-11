using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Domain.Entities;
using DomainRepos.Abstracts;
using DomainStorage.EFStorage;
using vinCMS.Helpers.Routing;
using PagedList;
using vinCMS.Infraestructure;
using vinCMS.Models;
using vinCMS.Infraestructure.Filters;

namespace vinCMS.Controllers
{

    [ExceptionError]
    public class BlogController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepo;
        private IBlogPostRepository BlogPostRepo { 
            get { return _blogPostRepo; } 
        }

        public const int POSTS_PER_PAGE = 5;
        public const int NUMBER_OF_RELATED_POSTS = 5;

        /// <summary>
        /// Blog Controller constructor. Initializes the blogPostRepository that
        /// will allow the controller to access the repository of BlogPosts
        /// </summary>
        /// <param name="blogPostRepo"></param>
        public BlogController(IBlogPostRepository blogPostRepo)
        {
            _blogPostRepo = blogPostRepo;
        }

        #region Action Methods

        /// <summary>
        /// Index Action Method. Renders a PagedList of BlogPosts
        /// </summary>
        /// <returns></returns>
        public ViewResult Index(int page = 0)
        {
            var blogPostViewModel =
                new BlogPostViewModel
                    {
                        BlogPostPagedList =
                            new StaticPagedList<BlogPost>(BlogPostRepo.GetOrderedQueryableBlogPostWithIncludes().Skip(page*POSTS_PER_PAGE).Take(POSTS_PER_PAGE).ToList(),
                                                    // IEnumerable<BlogPost>
                                                    page, // Page
                                                    POSTS_PER_PAGE,
                                                    BlogPostRepo.GetOrderedQueryableBlogPostWithIncludes().Count()),
                        // Number of posts per page   
                        Title = "Blog"
                    };
            return View(blogPostViewModel);
        }

        /// <summary>
        /// Detail action method. Renders the details of a selected blogpost
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(int year, int month, int day, string path)
        {
            // Grab blog post from db
            BlogPost blogPost = BlogPostRepo.GetByPath(year, month, day, path);
            // The url is canonical because path is a UK in the database and we
            // use year, month, and day in the query
            BlogPostDetailModel blogPostDetailModel = new BlogPostDetailModel()
                                                          {
                                                              BlogPost = blogPost,
                                                              RelatedBlogPosts =
                                                                  _blogPostRepo.GetListOfRelatedBlogPosts(
                                                                      blogPost,
                                                                      NUMBER_OF_RELATED_POSTS),
                                                              NewComment = new Comment()
                                                          };
            return View(blogPostDetailModel);
        }

        /// <summary>
        /// Action method that manages the post from a comment form within a blog post detail
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="path"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Transaction]
        public ActionResult Details(int year, int month, int day, string path,[Bind(Exclude="CommentID")] Comment comment)
        {
            // grab blogPost from context
            BlogPost blogPost = BlogPostRepo.GetByPath(year, month, day, path);
            // Create new blogPostDetailModel
            BlogPostDetailModel blogPostDetailModel;
            // grab related posts
            var relatedBlogPosts = BlogPostRepo.GetListOfRelatedBlogPosts(blogPost, NUMBER_OF_RELATED_POSTS);
            // check validation
            if (!ModelState.IsValid)
            {
                blogPostDetailModel = new BlogPostDetailModel
                                          {
                                              BlogPost = blogPost,
                                              NewComment = comment,
                                              RelatedBlogPosts = relatedBlogPosts
                                          };
                return View(blogPostDetailModel);
            }
            // if model is valid then add comment
            BlogPostRepo.AddCommentToBlogPost(blogPost, comment);
            // BlogPostRepo.SubmitChanges();
            return RedirectToAction("Details", blogPost.GetBlogDetails());
        }

        /// <summary>
        /// Tag action method. Renders a view with blogposts filtered by the tag whose name
        /// is "tagName"
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [OutputCache(Duration=600, VaryByParam="page; tagName")]
        public ViewResult Tag(string tagName, int page = 0)
        {
            // Recover original tagName
            tagName = RoutingHelpers.RecoverOriginalUrlSegment(tagName);
            // Create paged list of BlogPosts filtered by tag
            var blogPostViewModel =
                new BlogPostViewModel
                    {
                        BlogPostPagedList =
                            new StaticPagedList<BlogPost>(BlogPostRepo.GetQueryableBlogPostByTagName(tagName).Skip(page * POSTS_PER_PAGE).Take(POSTS_PER_PAGE).ToList(),
                                                    // Blog posts filtered tag name
                                                    page, // page
                                                    POSTS_PER_PAGE,
                                                    BlogPostRepo.GetQueryableBlogPostByTagName(tagName).Count()),
                        // posts per page
                        Title = "Blog posts with tag: " + tagName
                    };
                
            // Set message
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = "Filtering blog posts by tag: " + tagName;
            // render view
            return View("Index", blogPostViewModel);
        }


        /// <summary>
        /// Category action method. Renders a view with blogposts fileterd by the category whose name is "categoryName"
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [OutputCache(Duration=600, VaryByParam="page; categoryName")]
        public ViewResult Category(string categoryName, int page = 0)
        {
           // Recover original categoryName
            categoryName = RoutingHelpers.RecoverOriginalUrlSegment(categoryName);
           // Create paged list of blogposts filtered by category
            var blogPostViewModel =
                new BlogPostViewModel
                    {
                        BlogPostPagedList =
                            new StaticPagedList<BlogPost>(BlogPostRepo.GetQueryableBlogPostByCategory(categoryName).Skip(page * POSTS_PER_PAGE).Take(POSTS_PER_PAGE).ToList(),
                                                    //Blog posts filtered by category name
                                                    page, // page
                                                    POSTS_PER_PAGE,
                                                    BlogPostRepo.GetQueryableBlogPostByCategory(categoryName).Count()),
                        // posts per page
                        Title = "Blog posts with category: " + categoryName
                    };
            // Set message
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = "Filtering blog posts by category: " + categoryName;
            // render view
            return View("Index", blogPostViewModel);
        }

        /// <summary>
        /// Action method that returns an rss feed
        /// </summary>
        /// <returns></returns>
        public ActionResult RssSubscribe()
        {
            string encoding = Response.ContentEncoding.WebName;
            string domainUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + "/blog";
            XDocument rss = BlogPostRepo.GetRssForBlogPosts(encoding, domainUrl);
            return Content(rss.ToString(), "application/rss+xml");
        }

        #endregion

        #region Auxiliary Methods

        /// <summary>
        /// Checks whether or not the given url is canonical and has the proper format
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="title"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsCanonicalUrl(BlogPost blogPost, int year, int month, int day, string title, int id)
        {
            // To avoid accessing url helper and couple the controller with the routing system
            // I use simple yet not very good looking check
            return (blogPost.ContentID == id &&
                    blogPost.PublishingDate.Year == year &&
                    blogPost.PublishingDate.Month == month &&
                    blogPost.PublishingDate.Day == day &&
                    Helpers.Routing.RoutingHelpers.MakeSimpleUrlSegment(blogPost.Title) == title);
            /* RouteValueDictionary receivedRouteValues = new RouteValueDictionary
                                                           {
                                                               {"controller", "blog"},
                                                               {"action", "details"},
                                                               {"year", year},
                                                               {"month", month},
                                                               {"day", day},
                                                               {"title", title},
                                                               {"id", id}
                                                           };
             * */
            // If the received route is equal to the expected canonical route then True!
            // return Url.RouteUrl(receivedRouteValues) == Url.RouteUrl(expectedRouteValues);
        }

        #endregion


    }
}
