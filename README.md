# Musik API

## Introduction
This project features a RESTful web API and a console client application built with C# and Entity Framework. The API allows external services to fetch and modify data, utilizing both a local database and an external API.

## Features
* **Code-First Database Creation:** The project includes a program responsible for creating the database in a code-first approach.
* **RESTful API:** The API is designed following REST principles, allowing external services and applications to seamlessly interact with and manipulate data.
* **External API Integration:** Information is not only sourced from a local database but also fetched from an external API, showcasing the ability to integrate external data sources.
* **Client Console Application:** A console-based client program is implemented, offering a user-friendly interface to interact with the API and perform various operations.

## Technologies Used
* **C#:** The primary programming language used for both the API and client application.
* **Entity Framework (EF):** Employed for code-first database creation and management.

## Requirements:
* Microsoft Visual Studio
* Microsoft SQL Server
* SQL Server Management Studio
* Insomnia

## Getting Started
1. **Clone the Repository:**
	* https://github.com/ChasAlex/MusikApi.git
2. **Database Setup:**
	* Create a new SQL-database
	* Copy your database connectionstring and change to yours in appsettings.json "MusicDbCon".
	* Update-Database in Package Manager Console.
3. **Run the API:**
	* Launch the API program to start the server.
4. **Run the Client:** 
	* Execute the client program to interact with the API.

## Usage

### Using the API

#### Making JSON Posts

**Example test case 1: user signup**
```
HTTP request: POST /signup
Content-Type: application/json
```
```
{
  "fullname": "Erik Eriksson",
  "username": "erikaste",
  "password": "123456"
}
```

Expected response if created:
```
HTTP response: 201 Created
```

Expected response if username already exists:
```
HTTP response: 409 Conflict
```

**Example test case 2: user login**
```
HTTP request: POST /login
Content-Type: application/json
```
```
{
  "username": "erikaste",
  "password": "123456"
}
```

Expected response if credentials are correct:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
[
	{
		"id": 1001,
		"fullname": "Erik Eriksson"
	}
]
```

Expected response if credentials are not correct:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
Null
```

**Example test case 3: Add user-artist connection**
```
HTTP request: POST /userartist
Content-Type: application/json
```
```

{
	"userid": 1001,
	"artistid": 1002
}

```

Expected response:
```
HTTP response: 201 Created
```

**Example test case 4: Add user-song connection**
```
HTTP request: POST /usersong
Content-Type: application/json
```
```
{
	"userid": 1001,
	"songid": 1002
}
```

Expected response:
```
HTTP response: 201 Created
```

**Example test case 5: Add user-genre connection**
```
HTTP request: POST /usergenre
Content-Type: application/json
```
```
{
	"userid": 1001,
	"genreid": 1002
}
```

Expected response:
```
HTTP response: 201 Created
```

#### Retrieving Data
**Example test case 1: list all users**
```
HTTP request: GET /api/users
```

Expected response if there are users registered:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
[
	{
		"id": 1001,
		"fullname": "Erik Eriksson"
	}
]
```

**Example test case 2: list all users favorited artists**
```
HTTP request: GET /api/artists/1001
```

Expected response if user have favorited artists:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
[
	{
		"id": 1001,
		"name": "Anarchic Avocado",
		"description": "Caotic music where the screamers never know the lyrics, so it will always be a unique experience, but it's probably about avocados or something?",
		"country": "United Kingdom"
	},
	{
		"id": 1002,
		"name": "Binary Bloopers",
		"description": "Meet Binary Bloopers, the techno trio that accidentally turned their computer errors into infectious beats. They're the only band that programs their synthesizers with typos and considers glitches a part of the groove. Warning: May cause unexpected dance moves and spontaneous binary confetti showers",
		"country": "Sweden"
	}
]
```

Expected response if user does not have favorited artists:
```
HTTP response: 404 Not Found
```

**Example test case 3: list all users favorited songs**
```
HTTP request: GET /api/songs/1001
```

Expected response if user have favorited songs:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
[
	{
		"id": 1001,
		"name": "Smashed Guacamole"
	},
	{
		"id": 1002,
		"name": "404 Funk: When the Beat Can't Be Found"
	}
]
```

Expected response if user does not have favorited songs:
```
HTTP response: 404 Not Found
```

**Example test case 4: list all users favorited genres**
```
HTTP request: GET /api/genres/1001
```

Expected response if user have favorited genres:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
	{
		"id": 1001,
		"title": "Punk"
	},
		{
		"id": 1002,
		"title": "Techno"
	}
```

Expected response if user does not have favorited genres:
```
HTTP response: 404 Not Found
```

**Example test case 5: list all artists not favorited by user**
```
HTTP request: GET api/artists/notconnected/1001
```

Expected response if user has artists not (yet) favorited:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
[
	{
		"id": 1003,
		"name": "Rhyme Rascals",
		"description": "These lyrical tricksters drop rhymes faster than a magician pulls rabbits from a hat. Their mic skills are so sharp, they've been mistaken for word ninjas",
		"country": "USA"
	},
	{
		"id": 1004,
		"name": "Nebula Nomads",
		"description": "Embark on a cosmic journey with the Nebula Nomads, the intergalactic explorers of psytrance. They're the only band known to have traded their tour bus for a spaceship, navigating through wormholes while dropping beats that resonate with extraterrestrial life. Warning: May induce trance states, alien encounters, and an uncontrollable urge to dance among the stars",
		"country": "Portugal"
	}
]
```

Expected response if user has already favorited all artists:
```
HTTP response: 404 Not Found
```

**Example test case 6: list all songs not favorited by user**
```
HTTP request: GET api/songs/notconnected/1001
```

Expected response if user has songs not (yet) favorited:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
[
	{
		"id": 1003,
		"name": "Mic Check, Chickens on Deck"
	},
	{
		"id": 1004,
		"name": "Quantum Quirk"
	}
]
```

Expected response if user has already favorited all songs:
```
HTTP response: 404 Not Found
```

**Example test case 7: list all genres not favorited by user**
```
HTTP request: GET api/genres/notconnected/1001
```

Expected response if user has genres not (yet) favorited:
```
HTTP response: 200 OK
Content-Type: application/json
```
```
[
	{
		"id": 1003,
		"title": "Hip Hop"
	},
	{
		"id": 1004,
		"title": "Psytrance"
	},
]
```

Expected response if user has already favorited all genres:
```
HTTP response: 404 Not Found
```

### Using the Client program
Info about the client program...

## Contributors
Students:
* https://github.com/andersonandreas
* https://github.com/ChasAlex
* https://github.com/mlvestlund

Teachers:
* https://github.com/christoffer-qlok
* https://github.com/reidar-qlok

## Acknowledgments
Thanks to the school Chas Academy for providing our education and to our teachers Christoffer and Reidar