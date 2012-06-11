using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Domain.Entities;
using DomainRepos.Abstracts;
using PagedList;
using TechTalk.SpecFlow;
using vinCMS.Controllers;
using vinCMS.Models;
using Specs.Helpers;
using Moq;
using System.Web;
using System.Web.Routing;

namespace Specs.Steps
{
    [Binding]
    public class BlogAdminSteps
    {
        // blog posts
        private BlogPost BlogPost1 { get; set; }
        private BlogPost BlogPost2 { get; set; }
        private BlogPost BlogPost3 { get; set; }
        private BlogPost BlogPost4 { get; set; }
        private BlogPost BlogPost5 { get; set; }
        private BlogPost BlogPost6 { get; set; }
        private BlogPost BlogPost7 { get; set; }
        private BlogPost BlogPostFilledByUser { get; set; }
        private BlogPost BlogPostNotDraft { get; set; }
        // Categories
        private Category Category1 { get; set; }
        private Category Category2 { get; set; }
        private Category CategoryNew { get; set; }
        // User
        private User Author { get; set; }
        // infraestructure
        private IBlogPostRepository BlogPostRepo { get; set; }
        private AdminController Controller { get; set; }
        // Result
        private ActionResult Result { get; set; }

        #region TheAdminDashboardIndexWillShowASubsetOfBlogPostsPagedAndDraftPosts

        [Given(@"A series of blog posts")]
        public void GivenASeriesOfBlogPosts()
        {
            // blogposts
            BlogPost1 = new BlogPost { ContentID = 1, PublishingDate = DateTime.Today};
            BlogPost2 = new BlogPost { ContentID = 2, PublishingDate = DateTime.Today.AddDays(1)};
            BlogPost3 = new BlogPost { ContentID = 3, PublishingDate = DateTime.Today.AddDays(2)};
            BlogPost4 = new BlogPost { ContentID = 4, PublishingDate = DateTime.Today.AddDays(3)};
            BlogPost5 = new BlogPost { ContentID = 5, PublishingDate = DateTime.Today.AddDays(4)};
            BlogPost6 = new BlogPost { ContentID = 6, PublishingDate = DateTime.Today.AddDays(5)};
            // drafts
            BlogPost7 = new BlogPost { ContentID = 7, IsDraft = true, PublishingDate = DateTime.Today.AddDays(6)};
            // blogpost filled by user
            BlogPostFilledByUser = new BlogPost
            {
                ContentID = 11,
                AuthorID = 1,
                BodyContent = "Content",
                Title = "Title",
                CreationDate = DateTime.Now,
                PublishingDate = DateTime.Now,
                IsDraft = true
            };
            // blogpost that is not a draft
            BlogPostNotDraft = new BlogPost() { ContentID = 20, IsDraft = false };
        }
        
        [Given(@"A series of categories")]
        public void GivenASeriesOfCategories()
        {
            Category1 = new Category {CategoryID = 1};
            Category2 = new Category {CategoryID = 2};
        }

        [Given(@"A repository that allows us to access to the persistence layer")]
        public void GivenARepositoryThatAllowsUsToAccessTheseBlogposts()
        {
            var MoqBlogPostRepo = Helpers.UnitTestHelpers.MockBlogPostRepositoryReturnsMoqObject(BlogPost1, BlogPost2, BlogPost3, 
                                                                                                BlogPost4, BlogPost5, BlogPost6,
                                                                                                BlogPost7);
            // Setup
            MoqBlogPostRepo.Setup(x => x.GetListOfCategories()).Returns(new List<Category>{Category1, Category2});
            MoqBlogPostRepo.Setup(x => x.PublishBlogPost(BlogPostFilledByUser));
            MoqBlogPostRepo.Setup(x => x.UpdatePost(BlogPost1, null, ""));
            MoqBlogPostRepo.Setup(x => x.GetById(BlogPost1.ContentID)).Returns(BlogPost1);
            MoqBlogPostRepo.Setup(x => x.GetById(BlogPostNotDraft.ContentID)).Returns(BlogPostNotDraft);
            MoqBlogPostRepo.Setup(x => x.GetById(BlogPost7.ContentID)).Returns(BlogPost7);
            MoqBlogPostRepo.Setup(x => x.Delete(BlogPost1));
            // loading mockup of IBlogPostRepository
            BlogPostRepo = MoqBlogPostRepo.Object;
        }

