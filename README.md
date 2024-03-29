# CookBookAPI
This API was created as a final project for the Code Kentucky Program. My intention was to create an application that would serve as a personal cookbook, a simple place in my computer that would store the recipes I plan on trying throughout this year that is much elevated from the excel spreadsheet I was initially using. 
Using SQL Server, my application stores essential data about my recipes, such as their description, category, rating out of 10, and a website link to find the instructions.
Turning this into a CRUD API, and using Swagger to execute requests, also allowed me to make essential modifications to my data, such as modifying or deleting old entries.

The 3 features from the required list I included in this project are:
   1. Make your application a CRUD API
   2. Have 2 or more tables (entities) in your application that are related and have a function return data from both entities a join table. (As seen with RecipeCategories)
   3. Add comments to your code explaining how you are using at least 2 of the solid principles.

# How to run:
After much consideration, advice from one of my mentors, and difficulty with AWS Elastic Beanstalk, it became clear that the best method to deploy the end result of my application would be to use Ngrok. This service creates an endpoint that is publicly accessible for applications with relative ease. So instead of downloading a million files, changing connection strings, etc. all you need is a link. The only downside is that my laptop must be running, and I have to generate a new link every time it starts. So, to run it, please message me on Slack with a time-frame my application needs to be available for. I will send you the link and it should pull up in the click of a button. I understand the probable inconvenience of this, but it is a work in progress. 😊

# How to use it:
One the website is clicked, an interface will appear called "cookbook3," and under it will be a series of sections (category, owner, recipe, etc.) and in each major section are rows with colorful markers on the left of them. All of the ones entitled “GET” and blue in color are for the purpose to retrieve data from my database. POST is for creation, PUT is for updating, and DELETE is to delete. We will walk through how to use each type, starting with GET.

GET REQUESTS

So, to use the first option of the category section  GET api/Category:
1.	Click the drop down arrow on the row.
2.	On the right, you should see a button that says “Try it out,” click it.
3.	A little below, a blue bar should appear that says “execute,” click this next.
4.	It should load, and then scroll down a bit until a 200 response and the text of data is on screen. It should look similar to this setup but with more entries:

  	
  	
  	    {"id": 2,
  	
  	    "type": "snack"}
  
  
6. 	What is displayed is all of the categories that currently exist in the database that we can link to our recipes, and the generated Ids for them.
7.	(optional) click the drop arrow again to close up the request, or scroll further down (it’s easy to get lost when multiple tabs are open).

You’ll notice there are several other GETs that are accessible, but these are more specific. The second GET asks for a category Id, which if you saw the data from before, is a number 2-11. If you click “try it out,” input a number in the required box with a number 2-11, and execute it, it should return to you what category is associated with that Id. If a false entry is detected, you’ll get a 404.
The next GET will give you all the recipes associated with the category that is represented with its categoryId. So, again, input an Id (for example: 2) and it may give you results, 2 should return pb&J and some other snacks. The last get request, “get Category/{recipeId}/category,” will give you the category associated with a recipe if you feed it a RecipeId, but for that you may want to access GETRecipe further down to see the options available, or taking a guess at a number 2-10 is a good bet. The usage of the GETs are pretty much the same for each section, in their title it should be evident that it will ask you for some {somethingID} and return to you a result at /result. 

 POST REQUESTS
 
 Featured also are green POST requests, which will create a new item of that section. So, lets use recipe for example, as it is the one that requires the most information.
1.	 Scroll to recipe and hit the drop down bar on the POST and the “try it out” button. 
2.	You’ll see there are two boxes for OwnerId and CategoryId, both of which must have a number for the post request to work. I’m going to pretend we will add a recipe for Baklava made by our pre-existing owner Gracie R. Baklava is a dessert, so we’ll type in the corresponding categoryID : 6,  and our OwnerID: 5
3.	Then you’ll see a request body text box for creation. Delete the entire Id row, including all punctuation, EF Core will generate that for us. 
4.	Then type whatever you want for the name (Baklava), description (softly crunchy dessert), and recipe link (Or leave it as “string” if you so desire), just make sure the only thing you are changing stays between the quotation marks, or the application won’t accept it. JSON is very picky.  Once you’ve given your recipe, or baklava, some information, hit execute at the bottom of the text box and you should get a 200. 
5.	(optional) You can see the new data you put in by going back to the top GET request in recipes, hit execute again and it should show the recipe you just created.

PUT REQUESTS

The orange PUT requests update information.  The only differences between the PUT and the POST are that you are only required the main item’s ID, so assigning a different owner or category is optional. So, if you update a recipe, you need the recipeID of the recipe you are updating. Also, in the request body, instead of deleting the ID row, put the same recipeId in the quotations as you put in the *required* box. Otherwise, it works the same as POST.

DELETE REQUESTS

For this one, all you need is the Id of the item you want to delete. So if we wanted to delete our owner Gracie R, scroll to the owner section of the screen, 
1.	Hit the drop down bar on the DELETE request, 
2.	Hit “try it out”
3.	Enter Id: 5
4.	Hit execute and wait for a 200 response.
5.	(optional) See the GET owners at the top to notice that Gracie R is no longer an option in the list!
   
Those are all the manipulations of data currently available to be performed, and they work very similarly across the whole API. I encourage you to play around. I don’t have any genuine data in my database for my own purposes yet, so if you want to throw in a recipe for disaster that’s fine by me! 😊
Thanks for taking the time to view my work.
