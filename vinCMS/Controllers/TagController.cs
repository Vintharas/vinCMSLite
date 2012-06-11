using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using DomainRepos.Abstracts;

namespace vinCMS.Controllers
{

    public class TagController : Controller
    {
        private readonly IEntityRepository<Tag> _tagRepo;
        private IEntityRepository<Tag> TagRepo { get { return _tagRepo; } }

        private const int NUMBER_OF_TAGS = 25;

        /// <summary>
        /// Class constructor that initializes the tag repository
        /// </summary>
        /// <param name="tagRepo"></param>
        public TagController(IEntityRepository<Tag> tagRepo)
        {
            _tagRepo = tagRepo;
        }

        /// <summary>
        /// List action method that gets the most common
        /// </summary>
        /// <returns></returns>
        public ViewResult List()
        {
            List<Tag> listOfTags =
                _tagRepo.GetQueryableEntitySet().OrderByDescending(x => x.ContentContainers.Count).
                    Take(NUMBER_OF_TAGS).ToList();
            return View(listOfTags);
        }

    }
}
