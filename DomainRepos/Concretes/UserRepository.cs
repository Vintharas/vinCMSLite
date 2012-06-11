using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.ErrorHandling;
using DomainRepos.Abstracts;
using Domain.Entities;
using DomainStorage.Abstracts;
using DomainStorage.EFStorage;

namespace DomainRepos.Concretes{

    /// <summary>
    /// Repository that manages access to the context (unit of work) regarding Users
    /// </summary>
    public class UserRepository : IUserRepository
    {

        private readonly IContext _context;
        public IContext GetUnitOfWork
        {
            get { return _context; }
        }

        /// <summary>
        /// Returns a user that has the same user name as the argument pass to the method
        /// Throws an exception if the username is not found
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserByName(string userName)
        {
            return GetQueryableEntitySet().Where(x => x.UserName == userName).Single();
        }

        /// <summary>
        /// Checks if there is a user with the a specific username and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsUserWithPassword(string userName, string password)
        {
            return GetQueryableEntitySet().Where(x => x.UserName == userName && x.Password == password).FirstOrDefault() == null
                       ? false
                       : true;
        }

        /// <summary>
        /// Class constructor. Associates a context to the repository
        /// </summary>
        public UserRepository(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetById(int id)
        {
            User user = GetUnitOfWork.Users.AddIncludes().Where(x => x.UserID == id).FirstOrDefault();
            if (user == null)
                throw new EntityNotFoundException();
            return user;
        }

        /// <summary>
        /// Adds a user to the context
        /// </summary>
        /// <param name="user"></param>
        public void Add(User user)
        {
            GetUnitOfWork.Users.AddObject(user);
        }

        /// <summary>
        /// Deletes a user from the context
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user)
        {
            GetUnitOfWork.Users.DeleteObject(user);
        }

        /// <summary>
        /// Returns an IList of all users
        /// </summary>
        /// <returns></returns>
        public IList<User> All()
        {
            return GetUnitOfWork.Users.AddIncludes().ToList();
        }

        /// <summary>
        /// Returns a IQueryable of User that you can use to make extra operations
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetQueryableEntitySet()
        {
            return GetUnitOfWork.Users.AsQueryable();
        }

        #region private helper methods



        #endregion


    }

        internal static class UserRepositoryExtensions
        {
            
            public static IQueryable<User> AddIncludes(this IQueryable<User> users)
            {
                return users.Include("UserRole.RolePersmissions");
            }
        }

}
