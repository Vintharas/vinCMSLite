using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using NUnit.Framework;
using Specs.Helpers;
using Moq;
using vinCMS.Controllers;
using PagedList;
using System.Web.Mvc;
using vinCMS.Models;

namespace Specs.UnitTests
{
    [TestFixture]
    public class BlogControllerUnitTests
    {

        /*  Requirements
         * 
         *  The user can list a series of blogposts in the main blog url (first paged list)
         *  The user can list a series of blogposts in other pages using the paging links
         *  The blogposts are ordered by descending publishing date
         *  The user can access to a single blog post when he clicks on it
         *  The wrong url when accessing a blog post redirects the user to the canonical url
         *  The user can filter the blogpost by tag by clicking on a certain tag
         *  The user can filter the blogpost by category by clicking on a certain category
         * 
         * 
         * 
         */


        [Test]
        public void Can_List_All_BlogPosts_First_Page()
        {
            // Arrange : Given a repository of blog posts and a BlogPost Controller
            var mockBlogPostRepository = UnitTestHelpers.MockBlogPostRepository(
                new BlogPost {ContentID = 1, Title = "Blog post 1", BodyContent = "Content 1"},
                new BlogPost {ContentID = 2, Title = "Blog Post 2", BodyContent = "Content 2"});
            var controller = new BlogController(mockBlogPostRepository);
            // Act: When the user goes to see the index
            var result = controller.Index();
            // Assert: the user can see all the blogposts that belong to the first page
            var model = (BlogPostViewModel) result.ViewData.Model;
            model.BlogPostPagedList.Count.ShouldEqual(2);
            model.BlogPostPagedList.PageNumber.ShouldEqual(0);
            model.BlogPostPagedList.PageNumber.ShouldEqual(1);
        }

        [Test]
        public void Can_List_All_BlogPosts_Other_Pages()
        {
            //Arrange: Given a repository of blog posts and a BlogPost Controller
            // and that the number of posts per page is set to 5 (Pang! Magic Number!)
            var mockBlogPostRepository = UnitTestHelpers.MockBlogPostRepository(
                new BlogPost {ContentID = 1}, new BlogPost {ContentID = 2}, new BlogPost {ContentID = 3},
                new BlogPost {ContentID = 4}, new BlogPost {ContentID = 5}, new BlogPost {ContentID = 6});
            var controller = new BlogController(mockBlogPostRepository);
            // Act: When the user goes to see the second page
            var result = controller.Index(1);
            // Assert: the user can see all the blog posts that belong to the second page
            var model = (BlogPostViewModel) result.ViewData.Model;
            model.BlogPostPagedList.Count.ShouldEqual(1);
            model.BlogPostPagedList.PageNumber.ShouldEqual(2);
        }

        [Test]
        public void Can_List_BlogPosts_Ordered_Descending_By_PublishedDate()
        {
            //Arrange: Given a repository of blog posts and a BlogPost controller
            var mockBlogPostRepository = UnitTestHelpers.MockBlogPostRepository(
                new BlogPost {ContentID = 1, PublishingDate = DateTime.Now}, 
                new BlogPost {ContentID = 2, PublishingDate = DateTime.Now.AddDays(1)});
            var controller = new BlogController(mockBlogPostRepository);
            // Act: When the user goes to see the list of blogs
            var result = controller.Index();
            // Assert: the user can see all the blog posts orderer by descending publishing date
            var model = (BlogPostViewModel) result.ViewData.Model;
            model.BlogPostPagedList.First().ContentID.ShouldEqual(2);
            model.BlogPostPagedList[1].ContentID.ShouldEqual(1);
        }

        [Test]
        public void Can_View_A_Selected_BlogPost()
        {
            //Arrange: Given a series of blog posts, a repository and a controller
            const int year = 2010;
            const int month = 10;
            const int day = 10;
            BlogPost selectedBlogPost = new BlogPost
                                            {
                                                ContentID = 1, 
                                                Title = "This is a post title",
                                                Path = "this-is-a-post-title",
                                                PublishingDate = new DateTime(year, month, day),
                                            };

            var moqBlogPostRepository = UnitTestHelpers.MockBlogPostRepositoryReturnsMoqObject(selectedBlogPost,
                                                                            new BlogPost() {ContentID = 2});
            moqBlogPostRepository.Setup(x => x.GetByPath(year,  month, day, selectedBlogPost.Path)).Returns(selectedBlogPost);
            var controller = new BlogController(moqBlogPostRepository.Object);
            //Act: if the user clicks on one of the blogpost
            var result = controller.Details(year, month, day,                           // Date
                                            selectedBlogPost.Path);                     // path
            var view = (ViewResult) result;
            //Assert: then the user will be able to see the content of the blogpost
            ((BlogPostDetailModel) view.ViewData.Model).BlogPost.ContentID.ShouldEqual(1);
        }

