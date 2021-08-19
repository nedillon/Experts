### Solution Overview:
* This was written using Visual Studio 2019, targeting .NET Framework 4.8
* Data: The data layer. Handles reading and writing records from the "database"
* Data.Test: Unit tests for the Data project (incomplete)
* Shared: The business layer. Handles business logic throughout the solution
* Shared.Test: Unit tests for the Shared project
* UI: An ASP.NET MVC Web Site. Most all of the processing happens in Controllers.ModelsController

### Status:
I believe all of the requirements are at least functional. The UI is basic just as it comes with MVC so there could be a lot of improvement there. Unit tests are incomplete, but I added a few so I could easily debug and to give you an idea of where I was going with it. With more time, I would also implement more robust error handling (and logging).

### Project: 
Using .NET, we'd like you to create a simple experts directory search tool. The tool can either be
a full featured application or API only.
* Spend no more than 4 hours coding for the project. Do not include any initial application setup in this
time limit.
### Requirements:
The application should fulfill the following requirements:
* A member can be created using their name and a personal website address.
* When a member is created, all the heading (h1-h3) values are pulled in from the website to that
members profile.
* The website url is shortened (e.g. using http://goo.gl).
* After the member has been added, I can define their friendships with other existing members.
Friendships are bi-directional i.e. If David is a friend of Oliver, Oliver is always a friend of David as well.
* The interface should list all members with their name, short url and the number of friends.
* Viewing an actual member should display the name, website URL, shortening, website headings, and
links to their friends' pages.
* Now, looking at Alan's profile, I want to find experts in the application who write about a certain topic and
are not already friends of Alan.
* Results should show the path of introduction from Alan to the expert e.g. Alan wants to get introduced to
someone who writes about 'Dog breeding'. Claudia's website has a heading tag "Dog breeding in
Ukraine". Bart knows Alan and Claudia. An example search result would be Alan -> Bart -> Claudia ("Dog
breeding in Ukraine").
We encourage the use of any libraries for everything except the search functionality, in which we want to
see your simple algorithm approach.
### Add-ons:
* Sign up/log in functionality

* A UI that expands upon the basic requirements to have a user-friendly look and feel
* Anything else you, as a user, would enjoy seeing in an interface like this
### Things we're looking for:
* Navigable code
* Efficient algorithms
* Good separation of concerns
* Error handling
* Usage of gems/libraries
### Things we like:
* Well commented & well organized code
* Quality over quantity (the code you write should be good)
* Small, meaningful, commits
* Tests!
* Respect for the time limit - if you are in the midst of some work that you would like to finish, but have hit
the 4 hour time limit, please split additional work into a separate branch, to be evaluated separately
### Submission
* Remember to make meaningful commits as you work
* Somehow share your repository with us
* __Important:__ If there are credentials required (.env or master.key file), please email these to us
directly or we can't review your project
