using System.Web.Mvc;
using Domain.Entities;
using DomainRepos.Abstracts;
using DomainRepos.Concretes;
using Specs.Helpers;
using TechTalk.SpecFlow;
using vinCMS.Controllers;
using vinCMS.Infraestructure.Authentication;
using vinCMS.Models;
using System.Web;
using System.Web.Routing;

namespace Specs.Steps
{
    [Binding]
    public class WebAccountsSteps
    {
        public User User1 { get; set; }
        public User User2 { get; set; }
        public User User3 { get; set; }
        public UserViewModel UserViewModel { get; set; }
        public UserViewModel UserViewModelFilled { get; set; }
        public IUserRepository UserRepo { get; set; }
        public IFormsAuth FormsAuth { get; set; }
        public IMembership MembershipWrapper { get; set; }
        public ActionResult Result { get; set; }
        public AccountController Controller { get; set; }


        #region TheUserCanAccessToLogInFormInTheWebsite

        [Given(@"A series of users")]
        public void GivenASeriesOfUsers()
        {
            User1 = new User{UserID = 1, UserName = "user", Password = "password"};
            User2 = new User {UserID = 2};
            User3 = new User {UserID = 3};
        }

        [Given(@"A user view model")]
        public void GivenAUserViewModel()
        {
            UserViewModel = new UserViewModel();
            UserViewModelFilled = new UserViewModel { UserName = "username", Password = "password"};
        }

        [Given(@"A user repository to access those users")]
        public void GivenAUserRepositoryToAccessThoseUsers()
        {
            var moqUserRepo = Helpers.UnitTestHelpers.MockUserRepositoryReturnsMoqObject(User1, User2, User3);
            // further settings
            moqUserRepo.Setup(x => x.GetById(User1.UserID)).Returns(User1);
            UserRepo = moqUserRepo.Object;
        }

        [Given(@"A forms authentication wrapper")]
        public void GivenAFormsAuthenticationWrapper()
        {
            //Forms auth wrapper mock up
            FormsAuth = new Moq.Mock<IFormsAuth>().Object;
        }

        [Given(@"A membership system wrapper")]
        public void GivenAMembershipSystemWrapper()
        {
            // Membership wrapper mockup
            var MoqMembership = new Moq.Mock<IMembership>();
            // setup
            MoqMembership.Setup(x => x.ValidateUser(UserViewModelFilled.UserName, UserViewModelFilled.Password)).Returns(true);
            MembershipWrapper = MoqMembership.Object;
        }

        [Given(@"An account controller")]
        public void GivenAnAccountController()
        {
           Controller = new AccountController(UserRepo, FormsAuth, MembershipWrapper);
            var httpContext = new Moq.Mock<HttpContextBase>().Object;
           ControllerContext controllerContext = new ControllerContext(httpContext, new RouteData(), Controller);
           Controller.ControllerContext = controllerContext;
           Controller.ValueProvider = new ValueProviderCollection();
        }

        [When(@"I go into the login website")]
        public void WhenIGoIntoTheLoginWebsite()
        {
            Result = Controller.LogIn();
        }

        [Then(@"a login form is shown for me to log in")]
        public void ThenALoginFormIsShownForMeToLogIn()
        {
            ViewResult viewResult = (ViewResult) Result;
            ((UserViewModel) viewResult.ViewData.Model).UserName.ShouldEqual(null);
            ((UserViewModel) viewResult.ViewData.Model).Password.ShouldEqual(null);
        }

        #endregion

        #region TheUserCanLogInTheWebsite

        [When(@"I fill in the login form correctly and click on the login button")]
        public void WhenIFillInTheLoginFormCorrectlyAndClickOnTheLoginButton()
        {
            Result = Controller.LogIn(UserViewModelFilled, null);
        }

        [Then(@"I log in the website and I am redirected to the main admin page")]
        public void ThenILogInTheWebsiteAndIAmRedirectedToTheMainAdminPage()
        {
            RedirectToRouteResult redirectResult = (RedirectToRouteResult) Result;
            // fails because it needs the url helper that is not initialized. Look at this!
            redirectResult.RouteValues["action"].ShouldEqual("index");
            redirectResult.RouteValues["controller"].ShouldEqual("admin");
        }

        #endregion

        #region TheUserCanCancelTheLogInProcessInTheWebsite


        #endregion

    }
}
