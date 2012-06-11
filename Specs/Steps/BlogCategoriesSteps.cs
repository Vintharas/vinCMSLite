using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DomainRepos.Abstracts;
using TechTalk.SpecFlow;
using Domain.Entities;
using vinCMS.Controllers;
using System.Web.Mvc;
using Specs.Helpers;
using NUnit.Framework;

namespace Specs.Steps
{
    [Binding]
    public class StepDefinitions
    {
        private IEntityRepository<Category> CategoryRepo { get; set; }
        private Category Category1 { get; set; }
        private Category Category2 { get; set; }
        private Category Category3 { get; set; }
        private CategoryController Controller { get; set; }
        private ViewResult ListViewResult { get; set; }

            [Given(@"I have a series of categories")]
            public void GivenIHaveASeriesOfCategories()
            {
                Category1 = new Category {CategoryID = 1, Name = "Category 1"};
                Category2 = new Category {CategoryID = 2, Name = "Category 2"};
                Category3 = new Category {CategoryID = 3, Name = "Category 3"};
            }
        
            [Given(@"a repository with those categories")]
            public void GivenARepositoryWithThoseCategories()
            {
                CategoryRepo = Helpers.UnitTestHelpers.MockEntityRepository<Category>(Category1, Category2, Category3);
            }

            [Given(@"a category controller that uses that repository")]
            public void GivenACategoryControllerThatUsesThatRepository()
            {
                Controller = new CategoryController(CategoryRepo);
            }

            [When(@"I call the List action")]
            public void WhenICallTheListAction()
            {
                ListViewResult = Controller.List();
            }

            [Then(@"the result should be a view with the list of categories")]
            public void ThenTheResultShouldBeAViewWithTheListOfCategories()
            {
                var model = ((IEnumerable<Category>) ListViewResult.ViewData.Model);
                model.AsQueryable().Count().ShouldEqual(3);
                Assert.IsNotNull(model.AsQueryable().FirstOrDefault(x => x.CategoryID == 1));
                Assert.IsNotNull(model.AsQueryable().FirstOrDefault(x => x.CategoryID == 2));
                Assert.IsNotNull(model.AsQueryable().FirstOrDefault(x => x.CategoryID == 3));
            }

            [Then(@"the result should be an alphabetically ordered list of categories")]
            public void ThenTheResultShouldBeAnAlphabeticallyOrderedListOfCategories()
            {
                var model = ((IEnumerable<Category>) ListViewResult.ViewData.Model);
                model.AsQueryable().Count().ShouldEqual(3);
                List<Category> listOfCategories = model.ToList();
                listOfCategories[0].Name.ShouldEqual(Category1.Name);
                listOfCategories[1].Name.ShouldEqual(Category2.Name);
                listOfCategories[2].Name.ShouldEqual(Category3.Name);
            }

    }
}
