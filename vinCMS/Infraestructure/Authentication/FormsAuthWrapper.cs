using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace vinCMS.Infraestructure.Authentication
{
    /// <summary>
    /// Class that wraps the forms authentication library in order to ease unit testing
    /// </summary>
    public class FormsAuthWrapper : IFormsAuth
    {
        /// <summary>
        /// Method that sets an authentication cookie for a given user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="createPersistentCookie"></param>
        public void SetAuthCookie(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        /// <summary>
        /// Method that signs the user out
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}