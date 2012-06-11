using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace vinCMS.Infraestructure.Authentication
{
    /// <summary>
    /// Wrapper that wraps the membership library in order to ease unit testing
    /// </summary>
    public class MembershipWrapper : IMembership
    {
        /// <summary>
        /// Validates a given user by comparing his username and password with those
        /// in the system.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }

        public int MaxInvalidPasswordAttempts
        {
            get { return Membership.MaxInvalidPasswordAttempts; }
        }
    }
}