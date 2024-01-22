using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;

namespace Client
{
    public class LoggedInUser
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
    }
    internal class Login
    {
        public static async Task UserLogin()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Login");
            Console.WriteLine("Enter username: ");
            string usernameInput = Console.ReadLine().ToLower();
            Console.WriteLine("Enter password: ");
            string passwordInput = Console.ReadLine();

            string loginApiUrl = "http://localhost:5158/login";

            var credentials = new
            {
                Username = usernameInput,
                Password = passwordInput
            };

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(loginApiUrl, credentials);
                if (response.IsSuccessStatusCode)
                {
                    var loggedInUser = await response.Content.ReadFromJsonAsync<LoggedInUser>();

                    if (loggedInUser != null)
                    {
                        Console.WriteLine($"Login successful. User: {loggedInUser.Id}. {loggedInUser.Fullname}");
                    }
                    else
                    {
                        Console.WriteLine("Failed to login");
                    }
                }
            }
        }
    }
}
