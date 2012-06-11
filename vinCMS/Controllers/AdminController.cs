using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainRepos.Abstracts;
using Domain.Entities;
using PagedList;
using vinCMS.Models;
using System.Web.Security;
using vinCMS.Infraestructure.Authentication;
using vinCMS.Helpers.Routing;
using System.IO;
using vinCMS.Infraestructure.Filters;

namespace vinCMS.Controllers
{
    [Authorize]
    [ValidateInput(false)]
    public class AdminController : Controller
    {

        private readonly IBlogPostRepository _blogPostRepo;


        private IBlogPostRepository BlogPostRepo { get { return _blogPostRepo; } }

        public const int POSTS_PER_PAGE = 5;
        public const int MAX_DRAFT_NUMBER = 50;
        public const int CATEGORIES_PER_PAGE = 20;
        public const int TAGS_PER_PAGE = 20;
        public const int MEDIA_PER_PAGE = 20;

        #region messages

        public const string CANCEL_DELETE = "The deletion of the blog post has been cancelled";
        public const string CANCEL_CREATE = "The creation of the blog post has been cancelled";
        public const string CANCEL_EDIT = "The edition of the blog post has been cancelled";
        public const string SUCCESS_DELETE = "The blog post has been successfully deleted";
        public const string SUCCESS_PUBLISH = "The selected blog post has been successfully published";
        public const string SUCCESS_EDIT = "The blogpost has been successfully edited";
        public const string SUCCESS_EDITPUBLISH = "The blogpost has been successfully edited and published";
        public const string SUCCESS_EDIT_ALREADY_PUBLISHED = "The blogpost has been successfully edited. It was already published";
        public const string ALREADY_PUBLISHED = "The selected blog post has already been published";

        public const string SUCCESS_CATEGORY_ADD = "The category has been successfully created";
        public const string SUCCESS_DELETE_CATEGORY = "The category has been successfully deleted";
        public const string CANCEL_EDIT_CATEGORY = "The edition of the category has been cancelled";
        public const string SUCCESS_EDIT_CATEGORY = "The category has been successfully edited";
        public const string CANCEL_DELETE_CATEGORY = "The deletion of the category has been cancelled";

        public const string SUCCESS_TAG_ADD = "The tag has been successfully created";
        public const string SUCCESS_EDIT_TAG = "The tag has been successfully edited";
        public const string CANCEL_EDIT_TAG = "The edition of the category has been cancelled";
        public const string SUCCESS_DELETE_TAG = "The tag has been successfully deleted";
        public const string CANCEL_DELETE_TAG = "The deletion of the tag has been cancelled";

        public const string SUCCESS_DELETE_COMMENT = "The comment has been successfully deleted";
        public const string CANCEL_DELETE_COMMENT = "The deletion of the comment has been cancelled";

        public const string NO_FILE_UPLOADED = "No file was uploaded to the server";
        public const string SUCCESS_FILE_UPLOAD = "The file has been successfully uploaded";
        public const string CANCEL_DELETE_MEDIA = "The deletion of the file has been cancelled";
        public const string SUCCESS_DELETE_MEDIA = "The file has been successfully deleted";

        public const string CANCEL_CREATE_PAGE = "The creation of a new page has been cancelled";
        public const string SUCCESS_PAGE_CREATE = "The page has been successfully created";
        public const string CANCEL_EDIT_PAGE = "The edition of the selected page has been cancelled";
        public const string SUCCESS_EDIT_PAGE = "The selected page has been successfully edited";
        public const string CANCEL_DELETE_PAGE = "The deletion of the selected page has been cancelled";
        public const string SUCCESS_DELETE_PAGE = "The selected page has been successfully deleted";


        #endregion

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="blogPostRepo"></param>
        public AdminController(IBlogPostRepository blogPostRepo)
        {
            _blogPostRepo = blogPostRepo;
        }

