using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainRepos.Abstracts;
using Domain.Entities;
using vinCMS.Models;
using System.Web.Security;
using vinCMS.Infraestructure.Authentication;

namespace vinCMS.Controllers
{
    /// <summary>
    /// Controller that manages the account logic between the domain model
    /// layer and the views
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepo;
        private IUserRepository UserRepo { get { return _userRepo; } }
        private readonly IFormsAuth _formsAuth;
        private IFormsAuth FormsAuth { get { return _formsAuth; } }
        private readonly IMembership _membership;
        private IMembership MembershipWrapper { get { return _membership; } }

        #region Constants

        public const string ERROR_INCORRECT_LOGIN = "Username, password or both are incorrect";
        public const string CANCEL_LOGIN = "The login process has been cancelled";

        #endregion


        /// <summary>
        /// Class constructor. It initializes the IUserRepository object
        /// </summary>
        public AccountController(IUserRepository userRepo, IFormsAuth formsAuth, IMembership membership)
        {
            _userRepo = userRepo;
            _formsAuth = formsAuth;
            _membership = membership;
        }

        /// <summary>
        /// Action method that returns a login form, for the user to log in the website
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult LogIn()
        {
            return View(new UserViewModel());
        }

        /// <summary>
        /// Action method that manages the post request from a login form. 
        /// </summary>
        /// <param name="userModel"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken(Salt = "LogIn")]
        public ActionResult LogIn(UserViewModel userModel, string returnUrl)
        {
            // Check validation
            if (!ModelState.IsValid)
            {
                // Wrong data
                // Return same view with the same object
                return View(userModel);
            }
            // Valid Data -> Authenticate
            if (MembershipWrapper.ValidateUser(userModel.UserName, userModel.Password))
            {
                // Set cookie if it is a valid user
                FormsAuth.SetAuthCookie(userModel.UserName, false);
                return Redirect(returnUrl ?? Url.Action("index", "admin"));
            }
            // If the authentication fails
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE_ERROR] = ERROR_INCORRECT_LOGIN;
            return View(userModel);
        }

        /// <summary>
        /// Action method that logs the user out and redirects him to the index of the blog
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            FormsAuth.SignOut();
            return RedirectToAction("index", "blog");
        }

        /// <summary>
        /// Action method that cancels a log in process
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelLogIn()
        {
            TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE] = CANCEL_LOGIN;
            return RedirectToAction("index", "blog");
        }
    }
}
