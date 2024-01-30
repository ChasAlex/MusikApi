using API.DtoHandlers;
using API.Models;
using API.Models.DTOs.GetTopTracksByArtistDTO;
using API.Models.DTOs.GetTopTracksByGenreDTO;
using API.Models.ViewModels;
using API.Services;
using Database.Data.Interfaces;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.EndPoints
{

    public static class ApiExtensions
    {
        public static IEndpointRouteBuilder MusicApiExtensions(this IEndpointRouteBuilder musicApi)
        {
            //Gets all registered users
            musicApi.Map("api/users", async (IPersonRepo repo) =>
            {
                var users = await repo.GetAllUsersAsync();

                if (users == null || !users.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateUserDto(users));
            });

            //Login: Checks for matching credentials and retrieves connected users info
            musicApi.MapPost("/login", async (IPersonRepo repo, LoginCredential loginCredential) =>
            {
                var loggedInUser = await repo.GetUserByCredentialsAsync(loginCredential.Username, loginCredential.Password);
                return loggedInUser;
            });


            //Signup: Checks if username is free and if so creates a new user
            musicApi.MapPost("/signup", async (IPersonRepo repo, SignupInfo signupInfo) =>
            {
                string fullName = signupInfo.Fullname;
                string userName = signupInfo.Username;
                string password = signupInfo.Password;
                try
                {
                    await repo.CreateNewUserAsync(fullname, username, password);
                    return Results.StatusCode((int)HttpStatusCode.Created);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Conflict during user signup: {ex.Message}");
                    return Results.StatusCode((int)HttpStatusCode.Conflict);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during user signup: {ex.Message}");
                    return Results.StatusCode((int)HttpStatusCode.InternalServerError);
                }
            });

            //Gets all users favorited artists
            musicApi.Map("api/artists/{id}", async (IPersonRepo repo, int id) =>
            {
                var artists = await repo.GetAllArtistsByUserIdAsync(id);

                if (artists == null || !artists.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateArtistDto(artists));
            });

            //Gets all users favorited songs
            musicApi.Map("api/songs/{id}", async (IPersonRepo repo, int id) =>
            {
                var songs = await repo.GetAllSongsByUserIdAsync(id);

                if (songs == null || !songs.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateSongDto(songs));
            });

            //Gets all users favorited genres
            musicApi.Map("api/genres/{id}", async (IPersonRepo repo, int id) =>
            {
                var genres = await repo.GetAllGenresByUserIdAsync(id);

                if (genres == null || !genres.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateGenreDto(genres));
            });

            //Gets all artists not favorited by user (yet)
            musicApi.Map("api/artists/notconnected/{id}", async (IPersonRepo repo, int id) =>
            {
                var artists = await repo.GetAllArtistsNotConnectedByUserIdAsync(id);

                if (artists == null || !artists.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateArtistDto(artists));
            });

            //Gets all songs not favorited by user (yet)
            musicApi.Map("api/songs/notconnected/{id}", async (IPersonRepo repo, int id) =>
            {
                var songs = await repo.GetAllSongsNotConnectedByUserIdAsync(id);

                if (songs == null || !songs.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateSongDto(songs));
            });

            //Gets all genres not favorited by user (yet)
            musicApi.Map("api/genres/notconnected/{id}", async (IPersonRepo repo, int id) =>
            {
                var genres = await repo.GetAllGenresNotConnectedByUserIdAsync(id);

                if (genres == null || !genres.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateGenreDto(genres));
            });

            //Adds new connection between user and artist
            musicApi.Map("/userartist", async (IPersonRepo repo, UserArtist userArtist) =>
            {
                await repo.AddUserArtistAsync(userArtist);
                return Results.StatusCode((int)HttpStatusCode.Created);
            });


            //Adds new connection between user and genre
            musicApi.Map("/usergenre", async (IPersonRepo repo, UserGenre userGenre) =>
            {
                await repo.AddUserGenreAsync(userGenre);
                return Results.StatusCode((int)HttpStatusCode.Created);
            });


            //Adds new connection between user and song
            musicApi.Map("/usersong", async (IPersonRepo repo, UserSong userSong) =>
            {
                await repo.AddUserSongAsync(userSong);
                return Results.StatusCode((int)HttpStatusCode.Created);
            });

            musicApi.Map("/addartist", async (IPersonRepo repo,ArtistAddInfo artist) =>
            {

                var artistToadd = await repo.AddArtistbyNameAsync(artist.Name);

                UserArtist newArist = new UserArtist() {
                    ArtistId = artistToadd.Id,
                    UserId = artist.Id                               
                };

                await repo.AddUserArtistAsync(newArist);
                return Results.StatusCode((int)HttpStatusCode.OK);


            });



            return musicApi;
        }

        public static IEndpointRouteBuilder ExternalApiMusic(this IEndpointRouteBuilder musicApi)
        {
            //Gets top songs for a chosen artist
            musicApi.MapGet("/artist/{artist}", async (string artist, [FromServices] IMusicServices musicServices) =>
            {

                Toptracks tracks = await musicServices.GetTopTrackByArtistAsync(artist);

                GetTopTrackByArtistViewmodel[]? result = tracks?.Track?.Select(track => new GetTopTrackByArtistViewmodel
                {
                    Name = track.Name,
                    Playcount = track.Playcount,
                }).ToArray();

                return Results.Json(result);

            });

            //Gets top songs for a genre/tag
            musicApi.MapGet("/genre/{genre}", async (string genre, [FromServices] IMusicServices musicServices) =>
            {

                Tracks tracks = await musicServices.GetTopTracksByGenreAsync(genre);

                GetTopTracksByGenreViewmodel[]? result = tracks?.Track?.Select(track => new GetTopTracksByGenreViewmodel
                {
                    Name = track.Name

                }).ToArray();

                return Results.Json(result);

            });

            //Get bio, name and playcount for an artist
            musicApi.MapGet("/artistinfo/{artist}", async (string artist, [FromServices] IMusicServices musicServices) =>
            {

                Models.DTOs.GetArtistInfoDTO.Artist artist1 = await musicServices.GetInfoArtistAsync(artist);

                GetInfoArtistViewmodel result = new GetInfoArtistViewmodel
                {
                    Name = artist1.Name,
                    Playcount = artist1.Stats.Playcount,
                    Summary = artist1.Bio.Summary
                };
                return Results.Json(result);

            });


            //Lägger till en Artist till en användare via apiet

            

            return musicApi;
        }
    }
}
