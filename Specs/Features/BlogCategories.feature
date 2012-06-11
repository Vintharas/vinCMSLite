Feature: Blog Categories
	The blog posts of the website will be classified by category
	The category will be useful by means of filtering blog posts by category
	It will also give a certain idea of what a certain blog is about


Scenario: Show list of categories
	Given I have a series of categories
	And a repository with those categories
	And a category controller that uses that repository
	When I call the List action
	Then the result should be a view with the list of categories


Scenario: Show list of categories alphabetically ordered
	Given I have a series of categories
	And a repository with those categories
	And a category controller that uses that repository
	When I call the List action
	Then the result should be an alphabetically ordered list of categories
