# PokemonPals

THIS README IS A WORK IN PROGRESS. Currently, it holds a description of the app, and details for my instructors/classmates on how to setup the app and test it.

Pokemon Pals is a lot like [other Pokedex Tracker sites](https://pokedextracker.com/): users can create an account and keep track of the virtual monsters they’ve acquired in Pokemon Let’s Go, a video game for the Nintendo Switch. It serves as a companion app where players input members of their collection manually, inputting the Pokemon’s species, nickname, and various other details. The Pokemon is then marked as “captured.” 

## Future Features

On a user’s profile page, they can show off their favorite Pokemon, rare Pokemon they’d be willing to trade to other players, and Pokemon they need to fill their collection. Users can view a friend’s page to look at their collection and favorites as well. Users will be able to send Trade Requests by selecting Pokemon on another user’s page. With these requests, users can either say “Hey, I need a Pokemon in your collection” or “Hey, I can give you a Pokemon you’re missing.”

On their profile, users can display an About Me, their Nintendo Switch Friend Code, their favorite Pokemon games, and choose which of their custom lists they’d like to display. Users can also select a profile picture from a predetermined set (this set is just a collection of every Starter Pokemon).

## Installing the App
1. Clone down this master branch into a new repository
2. Open the project's root folder in Visual Studio
3. Open Visual Studio's Package Manager Console
4. In the Package Manager Console, run the command `Add-Migration CreateDatabase`.
5. If the command from step 4 succeeds, run `Update-Database` to create the PokemonPals database on your machine.
6. Navigate to the project's `appsettings.json` file and adjust the SQLSERVER string to match the name of the server running on your machine.
7. Open Visual Studio's SQL Sever Object Explorer, find the PokemonPals database, right-click on it, and create a new SQL query.
8. In this query, copy the script contained in [AllPokemonSQLQuery.sql](https://github.com/dylbyl/PokemonPals/blob/master/AllPokemonSQLQuery.sql). Execute the query to add all Pokemon to your database. These entries were scraped from [PokeAPI](https://pokeapi.co/docs/v2). 
9. Delete all text from the query, returning you to a blank script.
10. Repeat steps 8 and 9, using the [AvatarQuery.sql](https://github.com/dylbyl/PokemonPals/blob/master/AvatarQuery.sql) and the [GameQuery.sql](https://github.com/dylbyl/PokemonPals/blob/master/GameQuery.sql).

# Running and Testing
1. At the top of your Visual Studio screen, click the Run button labeled "IIS Express." This will run the app in debugger mode.
2. Try to access the Dex or Collection tabs in the NavBar. You should be redirected to the login page instead.
3. Register an account with the app.

## Dex View
1. Navigate to the Dex page using the NavBar.
2. Here, you can click a Pokemon image or name to go to the Details view for that species. Details will show the name, Pokedex number, official art, types, and various stats for that species.
3. In the Dex view OR the Details view, clicking a Pokeball image will let you "catch" the associated Pokemon.
4. On the Catch/Create screen, you can input a Pokemon's nickname, level, CP, gender, date caught, whether it's a favorite, whether you'd like to trade it away, and any additional comments. Users will copy this info from their Nintendo Switch game. Note: Nickname and comment can be empty, but the rest cannot.
5. After "catching" a Pokemon, return to the Dex view. Its card should now be highlighted, and the progress bar and completion stat at the top should have incremented.
6. Navigate to the Details view for the Pokemon you just caught. At the bottom of the page, you should see a new card for the Pokemon you just caught, displaying all of its info.
7. Try "catching" several species of Pokemon.

## Collection View.
1. Navigate to the Collection page using the NavBar.
2. Here, you will see cards for every Pokemon you've caught, as well as info and comments for each one. You can easily view every Pokemon you own, without having them split by species.
3. Click a Pokemon's image or name to go to their Edit screen. Here, you can edit any info you entered on the Create screen. You can also change the Pokemon's species (handy if you accidentally entered the wrong species initially, or if the Pokemon evolves in-game).
4. Try Editing and Saving information for a Pokemon you've caught.
5. Navigate to the Edit screen for a Pokemon once again. At the bottom of the view, you'll see a link named "Remove from Collection." Click this to bring you to our delete view.
6. The Delete view will show you all the info for the Pokemon you're trying to delete, and ask for confirmation of removal. If the Pokemon was marked as a favorite, a large warning will also be displayed. Try deleting one of the precious Pokemon from your collection.
7. Return to the Collection page. Try using the sort links at the top of the page. Clicking a link once should apply the tag, while clicking the link a second time should reverse the result order (order by desc if the first click sorted by asc, show un-nicknamed Pokemon if the first click sorted by nickname, etc).
8. Try using the search feature on the Collection page as well. The search is NOT case sensitive, and will search within any Pokemon nickname or species name. You can also search by type, but to do so, you must spell the full type (not case sensitive, but it doesn't work by partially spelling the type).

At this point, you've seen all the current, main features of Pokemon Pals. Hooray! Now go try to break the darn thing.
