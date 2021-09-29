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
I made a draft scheme of my application layout, this is just a draft, not necessarily the final version
![Image of my MVVM scheme](https://github.com/HowestDAE/project-JasperCaerels/blob/main/Scheme/Runescape_GE.png)

## Results
![Gif of result app](https://github.com/HowestDAE/project-JasperCaerels/blob/main/IMPORTANT/APPShowcase.gif)
I came to the conclusion that the Runescape G.E. API is far from perfect. After working for it for quite some time, the same occurrence came to me repeatedly, over-complex structure for simple tasks because of the illogically structured and online easily overloaded API (since users aren't their prime concern, this is logical). Sadly, while my application is completely functional, using 2 different endpoints for my online API requests, the high rate at which Gateway Time-outs when requesting data at a rapid rate, lessens the possible functionality. Everything I want my application to be doing can be done but the data isn't fixed, sometimes specific API calls will not be timed-out, other times the previously timed-out calls might now be the valid ones instead.

I have some example pictures for a clearer view of the imperfections and user-unfriendliness of the Runescape API (as a moderator of its developer company said himself)
![Image of Moderator](https://github.com/HowestDAE/project-JasperCaerels/blob/main/IMPORTANT/ModeratorProof.PNG)
![Image of GateWay time-out data](https://github.com/HowestDAE/project-JasperCaerels/blob/main/IMPORTANT/DataLoadingIn.PNG)

## Important image
![Image of Await response](https://github.com/HowestDAE/project-JasperCaerels/blob/main/IMPORTANT/AwaitResponse.PNG)

## How the app works
Now despite the flawed API, my app is completely functional with 3 buttons, 1 changing depending on where you are navigated, the other 2 always serving the same purpose.
The bottom left button takes you to your selected object (item when in the item page, category when in the category page) and sends you back when you're in the most nested ItemDetail page. If you came from a specific category it will bring you back there, if you came from the list of all available items you will be taken back there.

The bottom middle button brings you to all available categories, select one of choice and press on the, as mentioned above, bottom left button to see all the items in that category.

The bottom right button will take you to the list of all available items in the app, uncategorized.

The items have a corresponding picture left to their name in a listbox and a symbol image to their right (using converters), the symbol represents if their price change over the past month was positive (green arrow up), negative (red arrow down), or neutral (beige rectangle).
![Image of Listbox items](https://github.com/HowestDAE/project-JasperCaerels/blob/main/IMPORTANT/ExplanationItemPrices.PNG)

### Item detail
In the item detail page more details are shown of the item, a bigger version of its image, name on the top right, its current price at the left bottom, the price change over the past month in percentage with the corresponding icon on that change. 
![Image of Item in detail](https://github.com/HowestDAE/project-JasperCaerels/blob/main/IMPORTANT/ItemDetail.PNG)

## Switching between local and online API
An interface has been implemented to easily switch between local/online, simply switch the private field used in the Instance property to either _localInstance or _instance. 
![Image of Changing local/online repository](https://github.com/HowestDAE/project-JasperCaerels/blob/main/IMPORTANT/SwitchOnlineOffline.PNG)

I would personally not advise anyone to work with the Runescape API when they are just exploring XAML, JSON, JSON APIs, MVVM etc. while it's a dream to work at the company, I did not find it worth all the needlessly complex issues and stress, nonetheless it was an extremely educational project and I am proud of my results!

Till next time fellow adventurer,
Jasper Caerels
