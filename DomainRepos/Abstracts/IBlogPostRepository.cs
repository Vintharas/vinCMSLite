using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace DomainRepos.Abstracts
{
    public interface IBlogPostRepository : IEntityRepository<BlogPost>
    {
        IQueryable<BlogPost> GetOrderedQueryableBlogPost();
        IQueryable<BlogPost> GetOrderedQueryableBlogPostWithIncludes();
        IQueryable<BlogPost> GetQueryableBlogPostByTagName(string tagName);
        IQueryable<BlogPost> GetQueryableBlogPostByCategory(string categoryName);
        IList<BlogPost> GetListOfRelatedBlogPosts(BlogPost blogPost, int numberOfRelatedPosts);
        BlogPost GetByPath(int year, int month, int day, string path);

        BlogPost CreateNewBlogPost(string authorName);
        void AddBlogPost(BlogPost blogPost, int[] categories, string listOfTags);
        void PublishBlogPost(BlogPost blogPost);
        void UpdatePost(BlogPost blogPost, int[] categoryId, string listOfTags);
        void SubmitChanges();
        void AddCommentToBlogPost(BlogPost blogPost, Comment comment);

        // Categories
        IList<Category> GetListOfCategories();
        IQueryable<Category> GetQueryableOrderedCategories();
        IQueryable<Category> GetQueryableCategories();
        Category GetCategoryById(int categoryId);
        void AddCategory(Category category);
        void DeleteCategoryById(int categoryId);
        void DeleteCategory(Category category);
        
        // Users
        User GetUserByName(string userName);

        // Tags
        IQueryable<Tag> GetQueryableOrderedTags();
        IQueryable<Tag> GetQueryableTags();
        void AddTag(Tag tag);
        Tag GetTagById(int id);
        void DeleteTagById(int tagId);

        //Comments
        Comment GetCommentById(int id);
        void DeleteComment(Comment comment);
        void DeleteCommentById(int id);
        BlogPost GetBlogPostFromCommentId(int id);

        //Media files
        IQueryable<Media> GetQueryableOrderedMedia();
        IQueryable<Media> GetQueryableMedia();
        void AddNewMedia(int contentLength, string contentType, string fileName, string fullPath);
        Media GetMediaById(int id);
        void DeleteMedia(Media media);

        // Pages
        List<Page> GetOrderedListOfPages();
        IQueryable<Page> GetQueryablePages();
        IQueryable<Page> GetQueryableOrderedPages();
        void AddPage(Page page, string listOfTags);
        Page GetPageById(int id);
        void UpdatePage(Page page, string listOfTags);
        void DeletePageById (int contentId);
        Page GetHomePage();
        IEnumerable<Page> GetListOfNavigablePages ();

        // Rss
        System.Xml.Linq.XDocument GetRssForBlogPosts(string encoding, string link);
    }
}