        [Given(@"An admin controller")]
        public void GivenAnAdminController()
        {
            Controller = new AdminController(BlogPostRepo);
            ControllerContext controllerContext = new ControllerContext(new Mock<HttpContextBase>().Object, new RouteData(), Controller);
            Controller.ControllerContext = controllerContext;
            Controller.ValueProvider = new ValueProviderCollection();
        }

        [When(@"I go into the admin dashboard")]
        public void WhenIGoIntoTheAdminDashboard()
        {
            Result = Controller.Index();
        }

        [Then(@"the result should be a subset of blog posts and a list with the draft posts")]
        public void ThenTheResultShouldBeASubsetOfBlogPostsAndAListWithTheDraftPosts()
        {
            ViewResult viewResult = (ViewResult) Result;
            AdminViewModel model = (AdminViewModel) viewResult.ViewData.Model;
            // note that the number of posts per page is harcoded in the controller!! wow! how bad!
            // subset of blog posts
            model.PagedListBlogPosts.Count.ShouldEqual(5);
            model.PagedListBlogPosts.First().ContentID.ShouldEqual(6);
            model.PagedListBlogPosts[1].ContentID.ShouldEqual(5);
            // draft posts
            model.PostDraftsList.Count().ShouldEqual(1);
            model.PostDraftsList.First().ContentID.ShouldEqual(7);
        }

        #endregion

        #region TheAdminDashboardIndexWillShowASecondSubsetOfBlogPostsPagedAndDraftPostsWhenYouPressInOlderPosts

        [When(@"I go into the admin dashboard and I press on the second page")]
        public void WhenIGoIntoTheAdminDashboardAndIPressOnTheSecondPage()
        {
            int secondPage = 1;
            Result = Controller.Index(secondPage);
        }

        [Then(@"the result should be a subset of blog posts \(those on page 2\) and a list with the draft posts")]
        public void ThenTheResultShouldBeASubsetOfBlogPostsThoseOnPage2AndAListWithTheDraftPosts()
        {
            ViewResult viewResult = (ViewResult)Result;
            AdminViewModel model = (AdminViewModel)viewResult.ViewData.Model;
            // note that the number of posts per page is harcoded in the controller!! wow! how bad!
            // subset of blog posts
            model.PagedListBlogPosts.Count.ShouldEqual(1);
            model.PagedListBlogPosts.First().ContentID.ShouldEqual(1);
            // draft posts
            model.PostDraftsList.Count().ShouldEqual(1);
            model.PostDraftsList.First().ContentID.ShouldEqual(7);
        }



        #endregion

        #region TheAdminWillBeAbleToAddNewPostsByClickingInTheAddNewPostLink

        [Given(@"An admin")]
        public void GivenAnAdmin()
        {
            Author = new User {UserID = 1};
        }

        [When(@"I go into the admin dashboard and I press on the 'Add New Post' link")]
        public void WhenIGoIntoTheAdminDashboardAndIPressOnTheAddNewPostLink()
        {
            // Need to set the proper login system for this to work!!
            // Right now I am harcoding and admin user
            Result = Controller.CreateBlogPost();
        }

        [Then(@"the result should be a form in which I can create a new blogPost")]
        public void ThenTheResultShouldBeAFormInWhichICanCreateANewBlogPost()
        {
            ViewResult viewResult = (ViewResult) Result;
            // the model is a new blogpost and a list of categories is available as a field in ViewData
            BlogPost newBlogPost = (BlogPost) viewResult.ViewData.Model;
            newBlogPost.ContentID.ShouldEqual(0);
            ((IEnumerable<Category>)viewResult.ViewData[vinCMS.Infraestructure.Constants.VIEW_CATEGORYLIST]).Count().ShouldEqual(2);
        }

        #endregion

        #region TheAdminWillBeAbleToCancelTheCreationOfANewBlogPostByClickingInTheCancelLinkOnThePostCreationForm

        [When(@"I click in the 'cancel' button inside a create blog post form")]
        public void WhenIClickInTheCancelButtonInsideACreateBlogPostForm()
        {
            Result = Controller.CancelBlogPostCreation();
        }

