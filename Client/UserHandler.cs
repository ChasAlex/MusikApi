using System.Net.Http.Json;
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
            int chosenArtistId = await GetArtistsNotConnectedToUser();

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
            Console.Write("Press Enter to return to menu");
            Console.ReadKey();
        }

        public async Task<int> GetArtistsNotConnectedToUser()
        {
            int userId = _user.Id;
            string artistNotConUserIdApiURL = $"http://localhost:5158/api/artists/notconnected/{userId}";
            HttpResponseMessage response = await _client.GetAsync(artistNotConUserIdApiURL);
            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<Artist> AllNonConnectedArtists = JsonSerializer.Deserialize<List<Artist>>(jsonResponse);
                List<string> artistNames = AllNonConnectedArtists.Select(artist => artist.Name).ToList();
                Menu menu = new Menu();
                int newFavArtistChoice = menu.ShowMenu(artistNames, "Artists you haven't listed as favourites yet:");
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
