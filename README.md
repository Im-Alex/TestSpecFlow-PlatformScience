# TestSpecFlow

The first thing that I did was to start the Docker service, it was really complicated because I don't have much experience, and when I finally could I got an error page at the beginning, that worried me because I had followed all the instructions, after investigating and asking, the second thing I did was to make my tests in Postman to see how the data was sent and received, also to check the example that Postman gives to make the connection in C#.
Once all that was done, I created this project using SpecFlow.
The complications I had was the way to send the data.
The answers, but I was able to solve it by debugging and that is that for example "patches" was a list of list, and I just made it list, from there on everything was a little easy.

![image](https://user-images.githubusercontent.com/48999697/231797215-45dcd2b3-30fe-46fb-b48d-cca26e3b56b8.png)


# Connection example


![image](https://user-images.githubusercontent.com/48999697/231797321-a3434802-ffa1-4671-83e5-969dbf024005.png)

# Test in Postman
![image](https://user-images.githubusercontent.com/48999697/231798332-3de2823f-92ea-4ab7-ab52-7c6e3a2936ba.png)
