Feature: Page Tags
	The blog posts and pages of the website will be classified by tag
	The tag will be useful by means of filtering blog posts by tag
	It will also give a certain idea of what a certain blog or page is about
	The tags will work as well as keywords that will be placed in the metadata header of any given page

@mytag
Scenario: Show list of most common tags
	Given a series of tags 
	And a repository with those tags
	And a tag controller
	When the user calls the list action
	Then the result should be list of the most common tags