        /// <summary>
        /// Action method that renders the index view for the administration pages
        /// </summary>
        /// <returns></returns>
        public ViewResult Index(int page = 0)
        {
            // paged list of published blogs
            var pagedListOfPublishedPosts = new StaticPagedList<BlogPost>(
                BlogPostRepo.GetOrderedQueryableBlogPost().Skip(page*POSTS_PER_PAGE).Take(POSTS_PER_PAGE).ToList(),
                page,
                POSTS_PER_PAGE,
                BlogPostRepo.GetQueryableEntitySet().Count()
                );
            // list of blog post drafts
            List<BlogPost> listOfPostDrafts =
                BlogPostRepo.GetQueryableEntitySet().Where(x => x.IsDraft == true).OrderByDescending(x => x.CreationDate)
                    .Take(MAX_DRAFT_NUMBER).ToList();
            // list of pages
            List<Page> listOfPages = BlogPostRepo.GetOrderedListOfPages();

            // Set up view model
            var adminViewModel = new AdminViewModel
                                     {
                                         PagedListBlogPosts = pagedListOfPublishedPosts,
                                         PostDraftsList = listOfPostDrafts,
                                         PagesList = listOfPages
                                     };
            return View(adminViewModel);
        }

        #region Blog Post action methods

        /// <summary>
        /// Action method that manages the request of creating a new blog post
        /// and renders the appropiate view to carry out that task
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult CreateBlogPost()
        {
            // Create new blogPost
            BlogPost newBlogPost = BlogPostRepo.CreateNewBlogPost(this.User.Identity.Name);
            // get list of all categories
            ViewData[vinCMS.Infraestructure.Constants.VIEW_CATEGORYLIST] =
                BlogPostRepo.GetListOfCategories();
            return View(newBlogPost);
        }

        /// <summary>
        /// Action that manages the post request of a create new blog post
        /// form
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="categoryID"></param>
        /// <param name="listOfTags"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateBlogPost")]
        [ValidateAntiForgeryToken(Salt = "CreateBlogPost")]
        [Transaction]
        public ActionResult CreateBlogPost(BlogPost blogPost, int[] categoryID, string listOfTags, string submitButton)
        {
            if (!ModelState.IsValid)
            {
                // get list of all categories again, and show form
                ViewData[vinCMS.Infraestructure.Constants.VIEW_CATEGORYLIST] =
                    BlogPostRepo.GetListOfCategories();
                return View(blogPost);
            }
            else
            {
                switch (submitButton)
                {
                    case "Preview":
                        return CreateBlogPost_OnPreview(blogPost, categoryID, listOfTags);
                    case "Publish":
                        return CreateBlogPost_OnPublish(blogPost, categoryID, listOfTags);
                    default:
                        return RedirectToAction("Index");
                }
            }
        }

