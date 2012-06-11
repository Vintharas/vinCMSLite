using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainStorage.Abstracts;

namespace DomainRepos.Abstracts
{
    public interface IEntityRepository<TEntity>
    {
        TEntity GetById(int id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        IList<TEntity> All();
        IQueryable<TEntity> GetQueryableEntitySet();
        IContext GetUnitOfWork { get; }
    }
}
