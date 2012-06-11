Feature: Web Accounts
	The website has a account system that manages authentication
	A series of users with password associated
	The system is controlled by a web UI

Scenario: The user can access to log in form in the website
	Given A series of users
	And A user view model
	And A user repository to access those users
	And A forms authentication wrapper
	And A membership system wrapper
	And An account controller
	When I go into the login website
	Then a login form is shown for me to log in

Scenario: The user can log in the website
	Given A series of users
	And A user view model
	And A user repository to access those users
	And A forms authentication wrapper
	And A membership system wrapper
	And An account controller
	When I fill in the login form correctly and click on the login button
	Then I log in the website and I am redirected to the main admin page

Scenario: The user can cancel the log in process in the website
	Given A series of users
	And A user view model
	And A user repository to access those users
	And A forms authentication wrapper
	And A membership system wrapper
	And An account controller
	When I click the Cancel button in the login form
	Then I cancel the login process and I am redirected to the main page

Scenario: The user will get a validation error message if he doesn't fill in right the user form
	Given A series of users
	And A user view model
	And A user repository to access those users
	And A forms authentication wrapper
	And A membership system wrapper
	And An account controller
	When I fill in the login form incorrectly in terms of validation and click on the login button
	Then The validation should kick in, show a message and reload the form

Scenario: The user will get an error message if the username or password are incorrect
	Given A series of users
	And A user view model
	And A user repository to access those users
	And A forms authentication wrapper
	And A membership system wrapper
	And An account controller
	When I type in a wrong username or password and click on the login button
	Then I should get an error message and the form should be reloaded

Scenario: The user can log out of the clicking in the logout link that appears in several places of the website
	Given A series of users
	And A user view model
	And A user repository to access those users
	And A forms authentication wrapper
	And A membership system wrapper
	And An account contoller
	When I click in the logout link
	Then I log out from the web app

