using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Domain.ErrorHandling;
using DomainRepos.Abstracts;
using DomainStorage.Abstracts;

namespace DomainRepos.Concretes
{
    public class CategoryRepository : IEntityRepository<Category>
    {

        private readonly IContext _context;

        /// <summary>
        /// Class constructor. Initializes the datalayer context
        /// </summary>
        public CategoryRepository(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetById(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.CategoryID == id);
            if (category == null)
                throw new EntityNotFoundException();
            return category;
        }

        /// <summary>
        /// Adds a category to the context
        /// </summary>
        /// <param name="entity"></param>
        public void Add(Category entity)
        {
            _context.Categories.AddObject(entity);
        }

        /// <summary>
        /// Deletes a category that receives as an argument
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Category entity)
        {
            _context.Categories.DeleteObject(entity);
        }

        /// <summary>
        /// Gets all the categories from the data context
        /// </summary>
        /// <returns></returns>
        public IList<Category> All()
        {
            return _context.Categories.ToList();
        }

        /// <summary>
        /// Gets all categories as an IQueryable()
        /// </summary>
        /// <returns></returns>
        public IQueryable<Category> GetQueryableEntitySet()
        {
            return _context.Categories.AsQueryable();
        }

        /// <summary>
        /// Returns the context on which the repository is working
        /// </summary>
        public IContext GetUnitOfWork
        {
            get { return _context; }
        }
    }
}
