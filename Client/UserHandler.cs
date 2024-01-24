using API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;



namespace Client
{

    interface IUserHandler
    {
        //public Login() { }
        //public AllaUsers() { }
        //public GenresByid(int id) { }
        //public ArtistByid(int id) { }
        //public SongByid(int id) { }
        public void ConnectUsertoArtist(int id,string artist) { }
        public void ConnectUsertoGenre(int id, string genre) { }
        public void ConnectUsertoSong(int id, string song) { }
        public void GetInfoFromAritst(string aritst) { }


        
    }

    public class UserHandler
    {
        private static HttpClient? _client;
        private LoggedInUser _user;
        private static GetInfoArtistViewmodel _infoArtistViewmodel = new GetInfoArtistViewmodel();
        

        public UserHandler(LoggedInUser user)
        {
            _client = new HttpClient();
            _user = user;
            

            
        }

        public static async Task GetInfoFromArist(string artist) 
        {
            try
            {
                string apiUrl = $"http://localhost:5158/artistinfo/{artist}";

                HttpResponseMessage response = await _client.GetAsync(apiUrl);
                
                if(response.IsSuccessStatusCode)
                {
                    _infoArtistViewmodel = JsonSerializer.Deserialize<GetInfoArtistViewmodel>(await response.Content.ReadAsStringAsync());
                    Console.WriteLine($"Name: {_infoArtistViewmodel.Name}");
                    Console.WriteLine($"Playcount: {_infoArtistViewmodel.Playcount}");
                    Console.WriteLine($"Bio: {_infoArtistViewmodel.Summary}");
                    
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
