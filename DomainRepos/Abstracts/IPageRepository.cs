using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace DomainRepos.Abstracts
{
    public interface IPageRepository : IEntityRepository<Page>
    {
        Page GetPageByPath(string path);
        Page GetHomePage();
    }
}
