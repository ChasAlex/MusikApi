﻿using System.Net.Http.Json;
using System.Text.Json;
using Client.Models.Viewmodels;

namespace Client
{

    interface IUserHandler
    {
        //public Login() { }
        //public AllaUsers() { }
        //public GenresByid(int id) { }
        public void ArtistById(User loggedInUser) { }
        //public SongByid(int id) { }
        public void ConnectUserToArtist(User loggedInUser) { }
        public void ConnectUsertoGenre(int id, string genre) { }
        public void ConnectUsertoSong(int id, string song) { }
        public void GetinfoFromAritst(string aritst) { }


        
    }

    public class UserHandler
    {
        private HttpClient _client;
        private User _user;

        public UserHandler(User user)
        {
            _client = new HttpClient();
            _user = user;
        }
        public async Task ArtistById()
        {
            int userId = _user.Id;
            string artistUserIdApiURL = $"http://localhost:5158/api/artists/{userId}";
            HttpResponseMessage response = await _client.GetAsync(artistUserIdApiURL);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Artist> favouriteArtists = JsonSerializer.Deserialize<List<Artist>>(jsonResponse);
                List<string> artistNames = favouriteArtists.Select(artist => artist.Name).ToList();
                Menu menu = new Menu();
                int IndexChosenArtist = menu.ShowMenu(artistNames, "Favorited artists");
                string chosenArtist = artistNames[IndexChosenArtist];
                Artist chosenArtistInfo = favouriteArtists.FirstOrDefault(artist => artist.Name == chosenArtist);

                Console.WriteLine("Favorite artist details\n");
                await Console.Out.WriteLineAsync($"Name: {chosenArtistInfo.Name}\nDescription: {chosenArtistInfo.Description}\nCountry: {chosenArtistInfo.Country}\n");
            }
            else
            {
                await Console.Out.WriteLineAsync("No favorite artists registered.");
            }
            Console.Write("Press Enter to return to menu");
            Console.ReadKey();
        }
        public async Task ConnectUserToArtist()
        {
            //make an api that retrieves a list of all not connected to user
            //ask which artist the user wants to add to favourites, or let the user exit the method

            string userartistApiURL = "http://localhost:5158/userartist";
            Console.WriteLine("Artist Id:");
            int artistId = int.Parse(Console.ReadLine());

            var userArtist = new
            {
                UserId = _user.Id,
                ArtistId = artistId
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync(userartistApiURL, userArtist);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Connection to artist added successfully!");
                Console.Write("Press Enter to return to menu");
                Console.ReadKey();
            }
            else
            {
                await Console.Out.WriteLineAsync($"Failed response code: {response.StatusCode} {response.ReasonPhrase}");
                Console.Write("Press Enter to return to menu");
                Console.ReadKey();
            }
        }

        public async Task GetInfoFromArist()
        {
            try
            {
                Console.Write("Search for an Artist: ");
                string artist = Console.ReadLine();
                string apiUrl = $"http://localhost:5158/artistinfo/{artist}";
                
                HttpResponseMessage response = await _client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    GetInfoFromArtistViewModel info = JsonSerializer.Deserialize<GetInfoFromArtistViewModel>(await response.Content.ReadAsStringAsync());
                    Console.WriteLine($"Name: {info.Name}");
                    Console.WriteLine($"Playcount: {info.Playcount}");
                    Console.WriteLine($"Bio: {info.Summary}");
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
