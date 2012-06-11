using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DomainRepos.Abstracts;
using Specs.Helpers;
using TechTalk.SpecFlow;
using Domain.Entities;
using vinCMS.Controllers;

namespace Specs.Steps
{
    [Binding]
    public class PageTagsSteps
    {
        private Tag Tag1 { get; set; }
        private Tag Tag2 { get; set; }
        private Tag Tag3 { get; set; }
        private Tag Tag4 { get; set; }
        private BlogPost Post1 { get; set; }
        private BlogPost Post2 { get; set; }
        private IEntityRepository<Tag> TagRepository { get; set; }
        private TagController Controller { get; set; }
        private IEnumerable<Tag> Model { get; set; }
            
        [Given(@"a series of tags")]
        public void GivenASeriesOfTags()
        {
            Tag1 = new Tag {Name = "tag 1", TagID = 1};
            Tag2 = new Tag {Name = "tag 2", TagID = 2};
            Tag3 = new Tag {Name = "tag 3", TagID = 3};
            Tag4 = new Tag {Name = "tag 4", TagID = 4};
            Post1 = new BlogPost {ContentID = 1};
            Post2 = new BlogPost {ContentID = 2};
            Tag1.ContentContainers.Add(Post1);
            Tag1.ContentContainers.Add(Post2);
            Tag2.ContentContainers.Add(Post1);
        }

        [Given(@"a repository with those tags")]
        public void GivenARepositoryWithThoseTags()
        {
            TagRepository = Helpers.UnitTestHelpers.MockEntityRepository<Tag>(Tag1, Tag2, Tag3, Tag4);
        }

        [Given(@"a tag controller")]
        public void GivenATagController()
        {
            Controller = new TagController(TagRepository);
        }

        [When(@"the user calls the list action")]
        public void WhenTheUserCallsTheListAction()
        {
            ViewResult result = Controller.List();
            Model = (IEnumerable<Tag>)result.ViewData.Model;
        }

        [Then(@"the result should be list of the most common tags")]
        public void ThenTheResultShould5BeListOfTheMostCommonTags()
        {
            // there are for tags
            Model.AsQueryable().Count().ShouldEqual(4);
            // they are ordered with the most common tags at the beginning and going down
            Model.AsQueryable().First().TagID.ShouldEqual(1);
            Model.ToList()[1].TagID.ShouldEqual(2);
        }

    }
}
