# PokemonPals

**Pokemon Pals** is a lot like [other Pokedex Tracker sites](https://pokedextracker.com/): users can create an account and keep track of the virtual monsters they’ve acquired in Pokemon Let’s Go, a video game for the Nintendo Switch. It serves as a companion app where you can record members of your collection manually, selecting the Pokemon’s species and inputting their nickname, level, and various other details. The Pokemon is then marked as “captured” and added to your collection. Pokemon marked as "Favorites" or "Open to Trade" can be shown on your Profile to other users. You can then send Trade Requests to other users, which can be carried out in-game.

# Using the App

Go to [the Azure-hosted Pokemon Pals site](https://pokemonpals.azurewebsites.net/) to use the app! You'll want to [Register](https://pokemonpals.azurewebsites.net/Identity/Account/Register) an account before you explore the app further. 

If you'd rather peruse an already made account, login with the username **dyl_byl** and the password **Admin1!**

***Important Note***: This app is for demo purposes only. It's not completely fleshed out, and does contain a few bugs. For this reason, please refrain from sending ANY trade requests, even for testing!

## Dex
![Dex view](https://github.com/dylbyl/PokemonPals/blob/master/images/Dex.png?raw=true)
[On the Dex page, you'll be shown all of the original 151 Pokemon](https://pokemonpals.azurewebsites.net/Pokemon/Dex), in ascending order based on their Pokedex number. You can change the sort order of this view by clicking "Number" or "Name" at the top of the page. You can also search for a Pokemon by typing in its species name and clicking "Search". Once you've found the Pokemon you'd like to add to your collection, click the Pokeball icon on its card. You'll be able to give it a nickname, record it's level and CP, give it a comment, or mark it as a favorite/Open to Trade.

![Dex Details view](https://github.com/dylbyl/PokemonPals/blob/master/images/Details1.png?raw=true)
![Dex Collection view](https://github.com/dylbyl/PokemonPals/blob/master/images/Details2.png?raw=true)
Clicking the Pokemon's name or icon brings you to [a Details view for that species](https://pokemonpals.azurewebsites.net/Pokemon/Details/3). Here, you can see that Pokemon's official art, types, and stats. The bottom of the page will display all of the Pokemon you've caught of that species, if you have any.

## Collection
![Collection view](https://github.com/dylbyl/PokemonPals/blob/master/images/Collection.png?raw=true)
![Filtered Collection view](https://github.com/dylbyl/PokemonPals/blob/master/images/CollectionFiltered.png?raw=true)
[The Collection page shows every Pokemon you've caught and recorded on Pokemon Pals](https://pokemonpals.azurewebsites.net/CaughtPokemon/Collection). You can sort the list based on Pokedex number, name, level, or CP. You can filter the Collection to show only Pokemon you have/haven't nicknamed, set as a favorite, marked Open for Trade, or added a Comment to. You can also search by Pokemon species name, nickname, comment, or type. When searching by type, you must spell the type completely -- it doesn't work with partial matches!

Clicking on a Pokemon's name or image here lets you Edit the details for that specific Pokemon. You can change all the info you input when you first caught the Pokemon, but you can also change its species! This is helpful for when a Pokemon evolves in your game, or if you simply chose the wrong species when recording its info. A link at the bottom of the Edit page also allows you to delete the Pokemon from your collection.

## Profiles
![User Profile](https://github.com/dylbyl/PokemonPals/blob/master/images/Profile.png?raw=true)
![Editing your Profile](https://github.com/dylbyl/PokemonPals/blob/master/images/ProfileEdit.png?raw=true)
![Searching for a User](https://github.com/dylbyl/PokemonPals/blob/master/images/UserSearch.png?raw=true)
On your profile page, you can show off your favorite Pokemon, rare Pokemon you’d be willing to trade to other players, your Discord username, your Nintendo Switch Friend Code, and some editable personal info. [You can search for a friend](https://pokemonpals.azurewebsites.net/Users/UserSearch) to look at their favorites and trade showcase as well.

## Trade Requests
![Trade Requests page](https://github.com/dylbyl/PokemonPals/blob/master/images/Requests.png?raw=true)
![Sending a Trade Request](https://github.com/dylbyl/PokemonPals/blob/master/images/SendRequest.png?raw=true)
When looking at a user's profile, you can click a button to request any Pokemon a user has put up for trade. You'll then be able to offer a Pokemon of your own in exchange, then attach a message. You can also search all tradeable Pokemon to find a specific species that you'd like! [On the Trade Requests page](https://pokemonpals.azurewebsites.net/Trades), you can also view all of your incoming and outgoing trade requests. In the future, you'll be able to comment on, edit, delete, and accept/reject trade requests. As for now, I suggest asking the user to add you on Discord and discussing the details of the trade there!

That's all for the instructions! Have fun with the app, and good luck catching (or trading) 'em all!
