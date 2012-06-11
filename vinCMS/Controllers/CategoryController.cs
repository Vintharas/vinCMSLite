using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using DomainRepos.Abstracts;

namespace vinCMS.Controllers
{
    /// <summary>
    /// Controller for Category Objects. It will manage category requests and
    /// prepare categories to be displayed in view
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly IEntityRepository<Category> _categoryRepo;
        private IEntityRepository<Category> CategoryRepo { get { return _categoryRepo; } }

        /// <summary>
        /// Class constructor for the controller category. Takes an object that
        /// implements the IEntityRepository of Category interface
        /// </summary>
        public CategoryController(IEntityRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        /// <summary>
        /// Action method that lists a series of blog categories. The
        /// categories will be ordered alphabetically
        /// </summary>
        /// <returns></returns>
        public ViewResult List()
        {
            var listOfCategories = CategoryRepo.All().OrderBy(x => x.Name).ToList();
            return View(listOfCategories);
        }

    }
}