        [Test]
        public void Can_Filter_BlogPosts_By_Tag()
        {
            // Arrange: Given a series of blogposts with tags associated, a repository and a blog controller
            var tag = new Tag {TagID = 1, Name = "tag 1"};
            var tag2 = new Tag {TagID = 2, Name = "tag 2"};
            var blogPost = new BlogPost {ContentID = 1};
            var blogPost2 = new BlogPost() {ContentID = 2};
            var blogPost3 = new BlogPost() {ContentID = 3};
            blogPost.Tags.Add(tag);
            blogPost2.Tags.Add(tag2);
            blogPost3.Tags.Add(tag);
            blogPost3.Tags.Add(tag2);
            // mock blog post repository
            var moqBlogPostRepository = UnitTestHelpers.MockBlogPostRepositoryReturnsMoqObject(blogPost, blogPost2, blogPost3);
            moqBlogPostRepository.
                Setup(x => x.GetQueryableBlogPostByTagName(tag.Name)).             // when a user wants to filter by tag
                Returns((new List<BlogPost> {blogPost, blogPost3}).AsQueryable()); // They will obtain the filtered blogposts
            var controller = new BlogController(moqBlogPostRepository.Object);
            // Act: when the user clicks on a tag
            var result = controller.Tag("tag-1");
            // Assert: the result will be a view with a list of blogposts filtered by tag
            var model = ((BlogPostViewModel) result.ViewData.Model).BlogPostPagedList;
            model.Count.ShouldEqual(2);
            model.Single(x => x.ContentID == 1).ContentID.ShouldEqual(1);
            model.Single(x => x.ContentID == 3).ContentID.ShouldEqual(3);
            Assert.IsNull(model.FirstOrDefault(x => x.ContentID == 2));
        }

        [Test]
        public void Can_Filter_Blogposts_By_Category()
        {
            // Arrange: Given a series of blogs associated with a categories, a repository that manages
            // the access to those blogposts, and a blog controller
            var category = new Category {CategoryID = 1, Name = "category 1"};
            var category2 = new Category {CategoryID = 2, Name = "category 2"};
            var blogPost = new BlogPost { ContentID = 1 };
            var blogPost2 = new BlogPost() { ContentID = 2 };
            var blogPost3 = new BlogPost() { ContentID = 3 };
            blogPost.Categories.Add(category);
            blogPost2.Categories.Add(category2);
            blogPost3.Categories.Add(category);
            var moqBlogPostRepository = UnitTestHelpers.MockBlogPostRepositoryReturnsMoqObject(blogPost, blogPost2,
                                                                                               blogPost3);
            moqBlogPostRepository.
                Setup(x => x.GetQueryableBlogPostByCategory(category.Name)).        // when a user wants to filter by category
                Returns((new List<BlogPost> {blogPost, blogPost3}).AsQueryable());  // They will obtain the filtered blogposts
            var controller = new BlogController(moqBlogPostRepository.Object);
            // Act: when the user clicks on category
            var result = controller.Category("category-1");
            // Assert: the result will be a view with a list of blogposts filtered by category
            var model = ((BlogPostViewModel) result.ViewData.Model).BlogPostPagedList;
            model.Count.ShouldEqual(2);
            model.Single(x => x.ContentID == 1).ContentID.ShouldEqual(1);
            model.Single(x => x.ContentID == 3).ContentID.ShouldEqual(3);
            Assert.IsNull(model.FirstOrDefault(x => x.ContentID == 2));
        }

        [Test]
        public void Can_Get_A_Related_List_Of_BLogPosts()
        {
            // Arrange: Given a series of blogposts that are related by tags and categories, a repository
            // that manages those blogposts and a blog controller
            // categories
            var category = new Category() { CategoryID = 1, Name = "category 1" };
            var category2 = new Category() { CategoryID = 2, Name = "category 2" };
            // blogposts
            var blogPost = new BlogPost() { ContentID = 1 , Title = "blogPost 1", Path="blogpost-1", PublishingDate = DateTime.Now};
            var blogPost2 = new BlogPost() { ContentID = 2 };
            var blogPost3 = new BlogPost() { ContentID = 3 };
            var blogPost4 = new BlogPost()  { ContentID = 4 };
            // add categories to blogposts
            blogPost.Categories.Add(category);
            blogPost2.Categories.Add(category2);
            blogPost3.Categories.Add(category);
            // create mock repository
            var moqBlogPostRepository = UnitTestHelpers.MockBlogPostRepositoryReturnsMoqObject(blogPost, blogPost2,
                                                                                               blogPost3);
            const int numberOfRelatedPosts = 5;
            //moq get by id
            moqBlogPostRepository.Setup(x => x.GetByPath(blogPost.PublishingDate.Year,
                                            blogPost.PublishingDate.Month,
                                            blogPost.PublishingDate.Day, blogPost.Path)).Returns(blogPost);
            //moq getQueryableRelatedBlogPosts
            moqBlogPostRepository.
                Setup(x => x.GetListOfRelatedBlogPosts(blogPost, numberOfRelatedPosts)). // when a user wants to get related posts
                Returns((new List<BlogPost> { blogPost3 }));                  // They will obtain the related posts
            var controller = new BlogController(moqBlogPostRepository.Object);
            // Act: when the user goes into the detail screen
            var result = controller.Details(blogPost.PublishingDate.Year,
                                            blogPost.PublishingDate.Month,
                                            blogPost.PublishingDate.Day,
                                            blogPost.Path);
            // Assert: then he can see a series of related blogposts
            var viewResult = ((ViewResult) result);
            var model = ((BlogPostDetailModel) viewResult.ViewData.Model);
            model.HasRelatedBlogPosts.ShouldEqual(true);
            model.RelatedBlogPosts.Count.ShouldEqual(1);
        }

    }

}
