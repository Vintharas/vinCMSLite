using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainRepos.Abstracts
{
    public class RepositoryEnums
    {
        public enum QueryOptions
        {
            None,
            AddIncludes,
            OrderDescending,
            OrderAscending
        }
    }
}
