using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.ErrorHandling;
using DomainRepos.Abstracts;
using Domain.Entities;
using DomainStorage.Abstracts;

namespace DomainRepos.Concretes
{
    public class TagRepository : IEntityRepository<Tag>
    {
        private readonly IContext _context;

        /// <summary>
        /// class Constructor. Sets the context the repository is going to work with
        /// </summary>
        /// <param name="context"></param>
        public TagRepository(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a tag by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tag GetById(int id)
        {
            Tag tag = _context.Tags.Where(x => x.TagID == id).FirstOrDefault();
            if (tag == null)
                throw new EntityNotFoundException();
            return tag;
        }

        /// <summary>
        /// Adds a tag to the context
        /// </summary>
        /// <param name="entity"></param>
        public void Add(Tag entity)
        {
            _context.Tags.AddObject(entity);
        }

        /// <summary>
        /// Deletes a tag from the context
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(Tag entity)
        {
            _context.Tags.DeleteObject(entity);
        }

        /// <summary>
        /// Returns all tags from the context
        /// </summary>
        /// <returns></returns>
        public IList<Tag> All()
        {
            return _context.Tags.ToList();
        }

        /// <summary>
        /// Returns an IQueryable of a tag object
        /// </summary>
        /// <returns></returns>
        public IQueryable<Tag> GetQueryableEntitySet()
        {
            return _context.Tags.AsQueryable();
        }

        /// <summary>
        /// Gets the Context on which the repository is working
        /// </summary>
        public IContext GetUnitOfWork
        {
            get { throw new NotImplementedException(); }
        }
    }
}