        [Then(@"the result should be a redirection to the Index and a message saying that the blog post creation has been cancelled")]
        public void ThenTheResultShouldBeARedirectionToTheIndexAndAMessageSayingThatTheBlogPostCreationHasBeenCancelled()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("Index");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.CANCEL_CREATE);
        }     
        
        #endregion

        #region TheAdminWillBeAbleToPreviewTheCreationOfANewBlogPostButIfTheFormIsNotValidThereWillBeARedirectionToTheSameForm

        [Given(@"A new blog post that has been created and filled in by the user")]
        public void GivenANewBlogPostThatHasBeenCreatedAndFilledInByTheUser()
        {
            BlogPostFilledByUser = new BlogPost
                                       {
                                           ContentID = 11,
                                           AuthorID = 1,
                                           BodyContent = "Content",
                                           Title = "Title",
                                           CreationDate = DateTime.Now,
                                           PublishingDate = DateTime.Now,
                                           IsDraft = true
                                       };
        }

        [When(@"I click in the 'preview' button inside a create blog post form but the data is invalid")]
        public void WhenIClickInThePreviewButtonInsideACreateBlogPostFormButTheDataIsInvalid()
        {
            Controller.ModelState.AddModelError("Error", "There was an error in the model");
            Result = Controller.CreateBlogPost(BlogPostFilledByUser, null, string.Empty, "Preview");
        }

        [Then(@"the result should be a redirection to the same form with validation errors")]
        public void ThenTheResultShouldBeARedirectionToTheSameFormWithValidationErrors()
        {
            ViewResult viewResult = (ViewResult) Result;
            ((BlogPost)viewResult.ViewData.Model).ContentID.ShouldEqual(BlogPostFilledByUser.ContentID);
        }

        #endregion

        #region TheAdminWillBeAbleToPreviewTheCreationOfANewBlogPostByClickingInThePreviewButtonOnThePostCreationForm

        [When(@"I click in the 'preview' button inside a create blog post form")]
        public void WhenIClickInThePreviewButtonInsideACreateBlogPostForm()
        {
            Result = Controller.CreateBlogPost(BlogPostFilledByUser, null, string.Empty, "Preview");
        }

        [Then(@"the result should be the saving of the blog post as a draft and a preview of the blogpost")]
        public void ThenTheResultShouldBeTheSavingOfTheBlogPostAsADraftAndAPreviewOfTheBlogpost()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)Result;
            redirectResult.RouteValues["action"] = "PreviewBlogPost";
            redirectResult.RouteValues["id"] = BlogPostFilledByUser.ContentID;
        }

        #endregion

        #region TheAdminWillBeAbleToPublishANewBlogPostByClickingInThePublishButtonOnThePostCreationForm

        [When(@"I click in the 'publish' button inside a create blog post form")]
        public void WhenIClickInThePublishButtonInsideACreateBlogPostForm()
        {
            Result = Controller.CreateBlogPost(BlogPostFilledByUser, null, string.Empty, "Publish");
        }

        [Then(@"the result should be the saving of the blog post, a redirection to the admin index and a message saying the creation was successful")]
        public void ThenTheResultShouldBeTheSavingOfTheBlogPostARedirectionToTheAdminIndexAndAMessageSayingTheCreationWasSuccessful()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("Index");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.SUCCESS_PUBLISH);
        }
        
        #endregion

        #region TheAdminWillBeAbleToPublishANewBlogPostButIfTheFormIsNotValidThereWillBeARedirectionToTheSameForm

        [When(@"I click in the 'publish' button inside a create blog post form but the data is invalid")]
        public void WhenIClickInThePublishButtonInsideACreateBlogPostFormButTheDataIsInvalid()
        {
            Controller.ModelState.AddModelError("Error", "There was an error in the model");
            Result = Controller.CreateBlogPost(BlogPostFilledByUser, null, string.Empty, "Publish");
        }

        #endregion

        #region TheAdminWillBeAbleToEditAnExistingBlogPost

        [When(@"I click in the 'edit' link beside a certain blog posts")]
        public void WhenIClickInTheEditLinkBesideACertainBlogPosts()
        {
            Result = Controller.EditBlogPost(BlogPost1.ContentID);
        }

        [Then(@"the result should be a form that allow us to edit the form")]
        public void ThenTheResultShouldBeAFormThatAllowUsToEditTheForm()
        {
            ViewResult viewResult = (ViewResult) Result;
            ((BlogPost) viewResult.ViewData.Model).ContentID.ShouldEqual(BlogPost1.ContentID);
            ((IEnumerable<Category>)viewResult.ViewData[vinCMS.Infraestructure.Constants.VIEW_CATEGORYLIST]).Count().ShouldEqual(2);
        }

        #endregion

        #region TheAdminWillBeAbleToEditAnExistingBlogPostAndSaveIt

        [When(@"I click in the 'save' link inside the edition form")]
        public void WhenIClickInTheSaveLinkInsideTheEditionForm()
        {
            Result = Controller.EditBlogPost(BlogPost1.ContentID, null, "", "Save");
        }

        [Then(@"the result should be the saving of the modified blog post and a redirection to the index of the dashboard with a message saying the edition was successful")]
        public void ThenTheResultShouldBeTheSavingOfTheModifiedBlogPostAndARedirectionToTheIndexOfTheDashboardWithAMessageSayingTheEditionWasSuccessful()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("Index");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.SUCCESS_EDIT);
        }

        #endregion
        
        #region TheAdminWillBeAbleToEditAnExistingBlogPostAndPublishIt_IfItIsNotADraftThereWillBeARedirectionToTheIndexAndAMessage

        [Given(@"A blog post that is not a draft")]
        public void GivenABlogPostThatIsNotADraft()
        {
            BlogPostNotDraft = new BlogPost() {ContentID = 20, IsDraft = false};
        }

        [When(@"I click in the 'publish' link inside the edition form with this no-draft blogpost")]
        public void WhenIClickInThePublishLinkInsideTheEditionFormWithThisNo_DraftBlogpost()
        {
            Controller.ViewData.Model = BlogPostNotDraft;
            Result = Controller.EditBlogPost(BlogPostNotDraft.ContentID, null, "", "Publish");
        }

        [Then(@"the result should be the saving of the modified blog post and a redirection to the index saying that the blog post was already published")]
        public void ThenTheResultShouldBeTheSavingOfTheModifiedBlogPostAndARedirectionToTheIndexSayingThatTheBlogPostWasAlreadyPublished()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("Index");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.SUCCESS_EDIT_ALREADY_PUBLISHED);
        }


        #endregion

        #region TheAdminWillBeAbleToEditAnExistingBlogPostAndPublishItIfItWasADraft
        
        [When(@"I click in the 'publish' link inside the edition form")]
        public void WhenIClickInThePublishLinkInsideTheEditionForm()
        {
            Result = Controller.EditBlogPost(BlogPost7.ContentID, null, "", "Publish");
        }

        [Then(@"the result should be the saving of the modified blog post, its publishing and a redirection to the dashboard with a message saying it was successfully published")]
        public void ThenTheResultShouldBeTheSavingOfTheModifiedBlogPostItsPublishingAndARedirectionToTheDashboardWithAMessageSayingItWasSuccessfullyPublished()
        {
            RedirectToRouteResult redirectToRouteResult = (RedirectToRouteResult) Result;
            redirectToRouteResult.RouteValues["action"].ShouldEqual("Index");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.SUCCESS_EDITPUBLISH);
        }

        #endregion

        #region TheAdminWillBeAbleToSeeABlogPostDeletionFormWhenHeClicksInTheDeleteLinkBesideABlogPost

        [When(@"I click in the 'delete' link in the dashboard beside a blog post")]
        public void WhenIClickInTheDeleteLinkInTheDashboardBesideABlogPost()
        {
            Result = Controller.DeleteBlogPost(BlogPost1.ContentID);
        }

        [Then(@"the result should be a form that asks me if I am sure about deleting the post")]
        public void ThenTheResultShouldBeAFormThatAsksMeIfIAmSureAboutDeletingThePost()
        {
            ViewResult viewResult = (ViewResult) Result;
            ((BlogPost)viewResult.ViewData.Model).ContentID.ShouldEqual(BlogPost1.ContentID); 
        }

        
        #endregion

        #region TheAdminWillBeAbleToCancelTheDeletionOfABlogPostByClickingOnTheCancelButtonInTheDeletionForm

        [When(@"I click in the 'Cancel' link in the blog post deletion form")]
        public void WhenIClickInTheCancelLinkInTheBlogPostDeletionForm()
        {
            Result = Controller.CancelBlogPostDeletion();
        }

        [Then(@"the result should be a redirection to the dashboard and a message saying that I have cancelled the deletion of the blog post")]
        public void ThenTheResultShouldBeARedirectionToTheDashboardAndAMessageSayingThatIHaveCancelledTheDeletionOfTheBlogPost()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("Index");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.CANCEL_DELETE);
        }
        
        #endregion

        #region TheAdminWillBeAbleToDeleteABlogPostByClickingOnTheDeleteButtonInTheDeletionForm

        [When(@"I click in the 'Delete' link in the blog post deletion form")]
        public void WhenIClickInTheDeleteLinkInTheBlogPostDeletionForm()
        {
            Result = Controller.DeleteBlogPost_Post(BlogPost1.ContentID);
        }

        [Then(@"the result should be a redirection to the dashboard and a message saying that I have deleted the blogpost successfully")]
        public void ThenTheResultShouldBeARedirectionToTheDashboardAndAMessageSayingThatIHaveDeletedTheBlogpostSuccessfully()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("Index");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.SUCCESS_DELETE);
        }

        #endregion

        #region TheAdminWillBeAbleToSeeACategoryManagementDashboardWhenHeClicksOnTheManageCategoriesLink

        [Given(@"A repository that allows us to access to the persistence layer to work with the categories")]
        public void GivenARepositoryThatAllowsUsToAccessToThePersistenceLayerToWorkWithTheCategories()
        {
            var mockBlogPostRepo = Helpers.UnitTestHelpers.MockBlogPostRepositoryReturnsMoqObject(Category1, Category2);
            // Extra set up
            mockBlogPostRepo.Setup(x => x.SubmitChanges());
            mockBlogPostRepo.Setup(x => x.GetCategoryById(Category1.CategoryID)).Returns(Category1);
            // Get IBlogPostRepo
            BlogPostRepo = mockBlogPostRepo.Object;
        }

        [When(@"I click in the 'Manage Categories' link in the admin dashboard")]
        public void WhenIClickInTheManageCategoriesLinkInTheAdminDashboard()
        {
            Result = Controller.Categories();
        }

        [Then(@"the result should be a view that renders a form for adding new categories, and a number of forms for editing or deleting other categories")]
        public void ThenTheResultShouldBeAViewThatRendersAFormForAddingNewCategoriesAndANumberOfFormsForEditingOrDeletingOtherCategories()
        {
            ViewResult viewResult = (ViewResult) Result;
            var model = (vinCMS.Models.AdminCategoryViewModel) viewResult.ViewData.Model;
            model.PagedListCategories.Count.ShouldEqual(2);
            model.NewCategory.CategoryID.ShouldEqual(0);
        }


        #endregion

        #region TheAdminWillBeAbleToAddANewCategoryByFillingTheFormForANewCategoryAndClickingInTheAddButton

        [Given(@"A new category")]
        public void GivenANewCategory()
        {
            CategoryNew = new Category {CategoryID = 0, Name = "new category"};
        }

        [When(@"I click in the 'Add' link in the 'Manage Categories' dashboard")]
        public void WhenIClickInTheAddLinkInTheManageCategoriesDashboard()
        {
            Result = Controller.Categories(CategoryNew);
        }

        [Then(@"the result should be the creation of a new category and a redirection to the 'Manage Categories' dashboard with a proper message")]
        public void ThenTheResultShouldBeTheCreationOfANewCategoryAndARedirectionToTheManageCategoriesDashboardWithAProperMessage()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("categories");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.SUCCESS_CATEGORY_ADD);
        }

        #endregion

        #region TheAdminWillBeAbleToAddANewCategoryButIfTheDataIsNotValidTheFormWillLoadAgain

        [When(@"I click in the 'Add' link in the 'Manage Categories' dashboard, having added invalid data")]
        public void WhenIClickInTheAddLinkInTheManageCategoriesDashboardHavingAddedInvalidData()
        {
            Controller.ModelState.AddModelError("Name", "There was a validation error in the name of the Category");
            Result = Controller.Categories(CategoryNew);
        }

        [Then(@"the result should be a redirection to the form with a validation error message")]
        public void ThenTheResultShouldBeARedirectionToTheFormWithAValidationErrorMessage()
        {
            ViewResult viewResult = (ViewResult) Result;
            AdminCategoryViewModel model = (AdminCategoryViewModel) viewResult.ViewData.Model;
            model.NewCategory.CategoryID.ShouldEqual(CategoryNew.CategoryID);
            model.PagedListCategories.Count.ShouldEqual(2);
        }

        #endregion

        #region TheAdminWillBeAbleToEditAGivenCategory

        [When(@"I click in the 'Delete' link in the 'Manage Categories' dashboard")]
        public void WhenIClickInTheDeleteLinkInTheManageCategoriesDashboard()
        {
            Result = Controller.EditCategory(Category1.CategoryID);
        }

        [Then(@"the result should be a form that allows me to edit the category")]
        public void ThenTheResultShouldBeAFormThatAllowsMeToEditTheCategory()
        {
            ViewResult viewResult = (ViewResult) Result;
            ((Category) viewResult.ViewData.Model).CategoryID.ShouldEqual(Category1.CategoryID);
        }

        #endregion

        #region TheAdminWillBeAbleToCancelTheEditionOfACategory

        [When(@"I click in the 'Cancel' button in the edit category form")]
        public void WhenIClickInTheCancelButtonInTheEditCategoryForm()
        {
            Result = Controller.CancelCategoryEdition();
        }

        [Then(@"the result should be a redirection to the main category management dashboard")]
        public void ThenTheResultShouldBeARedirectionToTheMainCategoryManagementDashboard()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("categories");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.CANCEL_EDIT_CATEGORY);
        }

        
        #endregion

        #region TheAdminWillBeAbleToEditAGivenCategoryAndSaveIt

        [When(@"I click in the 'Save' button in the edit category form")]
        public void WhenIClickInTheSaveButtonInTheEditCategoryForm()
        {
            Category1.Name = "Category 1";
            Result = Controller.EditCategory_Post(Category1.CategoryID);
        }

        [Then(@"the result should be the saving of the edited category, a redirection to the category management dashboard and a message")]
        public void ThenTheResultShouldBeTheSavingOfTheEditedCategoryARedirectionToTheCategoryManagementDashboardAndAMessage()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("categories");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.SUCCESS_EDIT_CATEGORY);
        }

        #endregion

        #region TheAdminWillBeAbleToEditAGivenCategoryButIfTheDataIsInvalidTheUserWillBeNotifiedAndRedirectedAgaintToTheForm

        [When(@"I click in the 'Save' button in the edit category form when the data is invalid")]
        public void WhenIClickInTheSaveButtonInTheEditCategoryFormWhenTheDataIsInvalid()
        {
            Controller.ModelState.AddModelError("name", "there was an validation error with the category name!");
            Result = Controller.EditCategory_Post(Category1.CategoryID);
        }

        [Then(@"the result should be the same form with a validation message")]
        public void ThenTheResultShouldBeTheSameFormWithAValidationMessage()
        {
            ViewResult viewResult = (ViewResult) Result;
            ((Category) viewResult.ViewData.Model).CategoryID.ShouldEqual(Category1.CategoryID);
        }


        #endregion

        #region TheAdminWillBeAbleToAccessADeletionFormForAGivenCategoryByClickingOnTheDeleteLinkInTheCategoryAdminDashboard

        [When(@"I click in the 'Delete' button in the category admin dashboard")]
        public void WhenIClickInTheDeleteButtonInTheCategoryAdminDashboard()
        {
            Result = Controller.DeleteCategory(Category1.CategoryID);
        }

        [Then(@"the result should be a redirection to the delete category action that will show a deletion form")]
        public void ThenTheResultShouldBeARedirectionToTheDeleteCategoryActionThatWillShowADeletionForm()
        {
            ViewResult viewResult = (ViewResult) Result;
            ((Category)viewResult.ViewData.Model).CategoryID.ShouldEqual(Category1.CategoryID);
        }

        #endregion

        #region TheAdminWillBeAbleToCancelTheDeletionOfAGivenCategoryByClickingInTheCancelButtonInTheDeletionForm

        [When(@"I click in the 'Cancel' button in the category deletion form")]
        public void WhenIClickInTheCancelButtonInTheCategoryDeletionForm()
        {
            Result = Controller.CancelCategoryDeletion();
        }

        [Then(@"the result should be a redirection to the category admin dashboard an a message about the cancellation")]
        public void ThenTheResultShouldBeARedirectionToTheCategoryAdminDashboardAnAMessageAboutTheCancellation()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("categories");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.CANCEL_DELETE_CATEGORY);
        }

        #endregion TheAdminWillBeAbleToDeleteAGivenCategoryByClickingInTheDeleteButtonInTheDeletionForm

        [When(@"I click in the 'Delete' button in the category deletion form")]
        public void WhenIClickInTheDeleteButtonInTheCategoryDeletionForm()
        {
            Result = Controller.DeleteCategory_Post(Category1.CategoryID);
        }

        [Then(@"the result should be the deletion of the category and a redirection to the category admin dashboard")]
        public void ThenTheResultShouldBeTheDeletionOfTheCategoryAndARedirectionToTheCategoryAdminDashboard()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            redirectResult.RouteValues["action"].ShouldEqual("categories");
            Controller.TempData[vinCMS.Infraestructure.Constants.VIEW_MESSAGE].ShouldEqual(AdminController.SUCCESS_DELETE_CATEGORY);
        }


        #region

        #endregion
    }
}
