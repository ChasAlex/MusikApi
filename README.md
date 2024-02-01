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
HTTP response: 200 OK
Content-Type: application/json
```
```
[
	{
		201 Created
	}
]
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

### Using the Client program
Info about the client program...

## Testing
Info about the tests...

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