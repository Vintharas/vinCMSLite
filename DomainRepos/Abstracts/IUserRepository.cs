using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;

namespace DomainRepos.Abstracts
{
    public interface IUserRepository : IEntityRepository<User>
    {
        User GetUserByName(string userName);
        bool IsUserWithPassword(string userName, string password);
    }
}
