using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private static HttpClient _client;
        private LoggedInUser _user;
        

        public UserHandler(HttpClient client, LoggedInUser user)
        {
            _client = client;
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
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
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
