using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.ErrorHandling;
using DomainRepos.Abstracts;
using DomainStorage.Abstracts;
using DomainStorage.EFStorage;

namespace DomainRepos.Concretes
{
    /// <summary>
    /// Repository that encapsulate access to the context for Page class objects
    /// </summary>
    public class PageRepository : IPageRepository
    {

        private readonly IContext _context;
        public IContext GetUnitOfWork { get { return _context; } }

        /// <summary>
        /// Class constructor that initializes the context the repository is going to work on
        /// </summary>
        /// <param name="context"></param>
        public PageRepository(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets page by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Page GetById(int id)
        {
            Page page = GetQueryableEntitySet().Where(x => x.ContentID == id).FirstOrDefault();
            if (page == null)
                throw new EntityNotFoundException();
            return page;
        }

        /// <summary>
        /// Adds a page to the context
        /// </summary>
        /// <param name="page"></param>
        public void Add(Page page)
        {
            GetUnitOfWork.ContentContainers.AddObject(page);
        }

        /// <summary>
        /// Delete a page from the context
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Page page)
        {
            GetUnitOfWork.ContentContainers.DeleteObject(page);
        }

        /// <summary>
        /// Returns a list of All pages from the context
        /// </summary>
        /// <returns></returns>
        public IList<Page> All()
        {
            return GetQueryableEntitySet().ToList();
        }

        /// <summary>
        /// Gets a IQueryable of Page object
        /// </summary>
        /// <returns></returns>
        public IQueryable<Page> GetQueryableEntitySet()
        {
            return GetUnitOfWork.Pages.AsQueryable();
        }

        /// <summary>
        /// Gets a page by path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Page GetPageByPath(string path)
        {
            Page page = GetQueryableEntitySet().Include("Tags").Where(x => x.Path == path).FirstOrDefault();
            if (page == null)
                throw new EntityNotFoundException();
            return page;
        }

        /// <summary>
        /// Gets the home page
        /// </summary>
        /// <returns></returns>
        public Page GetHomePage()
        {
            return GetQueryableEntitySet().Include("Tags").Where(x => x.IsHomePage == true).FirstOrDefault();
        }

    }
}
