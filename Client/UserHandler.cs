using Client.Models.Viewmodels;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client
{
    public class UserHandler : IUserHandler
    {
        private HttpClient _client;
        private User _user;

        public UserHandler(User user)
        {
            _client = new HttpClient();
            _user = user;
        }

        //Gets users favorite artists, and retrieves more info about chosen artist
        public async Task ArtistByIdAsync()
        {
            int userId = _user.Id;
            string artistUserIdApiURL = $"http://localhost:5158/api/artists/{userId}";
            HttpResponseMessage response = await _client.GetAsync(artistUserIdApiURL);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Artist> favouriteArtists = JsonSerializer.Deserialize<List<Artist>>(jsonResponse);
                List<string> artistNames = favouriteArtists.Select(artist => artist.Name).ToList();

                int IndexChosenArtist = Menu.ShowMenu(artistNames, "Favorited artists");
                string chosenArtist = artistNames[IndexChosenArtist];

                Artist chosenArtistInfo = favouriteArtists.FirstOrDefault(artist => artist.Name == chosenArtist);
                Console.WriteLine("Favorite artist details\n");
                await Console.Out.WriteLineAsync($"Name: {chosenArtistInfo.Name}\nDescription: {chosenArtistInfo.Description}\nCountry: {chosenArtistInfo.Country}");
            }
            else
            {
                await Console.Out.WriteLineAsync("No favorite artists registered.");
            }
            Console.Write("\nPress Enter to return to menu");
            Console.ReadKey();
        }

        //Gets users favorite songs
        public async Task SongByIdAsync()
        {
            int userId = _user.Id;
            string songUserIdApiURL = $"http://localhost:5158/api/songs/{userId}";
            HttpResponseMessage response = await _client.GetAsync(songUserIdApiURL);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Song> favoriteSongs = JsonSerializer.Deserialize<List<Song>>(jsonResponse);

                Console.WriteLine("Favorite Songs\n");
                foreach (var song in favoriteSongs)
                {
                    await Console.Out.WriteLineAsync($"{song.Name}");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("No favorite songs registered.");
            }
            Console.Write("\nPress Enter to return to menu");
            Console.ReadKey();
        }

        //Gets users favorite genres
        public async Task GenresByIdAsync()
        {
            int userId = _user.Id;
            string genreUserIdApiURL = $"http://localhost:5158/api/genres/{userId}";
            HttpResponseMessage response = await _client.GetAsync(genreUserIdApiURL);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Genre> favouriteGenres = JsonSerializer.Deserialize<List<Genre>>(jsonResponse);
                Console.WriteLine("Favourite Genres\n");
                foreach (var genre in favouriteGenres)
                {
                    await Console.Out.WriteLineAsync($"{genre.Title}");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("No favorite genres registered.");
            }
            Console.Write("\nPress Enter to return to menu");
            Console.ReadKey();
        }

        //Gets all artists not (yet) connected as favorite by user
        public async Task<int> GetArtistsNotConnectedToUserAsync()
        {
            int userId = _user.Id;
            string artistNotConUserIdApiURL = $"http://localhost:5158/api/artists/notconnected/{userId}";
            HttpResponseMessage response = await _client.GetAsync(artistNotConUserIdApiURL);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Artist> AllNonConnectedArtists = JsonSerializer.Deserialize<List<Artist>>(jsonResponse);
                List<string> artistNames = AllNonConnectedArtists.Select(artist => artist.Name).ToList();

                int newFavArtistChoice = Menu.ShowMenu(artistNames, "Artists you haven't listed as favourites yet:");
                string newFavArtistName = artistNames[newFavArtistChoice];
                Artist newFavArtist = AllNonConnectedArtists.FirstOrDefault(artist => artist.Name == newFavArtistName);
                return newFavArtist.Id;
            }
            else
            {
                await Console.Out.WriteLineAsync("Something went wrong.");
                return 0;
            }
        }

        //Gets all songs not (yet) connected as favorite by user
        public async Task<int> GetSongsNotConnectedToUserAsync()
        {
            int userId = _user.Id;
            string songsNotConUserIdApiURL = $"http://localhost:5158/api/songs/notconnected/{userId}";
            HttpResponseMessage response = await _client.GetAsync(songsNotConUserIdApiURL);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Song> AllNonConnectedSongs = JsonSerializer.Deserialize<List<Song>>(jsonResponse);
                List<string> songNames = AllNonConnectedSongs.Select(song => song.Name).ToList();

                int newFavSongChoice = Menu.ShowMenu(songNames, "Songs you haven't listed as favourites yet:");
                string newFavSongName = songNames[newFavSongChoice];
                Song newFavSong = AllNonConnectedSongs.FirstOrDefault(song => song.Name == newFavSongName);
                return newFavSong.Id;
            }
            else
            {
                await Console.Out.WriteLineAsync("Something went wrong.");
                return 0;
            }
        }

        //Gets all genres not (yet) connected as favorite by user
        public async Task<int> GetGenresNotConnectedToUserAsync()
        {
            int userId = _user.Id;
            string genreNotConUserIdApiURL = $"http://localhost:5158/api/genres/notconnected/{userId}";
            HttpResponseMessage response = await _client.GetAsync(genreNotConUserIdApiURL);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Genre> AllNonConnectedGenres = JsonSerializer.Deserialize<List<Genre>>(jsonResponse);
                List<string> genreTitles = AllNonConnectedGenres.Select(genre => genre.Title).ToList();

                int newFavGenreChoice = Menu.ShowMenu(genreTitles, "Genres you haven't listed as favourites yet:");
                string newFavGenreTitle = genreTitles[newFavGenreChoice];
                Genre newFavGenre = AllNonConnectedGenres.FirstOrDefault(genre => genre.Title == newFavGenreTitle);
                return newFavGenre.Id;
            }
            else
            {
                await Console.Out.WriteLineAsync("Something went wrong.");
                return 0;
            }
        }

        //Creates connection between user and artist
        public async Task ConnectUserToArtistAsync()
        {
            int chosenArtistId = await GetArtistsNotConnectedToUserAsync();

            string userartistApiURL = "http://localhost:5158/userartist";

            var userArtist = new
            {
                UserId = _user.Id,
                ArtistId = chosenArtistId
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync(userartistApiURL, userArtist);
            if (response.IsSuccessStatusCode)
            {
                if (chosenArtistId != 0)
                {
                    await Console.Out.WriteLineAsync("Success!");
                }
                else
                {
                    await Console.Out.WriteLineAsync("No artists found.");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync($"Failed response code: {response.StatusCode} {response.ReasonPhrase}");
            }
            Console.Write("\nPress Enter to return to menu");
            Console.ReadKey();
        }

        //Creates connection between user and song
        public async Task ConnectUserToSongAsync()
        {
            int chosenSongId = await GetSongsNotConnectedToUserAsync();

            string userSongApiURL = "http://localhost:5158/usersong";

            var userSong = new
            {
                UserId = _user.Id,
                SongId = chosenSongId
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync(userSongApiURL, userSong);
            if (response.IsSuccessStatusCode)
            {
                if (chosenSongId != 0)
                {
                    await Console.Out.WriteLineAsync("Success!");
                }
                else
                {
                    await Console.Out.WriteLineAsync("No songs found.");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync($"Failed response code: {response.StatusCode} {response.ReasonPhrase}");
            }
            Console.Write("\nPress Enter to return to menu");
            Console.ReadKey();
        }

        //Creates connection between user and genre
        public async Task ConnectUserToGenreAsync()
        {
            int chosenGenreId = await GetGenresNotConnectedToUserAsync();

            string userGenreApiURL = "http://localhost:5158/usergenre";

            var userGenre = new
            {
                UserId = _user.Id,
                GenreId = chosenGenreId
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync(userGenreApiURL, userGenre);
            if (response.IsSuccessStatusCode)
            {
                if (chosenGenreId != 0)
                {
                    await Console.Out.WriteLineAsync("Success!");
                }
                else
                {
                    await Console.Out.WriteLineAsync("No genres found.");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync($"Failed response code: {response.StatusCode} {response.ReasonPhrase}");
            }
            Console.Write("\nPress Enter to return to menu");
            Console.ReadKey();
        }

        //Gets info of chosen artist with external api
        public async Task GetInfoFromAristAsync()
        {
            try
            {
                Console.Write("Search for an Artist: ");
                string artist = Console.ReadLine();
                string apiUrlinfo = $"http://localhost:5158/artistinfo/{artist}";
                string apiAddUrl = "http://localhost:5158/addartist";
                string UserToArtistUrl = "";

                HttpResponseMessage response = await _client.GetAsync(apiUrlinfo);

                if (response.IsSuccessStatusCode)
                {
                    GetInfoFromArtistViewModel info = JsonSerializer.Deserialize<GetInfoFromArtistViewModel>(await response.Content.ReadAsStringAsync());
                    Console.WriteLine($"Name: {info.Name}");
                    Console.WriteLine($"Playcount: {info.Playcount}");
                    Console.WriteLine($"Bio: {info.Summary}");
                    Console.ReadLine();

                    List<string> responds_options = new List<string>() { "Yes", "No" };

                    int answer = Menu.ShowMenu(responds_options, "Would you like to save this Artist?");
                    if (answer == 0)
                    {

                        HttpResponseMessage response1 = await _client.PostAsJsonAsync(apiAddUrl, new
                        {
                            Name = info.Name,
                            Id = _user.Id
                        });
                    }

                    Console.ReadLine();



                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine($"Exception: {ex.Message}");
            }
        }


        public async Task GetTopSongsGenreAsync()
        {
            try
            {
                Console.Write("Search for an Genre: ");
                string genre = Console.ReadLine();
                string apiUrlinfo = $"http://localhost:5158/genre/{genre}";
                

                HttpResponseMessage response = await _client.GetAsync(apiUrlinfo);

                if (response.IsSuccessStatusCode)
                {
                    var info = JsonSerializer.Deserialize<GetTopTracksByGenreViewmodel[]>(await response.Content.ReadAsStringAsync());

                    Console.WriteLine();
                    for(int i = 0; i < 10; i++)
                    {
                        Console.WriteLine($"{info[i].Name}");
                    }

                    Console.ReadLine();



                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine($"Exception: {ex.Message}");
            }
        }


        public async Task GetTopSongsArtistAsync()
        {
            try
            {
                Console.Write("Search for an Artist: ");
                string artist = Console.ReadLine();
                string apiUrlinfo = $"http://localhost:5158/artist/{artist}";


                HttpResponseMessage response = await _client.GetAsync(apiUrlinfo);

                if (response.IsSuccessStatusCode)
                {
                    var info = JsonSerializer.Deserialize<GetTopTrackByArtistViewmodel[]>(await response.Content.ReadAsStringAsync());

                    Console.WriteLine();
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine($"Song: {info[i].Name}");
                        Console.WriteLine($"Plays: {info[i].Playcount}");
                    }

                    Console.ReadLine();



                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
