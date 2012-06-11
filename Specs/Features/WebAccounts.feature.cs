// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.4.0.0
//      Runtime Version:4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace Specs.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Web Accounts")]
    public partial class WebAccountsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "WebAccounts.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Web Accounts", "The website has a account system that manages authentication\nA series of users wi" +
                    "th password associated\nThe system is controlled by a web UI", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The user can access to log in form in the website")]
        public virtual void TheUserCanAccessToLogInFormInTheWebsite()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user can access to log in form in the website", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("A series of users");
#line 8
 testRunner.And("A user view model");
#line 9
 testRunner.And("A user repository to access those users");
#line 10
 testRunner.And("A forms authentication wrapper");
#line 11
 testRunner.And("A membership system wrapper");
#line 12
 testRunner.And("An account controller");
#line 13
 testRunner.When("I go into the login website");
#line 14
 testRunner.Then("a login form is shown for me to log in");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The user can log in the website")]
        public virtual void TheUserCanLogInTheWebsite()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user can log in the website", ((string[])(null)));
#line 16
this.ScenarioSetup(scenarioInfo);
#line 17
 testRunner.Given("A series of users");
#line 18
 testRunner.And("A user view model");
#line 19
 testRunner.And("A user repository to access those users");
#line 20
 testRunner.And("A forms authentication wrapper");
#line 21
 testRunner.And("A membership system wrapper");
#line 22
 testRunner.And("An account controller");
#line 23
 testRunner.When("I fill in the login form correctly and click on the login button");
#line 24
 testRunner.Then("I log in the website and I am redirected to the main admin page");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The user can cancel the log in process in the website")]
        public virtual void TheUserCanCancelTheLogInProcessInTheWebsite()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user can cancel the log in process in the website", ((string[])(null)));
#line 26
this.ScenarioSetup(scenarioInfo);
#line 27
 testRunner.Given("A series of users");
#line 28
 testRunner.And("A user view model");
#line 29
 testRunner.And("A user repository to access those users");
#line 30
 testRunner.And("A forms authentication wrapper");
#line 31
 testRunner.And("A membership system wrapper");
#line 32
 testRunner.And("An account controller");
#line 33
 testRunner.When("I click the Cancel button in the login form");
#line 34
 testRunner.Then("I cancel the login process and I am redirected to the main page");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The user will get a validation error message if he doesn\'t fill in right the user" +
            " form")]
        public virtual void TheUserWillGetAValidationErrorMessageIfHeDoesnTFillInRightTheUserForm()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user will get a validation error message if he doesn\'t fill in right the user" +
                    " form", ((string[])(null)));
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("A series of users");
#line 38
 testRunner.And("A user view model");
#line 39
 testRunner.And("A user repository to access those users");
#line 40
 testRunner.And("A forms authentication wrapper");
#line 41
 testRunner.And("A membership system wrapper");
#line 42
 testRunner.And("An account controller");
#line 43
 testRunner.When("I fill in the login form incorrectly in terms of validation and click on the logi" +
                    "n button");
#line 44
 testRunner.Then("The validation should kick in, show a message and reload the form");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The user will get an error message if the username or password are incorrect")]
        public virtual void TheUserWillGetAnErrorMessageIfTheUsernameOrPasswordAreIncorrect()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user will get an error message if the username or password are incorrect", ((string[])(null)));
#line 46
this.ScenarioSetup(scenarioInfo);
#line 47
 testRunner.Given("A series of users");
#line 48
 testRunner.And("A user view model");
#line 49
 testRunner.And("A user repository to access those users");
#line 50
 testRunner.And("A forms authentication wrapper");
#line 51
 testRunner.And("A membership system wrapper");
#line 52
 testRunner.And("An account controller");
#line 53
 testRunner.When("I type in a wrong username or password and click on the login button");
#line 54
 testRunner.Then("I should get an error message and the form should be reloaded");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The user can log out of the clicking in the logout link that appears in several p" +
            "laces of the website")]
        public virtual void TheUserCanLogOutOfTheClickingInTheLogoutLinkThatAppearsInSeveralPlacesOfTheWebsite()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The user can log out of the clicking in the logout link that appears in several p" +
                    "laces of the website", ((string[])(null)));
#line 56
this.ScenarioSetup(scenarioInfo);
#line 57
 testRunner.Given("A series of users");
#line 58
 testRunner.And("A user view model");
#line 59
 testRunner.And("A user repository to access those users");
#line 60
 testRunner.And("A forms authentication wrapper");
#line 61
 testRunner.And("A membership system wrapper");
#line 62
 testRunner.And("An account contoller");
#line 63
 testRunner.When("I click in the logout link");
#line 64
 testRunner.Then("I log out from the web app");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion