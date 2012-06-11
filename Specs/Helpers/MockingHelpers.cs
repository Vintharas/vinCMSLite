using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using DomainRepos.Abstracts;
using Moq;
using Domain.Entities;
using DomainStorage.Abstracts;

namespace Specs.Helpers
{
    public static partial class UnitTestHelpers
    {

        /// <summary>
        /// Generates an mockup for IEntityRepository at runtime using Moq
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static IEntityRepository<T> MockEntityRepository<T>(params T[] entities)
        {
            var mockEntityRepository = new Mock<IEntityRepository<T>>();
            mockEntityRepository.Setup(x => x.All()).Returns(entities.ToList());
            mockEntityRepository.Setup(x => x.GetQueryableEntitySet()).Returns(entities.AsQueryable());

            return mockEntityRepository.Object;
        }

        /// <summary>
        /// Generates a mockup for IBlogPostRepository at runtime using Moq
        /// </summary>
        /// <param name="blogPosts"></param>
        /// <returns></returns>
        public static IBlogPostRepository MockBlogPostRepository(params BlogPost[] blogPosts)
        {
            var moqBlogPostRepository = MockBlogPostRepositoryReturnsMoqObject(blogPosts);
            return moqBlogPostRepository.Object;
        }

        /// <summary>
        /// Generates a Moq object for IBlogPostRepository. Sets some default behavior and returns the moq object
        /// for further customization
        /// </summary>
        /// <param name="blogPosts"></param>
        /// <returns></returns>
        public static Mock<IBlogPostRepository> MockBlogPostRepositoryReturnsMoqObject(params BlogPost[] blogPosts)
        {
            var moqBlogPostRepository = new Mock<IBlogPostRepository>();
            moqBlogPostRepository.Setup(x => x.All()).Returns(blogPosts.ToList());
            moqBlogPostRepository.Setup(x => x.GetQueryableEntitySet()).Returns(blogPosts.AsQueryable());
            moqBlogPostRepository.Setup(x => x.GetOrderedQueryableBlogPost()).Returns(
                blogPosts.AsQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.PublishingDate));
            moqBlogPostRepository.Setup(x => x.GetOrderedQueryableBlogPostWithIncludes()).Returns(
                blogPosts.AsQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.PublishingDate));
            moqBlogPostRepository.Setup(x => x.SubmitChanges());
            return moqBlogPostRepository;
        }

        /// <summary>
        /// Helper method that sets up a IBlogPostRepository that will work only with categories.
        /// It returns a Mock object for further configuration
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static Mock<IBlogPostRepository> MockBlogPostRepositoryReturnsMoqObject(params Category[] categories)
        {
            var moqBlogPostRepository = new Mock<IBlogPostRepository>();
            moqBlogPostRepository.Setup(x => x.GetListOfCategories()).Returns(categories.ToList());
            moqBlogPostRepository.Setup(x => x.GetQueryableCategories()).Returns(categories.AsQueryable());
            moqBlogPostRepository.Setup(x => x.GetQueryableOrderedCategories()).Returns(
                categories.AsQueryable().OrderBy(x => x.Name));
            return moqBlogPostRepository;
        }

        /// <summary>
        /// Helper method that sets up a IBlogPostRepository that will work only with categories. Returns a BlogPostRepository
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static IBlogPostRepository MockBlogPostRepository(params Category[] categories)
        {
            return MockBlogPostRepositoryReturnsMoqObject(categories).Object;
        }
    
        /// <summary>
        /// Helper method that sets up a mockup of a IUserRepository that will access a series of User objects passed as arguments.
        /// The method returns a Mock object for further customization
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static Mock<IUserRepository> MockUserRepositoryReturnsMoqObject(params User[] users)
        {
            var moqUserRepo = new Mock<IUserRepository>();
            moqUserRepo.Setup(x => x.All()).Returns(users.ToList());
            moqUserRepo.Setup(x => x.GetQueryableEntitySet()).Returns(users.AsQueryable());
            return moqUserRepo;
        }

        /// <summary>
        /// Helper method that sets up a mockup of a IUserRepository that will access to a series of User objects passed as argument
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static IUserRepository MockUserRepository(params User[] users)
        {
            return MockUserRepositoryReturnsMoqObject(users).Object;
        }

    }

       
}