        /// <summary>
        /// Action methods that manages the cancel process of a blog post that was being created
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelBlogPostCreation()
        {
            // Add message and Redirect to Admin index
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_CREATE;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action method that shows a preview for a given blogPost
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewBlogPost(int id)
        {
            // Get blog post
            BlogPost blogPost = BlogPostRepo.GetById(id);
            // Render Preview/Publish view
            return View(blogPost);
        }

        /// <summary>
        /// Action method that shows a form to edit a given blog post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult EditBlogPost(int id)
        {
            // Get post from context
            BlogPost blogPost = BlogPostRepo.GetById(id);
            // get list of all categories
            ViewData[vinCMS.Infraestructure.Constants.VIEW_CATEGORYLIST] =
                BlogPostRepo.GetListOfCategories();
            // render edition view
            return View(blogPost);
        }

        /// <summary>
        /// Action method that receives a post from the form whose purpose is to edit a blogpost
        /// </summary>
        /// <param name="categoryID"></param>
        /// <param name="listOfTags"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "EditBlogPost")]
        [Transaction]
        public ActionResult EditBlogPost(int ContentID, int[] categoryID, string listOfTags, string submitButton)
        {
            // Get blogPost from context
            // * review this * if the blogpost have been changed, and there are validation errors
            // we are returning the fresh blogpost from db.
            BlogPost blogPost = BlogPostRepo.GetById(ContentID);
            if (ModelState.IsValid)
            {
                return UpdateEditedBlogPost(blogPost, categoryID, listOfTags, submitButton);
            }
            else
            {
                // Model is not valid (based on defined validation rules)
                // thus we will show the form again
                // get list of all categories again, and show form
                ViewData[vinCMS.Infraestructure.Constants.VIEW_CATEGORYLIST] = BlogPostRepo.GetListOfCategories();
                return View(blogPost);
            }
        }

        /// <summary>
        /// Action methods that manages the cancel process of a blog post that was being edited
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelBlogPostEdition()
        {
            // Add message and Redirect to Admin index
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_EDIT;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// DeleteBlogPost get action method. Renders a view in which the user is prompted about the deletion
        /// of a given blog post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult DeleteBlogPost(int id)
        {
            // Get post from context
            BlogPost blogPost = BlogPostRepo.GetById(id);
            // render delete view
            return View(blogPost);
        }

        /// <summary>
        /// DeleteBlogPost post method. Deletes the blog post whose id is ContentID
        /// </summary>
        /// <param name="ContentID"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteBlogPost")]
        [ValidateAntiForgeryToken(Salt = "DeleteBlogPost")]
        [Transaction]
        public ActionResult DeleteBlogPost_Post(int ContentID)
        {
            // Delete blogPost
            BlogPost blogPost = BlogPostRepo.GetById(ContentID);
            BlogPostRepo.Delete(blogPost);
            // BlogPostRepo.SubmitChanges();
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_DELETE;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action methods that manages the cancel process of a blog post that was being created
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelBlogPostDeletion()
        {
            // Add message and Redirect to Admin index
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_DELETE;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action method that publishes a draft blog post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction]
        public ActionResult PublishBlogPost(int id)
        {
            BlogPost blogPost = BlogPostRepo.GetById(id);
            if (blogPost.IsDraft)
            {
                BlogPostRepo.PublishBlogPost(blogPost);
                TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_PUBLISH;
                //BlogPostRepo.SubmitChanges();
            }
            else
            {
                TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = ALREADY_PUBLISHED;
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Category action methods

        /// <summary>
        /// Action method that renders a form to manage categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Categories(int page = 0)
        {
            // get IQueryable of categories
            StaticPagedList<Category> pagedListCategories = new StaticPagedList<Category>(
                BlogPostRepo.GetQueryableOrderedCategories().Skip(page*CATEGORIES_PER_PAGE).Take(CATEGORIES_PER_PAGE).ToList(),
                page,
                CATEGORIES_PER_PAGE,
                BlogPostRepo.GetQueryableCategories().Count());
            // prepare view model
            AdminCategoryViewModel categoryViewModel = new AdminCategoryViewModel
                                                           {
                                                               NewCategory = new Category(),
                                                               PagedListCategories = pagedListCategories
                                                           };
            // renders view
            return View(categoryViewModel);
        }

        /// <summary>
        /// Action method that manages the post of the forms present in the category management dashboard
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "Categories")]
        [Transaction]
        public ActionResult Categories(Category category)
        {
            if (ModelState.IsValid)
            {
                // add to context
                BlogPostRepo.AddCategory(category);
                // submit changes
                // BlogPostRepo.SubmitChanges();
                // redirect to category management index 
                TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_CATEGORY_ADD;
                return RedirectToAction("categories");
            }
            else
            {
                // return the same view
                var catViewModel = new AdminCategoryViewModel
                {
                    NewCategory = category,
                    PagedListCategories = new PagedList.StaticPagedList<Category>(
                        BlogPostRepo.GetQueryableOrderedCategories().Skip(0).Take(CATEGORIES_PER_PAGE).ToList(),
                        0,
                        CATEGORIES_PER_PAGE,
                        BlogPostRepo.GetQueryableCategories().Count()
                        )
                };
                // the view will show a series of validation error messages
                return View(catViewModel);
            }
        }

        /// <summary>
        /// Action method that renders a form in which to edit a given category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult EditCategory(int id)
        {
            // Get category from context
            Category category = BlogPostRepo.GetCategoryById(id);
            // render form
            return View(category);
        }

        /// <summary>
        /// Action method that manages the post of the category edition form
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("EditCategory")]
        [ValidateAntiForgeryToken(Salt = "EditCategory")]
        [Transaction]
        public ActionResult EditCategory_Post(int categoryID)
        {
            // Get category from db
            Category category = BlogPostRepo.GetCategoryById(categoryID);
            // Check validation
            if (ModelState.IsValid)
            {
                TryUpdateModel(category);
                //BlogPostRepo.SubmitChanges();
                TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_EDIT_CATEGORY;
                return RedirectToAction("categories");
            }
            else
            {
                return View(category);
            }
        }

        /// <summary>
        /// Method that cancels the edition of a category
        /// and redirects to the main category management dashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelCategoryEdition()
        {
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_EDIT_CATEGORY;
            return RedirectToAction("categories");
        }

        /// <summary>
        /// Action method that renders a deletion form for a given category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult DeleteCategory(int id)
        {
            // grab category from context
            Category category = BlogPostRepo.GetCategoryById(id);
            // render form
            return View(category);
        }

        /// <summary>
        /// Action method that manages the post from a category deletion form
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken(Salt = "DeleteCategory")]
        [Transaction]
        public ActionResult DeleteCategory_Post(int categoryID)
        {
            // delete category
            BlogPostRepo.DeleteCategoryById(categoryID);
            // BlogPostRepo.SubmitChanges();
            // redirect to category management dashboard
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_DELETE_CATEGORY;
            return RedirectToAction("categories");
        }

        /// <summary>
        /// Action method that manages the cancellation of a category deletion
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelCategoryDeletion()
        {
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_DELETE_CATEGORY;
            return RedirectToAction("categories");
        }


        #endregion

        #region Tag action methods

        /// <summary>
        /// Action method that renders a form to manage tags
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Tags(int page = 0)
        {
            // get IQueryable of categories
            StaticPagedList<Tag> pagedListTags = new StaticPagedList<Tag>(
                BlogPostRepo.GetQueryableOrderedTags().Skip(page * TAGS_PER_PAGE).Take(TAGS_PER_PAGE).ToList(),
                page,
                TAGS_PER_PAGE,
                BlogPostRepo.GetQueryableTags().Count());
            // prepare view model
            AdminTagViewModel tagViewModel = new AdminTagViewModel
            {
                NewTag = new Tag(),
                PagedListTags = pagedListTags
            };
            // renders view
            return View(tagViewModel);
        }

        /// <summary>
        /// Action method that manages the post of the forms present in the tag management dashboard
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "Tags")]
        [Transaction]
        public ActionResult Tags(Tag tag)
        {
            if (ModelState.IsValid)
            {
                // add to context
                BlogPostRepo.AddTag(tag);
                // submit changes
                // BlogPostRepo.SubmitChanges();
                // redirect to category management index 
                TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_TAG_ADD;
                return RedirectToAction("tags");
            }
            else
            {
                // return the same view
                var tagViewModel = new AdminTagViewModel
                {
                    NewTag = tag,
                    PagedListTags = new PagedList.StaticPagedList<Tag>(
                        BlogPostRepo.GetQueryableOrderedTags().Skip(0).Take(TAGS_PER_PAGE).ToList(),
                        0,
                        TAGS_PER_PAGE,
                        BlogPostRepo.GetQueryableTags().Count()
                        )
                };
                // the view will show a series of validation error messages
                return View(tagViewModel);
            }
        }

