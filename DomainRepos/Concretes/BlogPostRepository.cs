using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Domain.Entities;
using Domain.ErrorHandling;
using DomainRepos.Abstracts;
using DomainStorage.Abstracts;
using DomainStorage.EFStorage;
using System;
using System.IO;

namespace DomainRepos.Concretes
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly IContext _context;

        /// <summary>
        /// Class constructor. Initializes the context the repository is going to work on
        /// </summary>
        /// <param name="context"></param>
        public BlogPostRepository(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a blogPost object by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogPost GetById(int id)
        {
            BlogPost blogPost = _context.BlogPosts.AddIncludes().FirstOrDefault(b => b.ContentID == id);
            if (blogPost == null)
                throw new EntityNotFoundException();
            return blogPost;
        }

        /// <summary>
        /// Adds a new blogPost to the context
        /// </summary>
        /// <param name="entity"></param>
        public void Add(BlogPost entity)
        {
            _context.ContentContainers.AddObject(entity);
        }

        /// <summary>
        /// Deletes a blogPost from the context
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(BlogPost entity)
        {
            _context.ContentContainers.DeleteObject(entity);
        }

        /// <summary>
        /// Method that obtains all the blogposts from the database
        /// </summary>
        /// <returns></returns>
        public IList<BlogPost> All()
        {
            return _context.BlogPosts.ToList();
        }

        /// <summary>
        /// Method that obtains a IQueryable of BlogPost objects
        /// </summary>
        /// <returns></returns>
        public IQueryable<BlogPost> GetQueryableEntitySet()
        {
            return _context.BlogPosts;
        }

        /// <summary>
        /// Method that obtains an ordered IQueryable of blog posts. It will be ordered by descending publishing dates
        /// The method only gets those posts that are not a Draft, i.e. isDraft == false
        /// </summary>
        /// <returns></returns>
        public IQueryable<BlogPost> GetOrderedQueryableBlogPost()
        {
            return GetQueryableEntitySet().Where(x => x.IsDraft == false).OrderByDescending(x => x.PublishingDate);
        }

        /// <summary>
        /// Method that obtains an ordered IQueryable of blog posts with includes. It will be ordered by descending publishing dates.
        /// It will have the associated author, tags and categories.
        /// </summary>
        /// <returns></returns>
        public IQueryable<BlogPost> GetOrderedQueryableBlogPostWithIncludes()
        {
            return GetOrderedQueryableBlogPost().AddIncludes();
        }

        /// <summary>
        /// Gets an IQueryable of Blogposts filtered by Tag by name "tagName". They are
        /// ordered by descending publishing date and include Author, tags and categories
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public IQueryable<BlogPost> GetQueryableBlogPostByTagName(string tagName)
        {
            var query = GetQueryableEntitySet();
            var newQuery = from blogPost in query
                           from tag in blogPost.Tags
                           where tag.Name == tagName
                           orderby blogPost.PublishingDate descending 
                           select blogPost;
            return newQuery.AddIncludes();
        }

        /// <summary>
        /// Gets an IQueryable of Blogposts filtered by category by name "categoryName". They
        /// are ordered by descending publishing date and include Author, tags and categories
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public IQueryable<BlogPost> GetQueryableBlogPostByCategory(string categoryName)
        {
            var query = GetQueryableEntitySet();
            var newQuery = from blogPost in query
                           from category in blogPost.Categories
                           where category.Name == categoryName
                           orderby blogPost.PublishingDate descending 
                           select blogPost;
            return newQuery.AddIncludes();
        }

        /// <summary>
        /// Gets a IList of Blogposts related to a given blogPost that is passed to the method
        /// Related in terms of categories
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="numberOfRelatedPosts"></param>
        /// <returns></returns>
        public IList<BlogPost> GetListOfRelatedBlogPosts(BlogPost blogPost, int numberOfRelatedPosts)
        {
            // simplified solution, check first category
            Category category = blogPost.Categories.FirstOrDefault();
            if (category == null)
            {
                return null;
            }
            else
            {
                var query = from post in GetQueryableEntitySet()
                            from postCategory in post.Categories
                            where postCategory.CategoryID == category.CategoryID
                            orderby post.PublishingDate descending
                            select post;
                return query.Take(numberOfRelatedPosts).ToList();
            }
        }

        /// <summary>
        /// Creates a new blogPost with default values and returns it
        /// </summary>
        /// <returns></returns>
        public BlogPost CreateNewBlogPost(string authorName)
        {
            // get author from database
            User author = GetUserByName(authorName);
            // return new blogpost
            return new BlogPost()
                       {
                           CreationDate = DateTime.Now,
                           PublishingDate = DateTime.Now,
                           User = author,
                           IsDraft = true
                       };
        }

        /// <summary>
        /// returns the context on which the repository is working
        /// </summary>
        public IContext GetUnitOfWork
        {
            get { return _context; }
        }

        /// <summary>
        ///  Adds a blog post to the context with its categories
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="categories"></param>
        /// <param name="listOfTags"></param>
        public void AddBlogPost(BlogPost blogPost, int[] categories, string listOfTags)
        {
            // add blogPost to Context
            blogPost.IsDraft = true;
            Add(blogPost);
            // add categories
            AddCategoriesToContentContainer(blogPost, categories);
            // add tags
            IEnumerable<string> formattedListOfTags = ExtractTagsFromString(listOfTags);
            AddListOfTagsToContentContainer(blogPost, formattedListOfTags);
        }

        /// <summary>
        /// Method that publishes a specific post that receives as an argument
        /// </summary>
        /// <param name="blogPost"></param>
        public void PublishBlogPost(BlogPost blogPost)
        {
            blogPost.IsDraft = false;
            blogPost.PublishingDate = DateTime.Now;
        }

        /// <summary>
        /// Method that updates a blog that has been edited. Deleting the categories and tags with which
        /// it is not longer associated and adding new categories and tags if there are any
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="categoryId"></param>
        /// <param name="listOfTags"></param>
        public void UpdatePost(BlogPost blogPost, int[] categoryId, string listOfTags)
        {
            UpdateCategoriesInContentContainer(blogPost, categoryId);
            UpdateTagsInContentContainer(blogPost, listOfTags);
        }

        /// <summary>
        /// Method that delegates the submitchanges on the unit of work context
        /// </summary>
        public void SubmitChanges()
        {
            GetUnitOfWork.SubmitChanges();
        }

        /// <summary>
        /// Returns the blogpost that matches the path that is passed and an argument
        /// to the function
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public BlogPost GetByPath(int year, int month, int day, string path)
        {
            BlogPost blogPost = GetQueryableEntitySet().Where(
                x => x.PublishingDate.Year == year && x.PublishingDate.Month == month && x.PublishingDate.Day == day
                     && x.Path == path).AddIncludes().FirstOrDefault();
            if (blogPost == null)
                throw new EntityNotFoundException();
            return blogPost;
        }

        #region Category Methods

        /// <summary>
        /// Gets and IQueryable of alphabetically ordered categories
        /// </summary>
        /// <returns></returns>
        public IQueryable<Category> GetQueryableOrderedCategories()
        {
            return GetQueryableCategories().OrderBy(x => x.Name);
        }

        /// <summary>
        /// Gets an IQueryable of categories
        /// </summary>
        /// <returns></returns>
        public IQueryable<Category> GetQueryableCategories()
        {
            return GetUnitOfWork.Categories.AsQueryable();
        }

        /// <summary>
        /// Gets a category by id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public Category GetCategoryById(int categoryId)
        {
            Category category = GetQueryableCategories().Where(x => x.CategoryID == categoryId).FirstOrDefault();
            if (category == null)
                throw new EntityNotFoundException();
            return category;
        }

        /// <summary>
        /// Adds a category to the context
        /// </summary>
        /// <param name="category"></param>
        public void AddCategory(Category category)
        {
            // format category name
            category.Name = category.Name.Trim();
            // add category to context
            GetUnitOfWork.Categories.AddObject(category);
        }

        /// <summary>
        /// Deletes a category whose id is passed as an argument to the method
        /// </summary>
        /// <param name="categoryId"></param>
        public void DeleteCategoryById(int categoryId)
        {
            Category category = GetCategoryById(categoryId);
            DeleteCategory(category);
        }
        
        /// <summary>
        /// Deletes a category that is passed as an argument to the method
        /// </summary>
        /// <param name="category"></param>
        public void DeleteCategory(Category category)
        {
            GetUnitOfWork.Categories.DeleteObject(category);
        }

        /// <summary>
        /// Returns an IList of categories
        /// </summary>
        /// <returns></returns>
        public IList<Category> GetListOfCategories()
        {
            return GetQueryableCategories().ToList();
        }

        #endregion

        #region User Methods

        /// <summary>
        /// Gets a user by name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserByName(string userName)
        {
            User user = GetUnitOfWork.Users.Where(x => x.UserName == userName).FirstOrDefault();
            if (user == null)
                throw new EntityNotFoundException();
            return user;
        }

        #endregion

        #region Tag Methods

        /// <summary>
        /// Returns an IQueryable of tags ordered alphabetically by name
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tag> GetQueryableOrderedTags()
        {
            return GetQueryableTags().OrderBy(x => x.Name);
        }

        /// <summary>
        /// Returns an IQueryable of tags
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tag> GetQueryableTags()
        {
            return GetUnitOfWork.Tags.AsQueryable();
        }

        /// <summary>
        /// Adds a tag to the context
        /// </summary>
        /// <param name="tag"></param>
        public void AddTag(Tag tag)
        {
            GetUnitOfWork.Tags.AddObject(tag);
        }

        /// <summary>
        /// Gets a tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tag GetTagById(int id)
        {
            Tag tag = GetQueryableTags().Where(x => x.TagID == id).FirstOrDefault();
            if (tag == null)
                throw new EntityNotFoundException();
            return tag;
        }

        /// <summary>
        /// Deletes a tag whose id is passed as an argument
        /// </summary>
        /// <param name="tagId"></param>
        public void DeleteTagById(int tagId)
        {
            // grab tag from context and delete it
            GetUnitOfWork.Tags.DeleteObject(GetTagById(tagId));
        }

        #endregion

        #region Comment Methods

        /// <summary>
        /// Adds a comment to a blog posts
        /// </summary>
        /// <param name="blogPost"></param>
        /// <param name="comment"></param>
        public void AddCommentToBlogPost(BlogPost blogPost, Comment comment)
        {
            comment.CommentDate = DateTime.Now;
            comment.CommentContent = FormatCommentComment(comment.CommentContent);
            blogPost.Comments.Add(comment);
        }

        /// <summary>
        /// Gets a comment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Comment GetCommentById(int id)
        {
            Comment comment = GetUnitOfWork.Comments.Where(x => x.CommentID == id).FirstOrDefault();
            if (comment == null)
                throw new EntityNotFoundException();
            return comment;
        }

        /// <summary>
        /// Deletes a comment
        /// </summary>
        /// <param name="comment"></param>
        public void DeleteComment(Comment comment)
        {
            GetUnitOfWork.Comments.DeleteObject(comment);
        }

        /// <summary>
        /// Deletes a comment by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCommentById(int id)
        {
            GetUnitOfWork.Comments.DeleteObject(GetCommentById(id));
        }

        /// <summary>
        /// Gets the blogpost that is related with a given comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogPost GetBlogPostFromCommentId(int id)
        {
            Comment comment = GetUnitOfWork.Comments.Include("BlogPost").Where(x => x.CommentID == id).FirstOrDefault();
            if (comment == null)
                throw new EntityNotFoundException();
            return comment.BlogPost;
        }

        /// <summary>
        /// Formats a comment content changing line breaks for html paragraphs
        /// </summary>
        /// <param name="commentContent"></param>
        /// <returns></returns>
        private string FormatCommentComment(string commentContent)
        {
            string pOpen = "<p>";
            string pClose = "</p>";
            string formattedContent = pOpen + commentContent.Trim() + pClose;
            return formattedContent.Replace("\n", pClose + pOpen); 
        }

        #endregion

        #region media methods

        /// <summary>
        /// Gets a 
        /// </summary>
        public IQueryable<Media> GetQueryableOrderedMedia()
        {
            return GetQueryableMedia().Include("MimeType").OrderBy(x => x.FileName);
        }

        /// <summary>
        /// Gets a queryable of Media
        /// </summary>
        public IQueryable<Media> GetQueryableMedia()
        {
            return GetUnitOfWork.Medias.AsQueryable();
        }

        /// <summary>
        /// Adds a new media file record to the context
        /// </summary>
        /// <param name="contentLength"></param>
        /// <param name="contentType"></param>
        /// <param name="fileName"></param>
        public void AddNewMedia(int contentLength, string contentType, string fileName, string fullPath)
        {
            // get mime type from database
            MimeType mime = GetUnitOfWork.MimeTypes.Where(x => x.Name == contentType).Single();
            // create media record
            Media media = new Media
                              {
                                  FileName = fileName,
                                  Extension = Path.GetExtension(fileName),
                                  MimeType = mime,
                                  Path = fullPath,
                                  UploadDate = DateTime.Now
                             };
            // add to context
            GetUnitOfWork.Medias.AddObject(media);
        }

        /// <summary>
        /// Gets a media object from the context by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Media GetMediaById(int id)
        {
            Media media = GetQueryableMedia().Where(x => x.MediaID == id).FirstOrDefault();
            if (media == null)
                throw new EntityNotFoundException();
            return media;
        }

        /// <summary>
        /// Deletes a media object from the context
        /// </summary>
        /// <param name="media"></param>
        public void DeleteMedia(Media media)
        {
            GetUnitOfWork.Medias.DeleteObject(media);
        }

        #endregion

        #region Page methods

        /// <summary>
        /// Gets a list of alphabetically ordered pages
        /// </summary>
        /// <returns></returns>
        public List<Page> GetOrderedListOfPages()
        {
            return GetQueryableOrderedPages().ToList();
        }

        /// <summary>
        /// Gets a IQueryable of pages
        /// </summary>
        /// <returns></returns>
        public IQueryable<Page> GetQueryablePages()
        {
            return GetUnitOfWork.Pages;
        }

        /// <summary>
        /// Gets an IQueryable of pages alphabetically ordered
        /// </summary>
        /// <returns></returns>
        public IQueryable<Page> GetQueryableOrderedPages()
        {
            return GetQueryablePages().OrderBy(x => x.Title);
        }

        /// <summary>
        /// Adds a new page to the context
        /// </summary>
        /// <param name="page"></param>
        /// <param name="listOfTags"></param>
        public void AddPage(Page page, string listOfTags)
        {
            // add blogPost to Context
            GetUnitOfWork.ContentContainers.AddObject(page);
            // add tags
            IEnumerable<string> formattedListOfTags = ExtractTagsFromString(listOfTags);
            AddListOfTagsToContentContainer(page, formattedListOfTags);
        }

        /// <summary>
        /// Gets a page by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Page GetPageById(int id)
        {
            Page page = GetQueryablePages().Include("Tags").Where(x => x.ContentID == id).FirstOrDefault();
            if (page == null)
                throw new EntityNotFoundException();
            return page;
        }

        /// <summary>
        /// Updates a page that has been modified. Deleting tag associations
        /// and adding new ones
        /// </summary>
        /// <param name="page"></param>
        /// <param name="listOfTags"></param>
        public void UpdatePage(Page page, string listOfTags)
        {
            UpdateTagsInContentContainer(page, listOfTags);
        }

        /// <summary>
        /// Deletes the page whose id is contentId
        /// </summary>
        /// <param name="contentId"></param>
        public void DeletePageById(int contentId)
        {
            GetUnitOfWork.ContentContainers.DeleteObject(GetPageById(contentId));
        }

        /// <summary>
        /// Returns the home page if there is one, returns null otherwise
        /// </summary>
        /// <returns></returns>
        public Page GetHomePage()
        {
            return GetQueryablePages().Where(x => x.IsHomePage == true).FirstOrDefault();
        }

        /// <summary>
        /// returns a list of navigable pages
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Page> GetListOfNavigablePages()
        {
            return GetQueryablePages().Where(x => x.IsNavigablePage == true).ToList();
        }

        #endregion

        #region rss methods

        /// <summary>
        /// Gets a rss document for blog posts
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public XDocument GetRssForBlogPosts(string encoding, string link)
        {
            // Get last blog posts
            List<BlogPost> listOfBlogPosts = GetOrderedQueryableBlogPost().Where(x => x.IsDraft == false).Take(10).ToList();
            // Create rss document
            XDocument rss = new XDocument(new XDeclaration("1.0", encoding, "yes"),
                                          new XElement("rss", new XAttribute("version", "2.0"),
                                                       new XElement("channel",
                                                                    new XElement("title", "Barbarian Meets Coding RSS 2.0 feed"),
                                                                    from post in listOfBlogPosts
                                                                    select new XElement("item",
                                                                                        new XElement("title", post.Title),
                                                                                        new XElement("description", post.Summary + " " + post.BodyContent),
                                                                                        new XElement("link", link)
                                                                        )
                                                           )
                                              ));
            return rss;
        }

       

        #endregion

        #region private helper methods

        /// <summary>
        /// Update the tags associated with a content container that has been edited
        /// </summary>
        /// <param name="contentContainer"></param>
        /// <param name="listOfTags"></param>
        private void UpdateTagsInContentContainer(ContentContainer contentContainer, string listOfTags)
        {
            IEnumerable<string> formattedListOfTags = ExtractTagsFromString(listOfTags);
            // gets a IEnumerable of string tags from the comma separated list of tags
            DeleteTagsFromContentContainer(contentContainer, formattedListOfTags);
            // add tags
            AddListOfTagsToContentContainer(contentContainer, formattedListOfTags);
        }

        /// <summary>
        /// Deletes the association between a content container and those tags that are no longer selected
        /// </summary>
        /// <param name="contentContainer"></param>
        /// <param name="stringTags"></param>
        private void DeleteTagsFromContentContainer(ContentContainer contentContainer, IEnumerable<string> stringTags)
        {
            var query = from tag in contentContainer.Tags
                        from tagString in stringTags
                        where tag.Name == tagString
                        select tag;
            var tagsPresent = query.Distinct().ToList();
            // Delete the association with tags that are no longer needed
            // select tags to delete
            List<Tag> tagsToDelete = contentContainer.Tags.Where(tag => !tagsPresent.Contains(tag)).ToList();
            // delete them
            foreach(var tag in tagsToDelete)
                contentContainer.Tags.Remove(tag);
        }

        /// <summary>
        /// Method that adds the tags represented by a string list of tags to a certain
        /// contentContainer
        /// </summary>
        /// <param name="contentContainer"></param>
        /// <param name="listOfTags"></param>
        private void AddListOfTagsToContentContainer(ContentContainer contentContainer, IEnumerable<string> listOfTags)
        {
            IEnumerable<Tag> tags = ManageTagsFromListOfTags(listOfTags);
            foreach (Tag tag in tags)
            {
                if (!contentContainer.Tags.Contains(tag))
                    contentContainer.Tags.Add(tag);        
            }
        }

        /// <summary>
        /// Manages the tags from the list of tags, Extracts the tags from the comma separated list of tags
        /// and obtains the equivalent Tag object, either from the database, or it creates a new one
        /// </summary>
        /// <param name="listOfTags"></param>
        /// <returns></returns>
        private IEnumerable<Tag> ManageTagsFromListOfTags(IEnumerable<string> listOfTags)
        {
            List<Tag> tags = new List<Tag>();
            foreach (string tagString in listOfTags)
            {
                // looks in the database to see if the tag already exists
                Tag tag = _context.Tags.FirstOrDefault(x => x.Name == tagString);
                // if it doesn't it creates a new tag
                if (tag == null)
                {
                    tag = new Tag { Name = tagString };
                    _context.Tags.AddObject(tag);
                }
                tags.Add(tag);
            }
            return tags;
        }

        /// <summary>
        /// Method that extracts the tags from a string that contains comma-separated tags
        /// Returns the tags trimmed and in lower case as a IEnumerable of strings
        /// </summary>
        /// <param name="listOfTags"></param>
        /// <returns></returns>
        private IEnumerable<string> ExtractTagsFromString(string listOfTags)
        {
            string[] tagArray = listOfTags.Split(',');
            List<string> listOfFormattedTags = new List<string>();
            foreach (string tagString in tagArray)
            {
                string formattedTag = tagString.Trim().ToLower();
                if (formattedTag != string.Empty)
                    listOfFormattedTags.Add(formattedTag);
            }
            return listOfFormattedTags;
        }

        /// <summary>
        /// Update the categories associated with a content container that has been edited
        /// </summary>
        /// <param name="contentContainer"></param>
        /// <param name="categoryId"></param>
        private void UpdateCategoriesInContentContainer(ContentContainer contentContainer, int[] categoryId)
        {
            // Delete association with categories no longer selected
            DeleteCategoriesFromContentContainer(contentContainer, categoryId);
            // Add association with categories that have been selected
            AddCategoriesToContentContainer(contentContainer, categoryId);
        }

        /// <summary>
        /// Deletes the associations with categories that are no longer selected
        /// </summary>
        /// <param name="contentContainer"></param>
        /// <param name="categoryId"></param>
        private void DeleteCategoriesFromContentContainer(ContentContainer contentContainer, int[] categoryId)
        {
            if (categoryId != null)
            {
                // This should be improved****
                var query = from category in contentContainer.Categories
                            from id in categoryId
                            where category.CategoryID == id
                            select category;
                List<Category> categoriesPresent = query.Distinct().ToList();
                // Delete those not present
                // Select categories to delete
                List<Category> categoriesToDelete = contentContainer.Categories.Where(category => !categoriesPresent.Contains(category)).ToList();
                // Delete them
                foreach (var category in categoriesToDelete)
                    contentContainer.Categories.Remove(category); 
            }
            else
            {
                while (contentContainer.Categories.Count > 0)
                    contentContainer.Categories.RemoveAt(0);
            }
        }

        /// <summary>
        /// Adds the categories that have been selected to a given content container
        /// </summary>
        /// <param name="contentContainer"></param>
        /// <param name="categories"></param>
        private void AddCategoriesToContentContainer(ContentContainer contentContainer, int[] categories)
        {
            if (categories != null)
            {
                foreach (var categoryID in categories)
                {
                    // get category from database
                    Category category = _context.Categories.Single(x => x.CategoryID == categoryID);
                    // add it to blogPost
                    if (!contentContainer.Categories.Contains(category))
                        contentContainer.Categories.Add(category);
                }    
            }
        }

        #endregion

    }
    
    internal static class BlogPostRepoExtensions
    {
        public static IQueryable<BlogPost> AddIncludes(this IQueryable<BlogPost> blogPosts)
        {
            return blogPosts.Include("User").Include("Tags").Include("Categories").Include("Comments");
        }
    }

}