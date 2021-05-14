# project-JasperCaerels
project-JasperCaerels created by GitHub Classroom

# The grand exchange, game design with economics inbetween!
## Introduction
Most MMORPGs have one same component, a player driven economy, Runescape is no different!
Since 2007-2008 their own personal system called the Grand Exchange was introduced.
The Grand Exchange is a place where players can come together to socialize,
manage their banks or trade items with fluctuating prices.

This project will recreate the G.E. (Grand Exchange) item API and portray a simpler version of it usable to check
prices of items, if it dropped or raised in price. Each item will be filtered through category and then once more with 1st letter of its name.

## Motivation
Besides the game aspect of this MMORPG, I'm also very interested in game driven economics, stocks and cryptocurrency.
In addition I've been playing Runescape since 2006 and aside from VR development, Jagex would be one of my dream companies to work at.
Even tho the work I'd want to manage there would be more C++ game engine based, it's still interesting to  work with their JSON API.


## File structure
![Image of my MVVM scheme](https://raw.githubusercontent.com/HowestDAE/project-JasperCaerels/main/Scheme/Runescape_GE.png?token=APE2NB4P7R63UFASBWF32KDAR4SZY)

## Results
I came to the conclusion that the Runescape G.E. API is far from perfect. After working for it for quite some time, the same occurrence came to me repeatedly, over-complex structure for simple tasks because of the illogically structured and online easily overloaded API. Sadly, while my application is completely functional, the high rate at which Gateway Time-outs when requesting data at a rapid rate, lessens the possible functionality. Everything I want my application to be doing can be done but the data isn't fixed, sometimes specific API calls will not be timed-out, other times the previously timed-out calls might now be the valid ones instead.

I have some example pictures for a clearer view of the imperfections and user-unfriendliness of the Runescape API (as a moderator of its developer company said himself)
![Image of Moderator](https://github.com/HowestDAE/project-JasperCaerels/blob/main/IMPORTANT/ModeratorProof.PNG)
![Image of GateWay time-out data]()
![Image of Await response]()
![Image of Changing local/online repository]()