        /// <summary>
        /// Action method that renders a form in which to edit a given tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult EditTag(int id)
        {
            // Get tag from context
            Tag tag = BlogPostRepo.GetTagById(id);
            // render form
            return View(tag);
        }

        /// <summary>
        /// Action method that manages the post of the tag edition form
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("EditTag")]
        [ValidateAntiForgeryToken(Salt = "EditTag")]
        [Transaction]
        public ActionResult EditTag_Post(int tagID)
        {
            // Get tag from db
            Tag tag = BlogPostRepo.GetTagById(tagID);
            // Check validation
            if (ModelState.IsValid)
            {
                TryUpdateModel(tag);
                // BlogPostRepo.SubmitChanges();
                TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_EDIT_TAG;
                return RedirectToAction("tags");
            }
            else
            {
                return View(tag);
            }
        }

        /// <summary>
        /// Method that cancels the edition of a tag
        /// and redirects to the main tag management dashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelTagEdition()
        {
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_EDIT_TAG;
            return RedirectToAction("tags");
        }

        /// <summary>
        /// Action method that renders a deletion form for a given tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult DeleteTag(int id)
        {
            // grab tag from context
            Tag tag = BlogPostRepo.GetTagById(id);
            // render form
            return View(tag);
        }

        /// <summary>
        /// Action method that manages the post from a category deletion form
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteTag")]
        [ValidateAntiForgeryToken(Salt = "DeleteTag")]
        [Transaction]
        public ActionResult DeleteTag_Post(int tagID)
        {
            // delete tag
            BlogPostRepo.DeleteTagById(tagID);
            // BlogPostRepo.SubmitChanges();
            // redirect to category management dashboard
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_DELETE_TAG;
            return RedirectToAction("tags");
        }

        /// <summary>
        /// Action method that manages the cancellation of a category deletion
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelTagDeletion()
        {
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_DELETE_TAG;
            return RedirectToAction("tags");
        }

        #endregion

        #region Media action methods

        /// <summary>
        /// Action method that renders a form to manage media files
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Media(int page = 0)
        {
            // get IQueryable of categories
            StaticPagedList<Media> pagedListMedia = new StaticPagedList<Media>(
                BlogPostRepo.GetQueryableOrderedMedia().Skip(page * MEDIA_PER_PAGE).Take(MEDIA_PER_PAGE).ToList(),
                page,
                MEDIA_PER_PAGE,
                BlogPostRepo.GetQueryableMedia().Count());
            // prepare view model
            AdminMediaViewModel mediaViewModel = new AdminMediaViewModel
            {
                NewMedia = new Media(),
                PagedListMedia = pagedListMedia
            };
            // renders view
            return View(mediaViewModel);
        }

        /// <summary>
        /// Action method that manages the post of the forms present in the tag management dashboard
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "Media")]
        [Transaction]
        public ActionResult Media(HttpPostedFileBase file)
        {
            // if there is no file, the form will be displayed again
            if (file == null)
            {
                TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE_ERROR] = NO_FILE_UPLOADED;
            }
            else
            {
                // format filename
                string formattedFileName = vinCMS.Helpers.Routing.RoutingHelpers.MakeSimpleFileSegment(file.FileName);
                // Upload file
                string savedFileNamePath = 
                    Path.Combine(
                    Server.MapPath(vinCMS.Infraestructure.Constants.MEDIA_PATH),
                    formattedFileName
                    );
                file.SaveAs(savedFileNamePath);
                // Generate Media entry in database
                BlogPostRepo.AddNewMedia(file.ContentLength, file.ContentType, formattedFileName, Path.Combine(vinCMS.Infraestructure.Constants.MEDIA_PATH, formattedFileName));
                // BlogPostRepo.SubmitChanges();
                TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_FILE_UPLOAD;
            }
            return RedirectToAction("media");
        }

        /// <summary>
        /// Action method that renders a media deletion form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult DeleteMedia(int id)
        {
            // get media
            Media media = BlogPostRepo.GetMediaById(id);
            // render deletion form
            return View(media);
        }

        /// <summary>
        /// Method that manages media deletion process
        /// </summary>
        /// <param name="mediaID"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("deletemedia")]
        [ValidateAntiForgeryToken(Salt = "DeleteMedia")]
        [Transaction]
        public ActionResult DeleteMedia_Post(int mediaID)
        {
            // grab media
            Media media = BlogPostRepo.GetMediaById(mediaID);
            // delete file
           System.IO.File.Delete(Server.MapPath(media.Path));
            // delete media
            BlogPostRepo.DeleteMedia(media);
            // BlogPostRepo.SubmitChanges();
            // redirect to media dashboard
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_DELETE_MEDIA;
            return RedirectToAction("media");
        }

        /// <summary>
        /// Action method that cancels de media deletion and redirects
        /// to the media management dashboard with an info message
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult CancelMediaDeletion()
        {
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_DELETE_MEDIA;
            return RedirectToAction("media");
        }

        #endregion

        #region comments action methods

        /// <summary>
        /// Action method that manages the deletion of a given post
        /// Renders a form that prompts the admin todelete a comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult DeleteComment(int id)
        {
            // grab comment
            Comment comment = BlogPostRepo.GetCommentById(id);
            // return form
            return View(comment);
        }

        /// <summary>
        /// Manages the post request from a comment deletion form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("DeleteComment")]
        [ValidateAntiForgeryToken(Salt = "DeleteComment")]
        [Transaction]
        public RedirectToRouteResult DeleteComment_Post(int commentID)
        {
            // grab comment
            Comment comment = BlogPostRepo.GetCommentById(commentID);
            // grab id of the blogpost it belong to, to prepare redirection
            BlogPost blogPost = BlogPostRepo.GetById((int)comment.BlogPostID);
            // delete comment
            BlogPostRepo.DeleteComment(comment);
            // BlogPostRepo.SubmitChanges();
            // redirect to blog post
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_DELETE_COMMENT;
            return RedirectToRoute(blogPost.GetBlogDetails());
        }

        /// <summary>
        /// Action method that cancels de deletion of a given comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RedirectToRouteResult CancelCommentDeletion(int id)
        {
            // Grag blogPost associated to comment
            BlogPost blogPost = BlogPostRepo.GetBlogPostFromCommentId(id);
            // redirect
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_DELETE_COMMENT;
            return RedirectToRoute(blogPost.GetBlogDetails());
        }

        #endregion

        #region pages action methods

        /// <summary>
        /// Action method that renders a form for the creation of a new page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult CreatePage()
        {
            // render form
            return View(new Page());
        }

        /// <summary>
        /// Action method that manages the post request of a form for the creation of a new page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "CreatePage")]
        [Transaction]
        public ActionResult CreatePage(Page page, string listOfTags)
        {
            // check validity
            if (!ModelState.IsValid)
                return View(page);
            // if it is valid, then we save the page and publish it
            BlogPostRepo.AddPage(page, listOfTags);
            // BlogPostRepo.SubmitChanges();
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_PAGE_CREATE;
            return RedirectToRoute(page.GetPageDetails());
        }

        /// <summary>
        /// Action method that manages the cancel of a page creation process
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult CancelPageCreation()
        {
            // message and redirection
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_CREATE_PAGE;
            return RedirectToAction("index");
        }

        /// <summary>
        /// Action method that renders a page edition form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult EditPage(int id)
        {
            // get page from context
            Page page = BlogPostRepo.GetPageById(id);
            // render form
            return View(page);
        }

        /// <summary>
        /// Action method that manages the post request of a page edition form
        /// </summary>
        /// <param name="pageEdited"></param>
        /// <param name="listOfTags"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "EditPage")]
        [Transaction]
        public ActionResult EditPage(Page pageEdited, string listOfTags)
        {
            // check validity
            if (!ModelState.IsValid)
                return View(pageEdited);
            // if it is valid, update changes
            Page page = BlogPostRepo.GetPageById(pageEdited.ContentID);
            TryUpdateModel(page);
            BlogPostRepo.UpdatePage(page, listOfTags);
            // BlogPostRepo.SubmitChanges();
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_EDIT_PAGE;
            return RedirectToRoute(page.GetPageDetails());
        }

        /// <summary>
        /// Action method that manages the cancellation of a page edition
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult CancelPageEdition()
        {
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_EDIT_PAGE;
            return RedirectToAction("index");
        }

        /// <summary>
        /// Action method that renders a page deletion form
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult DeletePage(int id)
        {
            // Get page from context
            Page page = BlogPostRepo.GetPageById(id);
            // render form
            return View(page);
        }

        /// <summary>
        /// Action method that manages the post request of a deletion form,
        /// and deletes the given page
        /// </summary>
        /// <param name="contentID"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("deletepage")]
        [ValidateAntiForgeryToken(Salt = "DeletePage")]
        [Transaction]
        public ActionResult DeletePage_Post(int contentID)
        {
            // Delete page by id
            BlogPostRepo.DeletePageById(contentID);
            // BlogPostRepo.SubmitChanges();
            // redirect to dashboard
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_DELETE_PAGE;
            return RedirectToAction("index");
        }

        /// <summary>
        /// Action method that manages the cancel of the
        /// deletion process of a page
        /// </summary>
        /// <returns></returns>
        public RedirectToRouteResult CancelPageDeletion()
        {
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_DELETE_PAGE;
            return RedirectToAction("index");
        }

        #endregion

        #region private helper methods for blog posts

        /// <summary>
        /// Method that manages the post of the CreateBlogPost action when the user clicks on the 'Publish' Button.
        /// It saves the blogpost and publishes it
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="categoryID"></param>
        /// <param name="listOfTags"></param>
        /// <returns></returns>
        private ActionResult CreateBlogPost_OnPublish(BlogPost blogPost, int[] categoryID, string listOfTags)
        {
            // Add BlogPost to Context
            BlogPostRepo.AddBlogPost(blogPost, categoryID, listOfTags);
            BlogPostRepo.PublishBlogPost(blogPost);
            // Submits changes
            // BlogPostRepo.SubmitChanges();
            // Redirects to Index with a message for the user
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_PUBLISH;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Method that manages the post of the CreateBlogPost action when the user clicks on the
        /// 'Preview' Button. It saves the blogpost as a draft and shows a preview to the user
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="categoryID"></param>
        /// <param name="listOfTags"></param>
        /// <returns></returns>
        private ActionResult CreateBlogPost_OnPreview(BlogPost blogPost, int[] categoryID, string listOfTags)
        {
            // Add BlogPost to Context
            BlogPostRepo.AddBlogPost(blogPost, categoryID, listOfTags);
            // Submits changes
            // BlogPostRepo.SubmitChanges();
            // Redirects to PreviewBlogPostAction
            return RedirectToAction("PreviewBlogPost", new { id=blogPost.ContentID});
        }

        /// <summary>
        /// Updates an edited blog post. Updates it changes to the persistence layer.
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="categoryId"></param>
        /// <param name="listOfTags"></param>
        /// <param name="submitButton"></param>
        /// <returns></returns>
        private ActionResult UpdateEditedBlogPost(BlogPost blogPost, int[] categoryId, string listOfTags, string submitButton)
        {
            // Update through model binding
            TryUpdateModel(blogPost);
            // Update tags and categories
            BlogPostRepo.UpdatePost(blogPost, categoryId, listOfTags);
            switch (submitButton)
            {
                case "Save":
                    TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_EDIT;
                    break;
                case "Publish":
                    if (blogPost.IsDraft)
                    {
                        BlogPostRepo.PublishBlogPost(blogPost);
                        TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_EDITPUBLISH;
                    }
                    else
                    {
                        TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = SUCCESS_EDIT_ALREADY_PUBLISHED;
                    }
                    break;
            }
            // Submit changes to context
            // BlogPostRepo.SubmitChanges();
            // Send back to index
            return RedirectToAction("previewblogpost", new{id = blogPost.ContentID});
        }

        #endregion

    }
}
