using API.DtoHandlers;
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
    public class LoginCredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public static class ApiExtensions
    {
        public static IEndpointRouteBuilder MusicApiExtensions(this IEndpointRouteBuilder musicApi)
        {

            musicApi.MapPost("/login", async (IPersonRepo repo, LoginCredential loginCredential) =>
            {
                var loggedInUser = await repo.GetUserByCredentials(loginCredential.Username, loginCredential.Password);
                return loggedInUser;
            });


            musicApi.Map("api/users", async (IPersonRepo repo) =>
            {
                var users = await repo.GetAllUsers();

                if (users == null || !users.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateUserDto(users));
            });


            musicApi.Map("api/genres/{id}", async (IPersonRepo repo, int id) =>
            {
                var genres = await repo.GetAllGenresByPersonId(id);

                if (genres == null || !genres.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateGenreDto(genres));
            });


            musicApi.Map("api/artists/{id}", async (IPersonRepo repo, int id) =>
            {
                var artists = await repo.GetAllArtistsByPersonId(id);

                if (artists == null || !artists.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateArtistDto(artists));
            });


            musicApi.Map("api/artists/notconnected/{id}", async (IPersonRepo repo, int id) =>
            {
                var artists = await repo.GetAllArtistsNotConnectedByPersonId(id);

                if (artists == null || !artists.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateArtistDto(artists));
            });


            musicApi.Map("api/songs/{id}", async (IPersonRepo repo, int id) =>
            {
                var songs = await repo.GetAllSongsByPersonId(id);

                if (songs == null || !songs.Any())
                {
                    return Results.NotFound();
                }

                return Results.Ok(Handler.CreateSongDto(songs));
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
            return musicApi;
        }

        public static IEndpointRouteBuilder ExternalApiMusic(this IEndpointRouteBuilder musicApi)
        {
            //Hämtar top låtar från en specifik artist
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

            // Hämta toplåtar för en genre/Tag
            musicApi.MapGet("/genre/{genre}", async (string genre, [FromServices] IMusicServices musicServices) =>
            {

                Tracks tracks = await musicServices.GetTopTracksByGenreAsync(genre);

                GetTopTracksByGenreViewmodel[]? result = tracks?.Track?.Select(track => new GetTopTracksByGenreViewmodel
                {
                    Name = track.Name

                }).ToArray();

                return Results.Json(result);

            });

            //Hämta Bio,name och playcount for en artist
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

            return musicApi;
        }




    }
}
