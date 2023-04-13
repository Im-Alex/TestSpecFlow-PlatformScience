# TestSpecFlow

The first thing that I did was to start the Docker service, it was really complicated because I don't have much experience, and when I finally could I got an error page at the beginning, that worried me because I had followed all the instructions, after investigating and asking, the second thing I did was to make my tests in Postman to see how the data was sent and received, also to check the example that Postman gives to make the connection in C#.
Once all that was done, I created this project using SpecFlow.
The complications I had was the way to send the data.
The answers, but I was able to solve it by debugging and that is that for example "patches" was a list of list, and I just made it list, from there on everything was a little easy.
