using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainRepos.Abstracts;
using Domain.Entities;
using vinCMS.Infraestructure.Filters;

namespace vinCMS.Controllers
{
    [ExceptionError]
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepo;
        private IPageRepository PageRepo { get { return _pageRepo; } }

        /// <summary>
        /// Class constructor. Initializes the repository object
        /// </summary>
        /// <param name="pageRepo"></param>
        public PageController(IPageRepository pageRepo)
        {
            _pageRepo = pageRepo;
        }

        /// <summary>
        /// Action method that renders a page detail for a given path, that is, the page itself
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ActionResult Details(string path)
        {
            // Get page from context
            Page page = PageRepo.GetPageByPath(path);
            // Render view
            return View(page);
        }

        /// <summary>
        /// Renders home page of the website
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
            //get home page from context
            Page homePage = PageRepo.GetHomePage();
            if (homePage == null)
                return RedirectToAction("index", "blog");
            //render view
            return View(homePage);
        }

    }
}
