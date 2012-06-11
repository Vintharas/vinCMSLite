Feature: Blog Admin
	The website will support an admin dashboard
	That, among other things, will allow the user to admin the blog
	The user authenticated as an administrator will be able to 
	manage blog posts: add new posts, edit existing posts, preview posts and delete posts


Scenario: The admin dashboard(Index) will show a subset of blog posts (paged) and draft posts
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I go into the admin dashboard
	Then the result should be a subset of blog posts and a list with the draft posts

Scenario: The admin dashboard(Index) will show a second subset of blog posts (paged) and draft posts when you press in 'older posts'
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I go into the admin dashboard and I press on the second page
	Then the result should be a subset of blog posts (those on page 2) and a list with the draft posts

Scenario: The admin will be able to add new posts by clicking in the 'Add New Post' link
	Given An admin
	And A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I go into the admin dashboard and I press on the 'Add New Post' link
	Then the result should be a form in which I can create a new blogPost

Scenario: The admin will be able to cancel the creation of a new blog post by clicking in the 'Cancel' link on the post creation form
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'cancel' button inside a create blog post form
	Then the result should be a redirection to the Index and a message saying that the blog post creation has been cancelled

Scenario: The admin will be able to preview the creation of a new blog post by clicking in the 'Preview' button on the post creation form
	Given A series of blog posts
	And A new blog post that has been created and filled in by the user
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'preview' button inside a create blog post form
	Then the result should be the saving of the blog post as a draft and a preview of the blogpost

Scenario: The admin will be able to preview the creation of a new blog post but if the form is not valid, there will be a redirection to the same form
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'preview' button inside a create blog post form but the data is invalid
	Then the result should be a redirection to the same form with validation errors

Scenario: The admin will be able to publish a new blog post by clicking in the 'Publish' button on the post creation form
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'publish' button inside a create blog post form
	Then the result should be the saving of the blog post, a redirection to the admin index and a message saying the creation was successful

Scenario: The admin will be able to publish a new blog post but if the form is not valid, there will be a redirection to the same form
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'publish' button inside a create blog post form but the data is invalid
	Then the result should be a redirection to the same form with validation errors

Scenario: The admin will be able to edit an existing blog post
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'edit' link beside a certain blog posts
	Then the result should be a form that allow us to edit the form

Scenario: The admin will be able to edit an existing blog post and save it
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'save' link inside the edition form
	Then the result should be the saving of the modified blog post and a redirection to the index of the dashboard with a message saying the edition was successful
	
Scenario: The admin will be able to edit an existing blog post and publish it if it was a draft
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'publish' link inside the edition form
	Then the result should be the saving of the modified blog post, its publishing and a redirection to the dashboard with a message saying it was successfully published

Scenario: The admin will be able to edit an existing blog post and publish it. If it is not a draft there will be a redirection to the index and a message
	Given A series of blog posts
 	And A blog post that is not a draft
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'publish' link inside the edition form with this no-draft blogpost
	Then the result should be the saving of the modified blog post and a redirection to the index saying that the blog post was already published

Scenario: The admin will be able to see a blog post deletion form when he clicks in the 'delete' link beside a blog post
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'delete' link in the dashboard beside a blog post
	Then the result should be a form that asks me if I am sure about deleting the post

Scenario: The admin will be able to cancel the deletion of a blog post by clicking on the 'Cancel' button in the deletion form
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'Cancel' link in the blog post deletion form
	Then the result should be a redirection to the dashboard and a message saying that I have cancelled the deletion of the blog post

Scenario: The admin will be able to delete a blog post by clicking on the 'Delete' button in the deletion form
	Given A series of blog posts
	And A series of categories
	And A repository that allows us to access to the persistence layer
	And An admin controller
	When I click in the 'Delete' link in the blog post deletion form
	Then the result should be a redirection to the dashboard and a message saying that I have deleted the blogpost successfully

Scenario: The admin will be able to see a category management dashboard when he clicks on the 'Manage Categories' link
	Given A series of categories
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Manage Categories' link in the admin dashboard
	Then the result should be a view that renders a form for adding new categories, and a number of forms for editing or deleting other categories

Scenario: The admin will be able to add a new category by filling the form for a new category and clicking in the 'Add' button
	Given A series of categories
	And A new category
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Add' link in the 'Manage Categories' dashboard
	Then the result should be the creation of a new category and a redirection to the 'Manage Categories' dashboard with a proper message

Scenario: The admin will be able to add a new category but if the data is not valid the form will load again
	Given A series of categories
	And A new category
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Add' link in the 'Manage Categories' dashboard, having added invalid data
	Then the result should be a redirection to the form with a validation error message

Scenario: The admin will be able to edit a given category
	Given A series of categories
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Delete' link in the 'Manage Categories' dashboard
	Then the result should be a form that allows me to edit the category

Scenario: The admin will be able to edit a given category and save it
	Given A series of categories
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Save' button in the edit category form
	Then the result should be the saving of the edited category, a redirection to the category management dashboard and a message

Scenario: The admin will be able to edit a given category, but if the data is invalid, the user will be notified and redirected againt to the form
	Given A series of categories
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Save' button in the edit category form when the data is invalid
	Then the result should be the same form with a validation message

Scenario: The admin will be able to cancel the edition of a category
	Given A series of categories
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Cancel' button in the edit category form
	Then the result should be a redirection to the main category management dashboard

Scenario: The admin will be able to access a deletion form for a given category by clicking on the 'Delete' link in the category admin dashboard
	Given A series of categories
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Delete' button in the category admin dashboard
	Then the result should be a redirection to the delete category action that will show a deletion form

Scenario: The admin will be able to delete a given category by clicking in the 'delete' button in the deletion form
	Given A series of categories
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Delete' button in the category deletion form
	Then the result should be the deletion of the category and a redirection to the category admin dashboard

Scenario: The admin will be able to cancel the deletion of a given category by clicking in the 'cancel' button in the deletion form
	Given A series of categories
	And A repository that allows us to access to the persistence layer to work with the categories
	And An admin controller
	When I click in the 'Cancel' button in the category deletion form
	Then the result should be a redirection to the category admin dashboard an a message about the cancellation
