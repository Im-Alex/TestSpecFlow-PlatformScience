Feature: Platform

A short summary of the feature

@tag1
Scenario: Cleans all patches of dirt

Given The room size is 5 by 5 
And the robot starts at x equals 1 and y equals 2
And there are patches of dirt at the following positions:
| X | Y |
| 1 | 0 |
| 2 | 2 |
| 1 | 3 |

When the robot moves according to the instruccions "NNESEESWNWW"
Then the final robot position should be at x equals 1 and y equals 3 
And the number of patches of dirt cleaned should be 1


@tag2
Scenario: Robot moves in a straight line and hits a wall

Given The room size is 5 by 5 
And the robot starts at x equals 0 and y equals 0
And there are patches of dirt at the following positions:
| X | Y |
| 1 | 1 |
When the robot moves according to the instruccions "EENNN"
Then the final robot position should be at x equals 4 and y equals 2
And the number of patches of dirt cleaned should be 0

@tag3
Scenario: Robot starts on a patch of dirt

Given The room size is 5 by 5 
And the robot starts at x equals 2 and y equals 2
And there are patches of dirt at the following positions:
| X | Y |
| 2 | 2 |
When the robot moves according to the instruccions "NNESEESWNWW"
Then the final robot position should be at x equals 1 and y equals 3
And the number of patches of dirt cleaned should be 2

@tag4
Scenario: Robot starts outside the room and hits a wall
Given The room size is 5 by 5 
And the robot starts at x equals 6 and y equals 6
And there are no patches of dirt in the room
When the robot moves according to the instruccions "WNSSEEN"
Then the final robot position should be at x equals 5 and y equals 4
And the number of patches of dirt cleaned should be 0
