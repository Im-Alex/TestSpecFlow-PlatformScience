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

