using API.DtoHandlers;
using System.Net;
using Database.Data.Interfaces;

using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace API.EndPoints
{
    public static class ApiExtensions
    {
        public static IEndpointRouteBuilder MusicApiExtensions(this IEndpointRouteBuilder musicApi)
        {
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
    }
}
