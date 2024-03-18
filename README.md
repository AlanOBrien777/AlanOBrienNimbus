# AlanOBrienNimbus
Hi,
Welcome to my ReadMe for this Technical Test supplied to me by Super Nimbus.

Project Overview:
This project is my creation of a very simple "cookie clicker" game which has authentication implemented to enter said game using BrainCloud.

Key Features: 
1. Anonymous Authentication: User can log in as a guest which will assign them an anonymous project Id.
2. Universal Authentication: User can fill in a Username and Password field and Log in /sign up with these credentials. This will create a profile with the users name entered in the braincloud backend.
3. Logging out of Users: Users can Log out once in the game which allows them to save their score and to switch account.
4. Changing Username: User can change their username once logged in using universal authentication.
5. Score tracking: Simple score count to keep track of how many times the player presses the cookie button.
6. Backend Leaderboard: Once the player logs out at the end of a session their score will be added to a leaderboard generated in the braincloud backend as long as they beat their previous score.
7. Highscore notification: If the player beats the current highscore on the leaderbaord they will get a pop up telling them so.
8. Username Display: Just to inform the player who they are signed in as. This will change if the username is changed.

Project Demo:
To test the anonymous authentication you can simply press the "Guest Button" on screen once you start the game. You will then be brought to the game screen where you can press the cookie button to add 1 to your score. To save your score you must press the logout button. Keep in mind once you log out of an anonymous account you can not log back in. But your score will be added to the brain cloud backend.

To test the universal authentication you can enter a username and a password into the text fields and press log in. You will then be in the game page and can also click the cookie button same as mentioned above. you can also type a new user name in to the text field and press change username which will change your name in the front and backend. 

You can naviagte to the User Browser of the app in BrainCloud and see the new user appear with a random profile id. you can also go to global then to leaderboards and see the current scores on the leaderboard. 

To get the new highscore pop up you must beat the previous high score on the leadeboard.

Easy(in quotations):
Clearly the Ui and the game idea itself is not super advanced so it was generally easy to set it up using canvases. I also found integrating the braincloud sdk pretty easy also and navigating the braincloud dashboard. I found implementing authentication relatively easy in the begining as it was a series of following steps.

Difficult:
I found the braincloud api a little difficult to use as I felt its explanitions were too simplified and/or only explained how to do something for their own examples. I found myself having to follow their tutorials for their projects and then try and interpit the code for my own project. Once I realised it was just a series of callbacks it was easier to do. But even in their youtube bootcamp videos they suddenly jump to a random scrtipt they never mentioned and expect you know exactly what they mean. 

I also found cloud scripting implementation difficult as you really need to do alot of api searching to find the functions you need and then they are only usable in a particular proxy function which was not mentioned originally. But again after some trial and error I got there.

It also seems there is not alot of third party info on braincloud. Such as other peoples projects or random blog posts from other developers talking about working with it and such and I felt I could only get limited info and ideas from Braincloud directly. 

I struggled a bit with time management on this project as I am working a full time job with varying shift hours and work days which made it hard to plan around that and I also overestimated my ability to sit down and learn new technologies for 2 hours at 11pm after finishing an 8 hour shift. 


Skills Learned:
Before this I did not have much experiencing developing with back end as a service applications so I do feel I learned how to work with them as they clearly can take some of the load of by doing some of the work for you such as the leaderboard implementation.

I learned how cloud scripting can be used quite well to get some third party info and how it can be good for getting information without affecting the main code running. 

I also felt I went into my cookie clicker idea without thinking how I could integrate cloud scripting into it which I think made it more difficult after I already did so much and had to find a way to make it work. I think in the future I would take more time planning how I would incoporate key features into an idea without going too far with an idea. 

I certianly learned to manage my time better and how to motivate myself during my free time by simply giving myself smaller tasks to approach each step of the way.

Video Link: https://www.youtube.com/watch?v=njlarMx2fgk





