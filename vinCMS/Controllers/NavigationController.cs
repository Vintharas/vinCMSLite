using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DomainRepos.Abstracts;
using Domain.Entities;
using vinCMS.Helpers.Routing;
using vinCMS.Models;

namespace vinCMS.Controllers
{
    /// <summary>
    /// Controller that manages navigation within the site
    /// </summary>
    public class NavigationController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepo;
        private IBlogPostRepository BlogPostRepo { get { return _blogPostRepo; } }

        /// <summary>
        /// Class constructor. Initializes the repository object
        /// </summary>
        public NavigationController(IBlogPostRepository blogPostRepo)
        {
            _blogPostRepo = blogPostRepo;
        }

        /// <summary>
        /// Action method that renders a navigation bar for the website
        /// </summary>
        /// <returns></returns>
        public ViewResult NavBar()
        {
            // create view model
            List<NavBarViewModel> viewModel = new List<NavBarViewModel>();
            // Home navigation link
            AddHomePageToNavBar(viewModel);
            // Blog navigation link
            viewModel.Add(new NavBarViewModel
                              { 
                                  NavLinkTitle =  "blog",
                                  NavLinkRoute = new RouteValueDictionary { { "controller", "blog" }, { "action", "index" } }
                              });
            // add rest of navigable pages
            AddNavigablePagesToNavBar(viewModel);
            // render navigation bar
            return View(viewModel);
        }

        #region private auxiliary methods

        /// <summary>
        /// Adds the home page to the navigation bar
        /// </summary>
        /// <param name="viewModel"></param>
        private void AddHomePageToNavBar(List<NavBarViewModel> viewModel)
        {
            Page homePage = BlogPostRepo.GetHomePage();
            if (homePage != null)
            {
                viewModel.Add(new NavBarViewModel
                                  {
                                      NavLinkTitle = "home",
                                      NavLinkRoute = new RouteValueDictionary { { "controller", "page" }, {"action", "home"} }
                                  });
            }
        }

        /// <summary>
        /// Adds a page to the navigation bar
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        private void AddPageToNavBar(Page page, List<NavBarViewModel> viewModel)
        {
            viewModel.Add(new NavBarViewModel
                              {
                                  NavLinkTitle = page.Title.ToLower(),
                                  NavLinkRoute = page.GetPageDetails()
                              });
        }

        /// <summary>
        /// Adds navigable pages to the navigation bar
        /// </summary>
        /// <param name="viewModel"></param>
        private void AddNavigablePagesToNavBar(List<NavBarViewModel> viewModel)
        {
            IEnumerable<Page> listOfNavigablePages = BlogPostRepo.GetListOfNavigablePages();
            foreach (var navPage in listOfNavigablePages)
            {
                AddPageToNavBar(navPage, viewModel);
            }
        }

        #endregion

    }
}
