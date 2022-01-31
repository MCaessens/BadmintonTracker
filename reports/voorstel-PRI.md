# Projectvoorstel PRI

## Wat is de Badminton Tracker?
De Badminton Tracker helpt je bij het ontdekken van patronen in de factors die soms een rol spelen bij het winnen of verliezen van een match.
De Badminton Tracker kan je niet alleen gebruiken om de score bij te houden maar het houdt ook je rackets, shuttlecocks en speellocaties bij.

## Hoe werkt het?
Eenmaal je je als gebruiker hebt aangemeld, zie je een lijst van al je games. Deze is uiteraard bij de start leeg. Voor je een game kan toevoegen zal er gevraagd worden om eerst een racket, shuttlecock en een locatie toe tevoegen. Eenmaal je van deze van elk minstens 1 hebt toegevoegd kan je verder naar je game.

Bij het maken van deze dingen zal er telkens gevraagd een foto toetevoegen die van je apparaat zelf afkomt of een foto die je zelf neemt.
Dit is dus voor zowel het racket, shuttlecock en de locatie. Bij de locatie zal ook de locatie van je apparaat op dat moment opgehaald worden.

Voor een game aan te maken zul je ook nog de naam van je tegenstander in moeten vullen. Zo kan je later dan zien tegen wie je gespeeld hebt. Eenmaal de game aangemaakt is zal je ook direct naar het "detail" scherm van deze zojuist aangemaakte game gaan. Op dit scherm zul je al de informatie kunnen zien die je zojuist hebt ingevuld, maar je ziet dan ook een score beginnend op 0 vs 0 uiteraard. Boven elke score staat een plus knop. Door op deze knop te drukken zal je dus de score van jouw of je tegenstander kunnen verhogen. Eenmaal de score van 1 van de personen het maximum bereikt (typisch is dit 21) zal de game gesloten worden en wordt je terug naar de lijst met games gestuurd.

Eenmaal je terug bent in het start scherm zul je zien dat er bij het "kaartje" van je zojuist aangemaakte en beëindigde game, een status genaamd **Finished** staat. Moest je nu weggegaan zijn van het detail scherm voor de game beëindigd werd zal je de status **In Progress** zien. Zo kan je zien welke games al klaar zijn en welke games je nog verder moet spelen.

Naast deze schermen is er ook een scherm die aantoont met welk racket, shuttlecock en op welke locatie je het vaakst wint.
Door deze info te zien kan je zelf een soort patroon in je winsten bepalen. Dit zijn natuurlijk niet de belangrijkste factoren maar
het kan altijd helpen.

## Hoe wordt het project gerealiseerd?
Dit project zal worden gemaakt aan de hand van een web api en vue.js. 

## Gebruik van Externe API
Als externe API zal ik gebruik maken van de bwfshuttleapi (https://www.bwfshuttleapi.com), deze api zal ik gebruiken om de ranked players te laten zien van elke category in de competitie.

## De endpoints:
### Games
HttpRequest Type | Endpoint
---------------- | --------
GET | api/games
GET | api/games/id
POST | api/games
PUT | api/games
DEL | api/games/id

### Rackets
HttpRequest Type | Endpoint Rackets
---------------- | --------
GET | api/rackets
GET | api/rackets/id
GET | api/rackets/id/games
POST | api/rackets
PUT | api/rackets
DEL | api/rackets/id

### Shuttlecocks
HttpRequest Type | Endpoint Shuttlecocks
---------------- | --------
GET | api/shuttlecocks
GET | api/shuttlecocks/id
GET | api/shuttlecocks/id/games
POST | api/shuttlecocks
PUT | api/shuttlecocks
DEL | api/shuttlecocks/id

### Locations
HttpRequest Type | Endpoint Locations
---------------- | --------
GET | api/locations
GET | api/locations/id
GET | api/locations/id/games
POST | api/locations
PUT | api/locations
DEL | api/locations/id

### Users
HttpRequest Type | Endpoint Users
---------------- | --------
GET | api/users
GET | api/users/id
GET | api/users/id/games
GET | api/users/id/rackets
GET | api/users/id/shuttlecocks
GET | api/users/id/locations
POST | api/users
PUT | api/users
DEL | api/users/id



