using System.Net.Http.Json;
using Client.Models.Viewmodels;

namespace Client
{
    public class Login
    {
        public async Task UserLogin()
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
                    var loggedInUser = await response.Content.ReadFromJsonAsync<User>();

                    if (loggedInUser != null)
                    {
                        Console.WriteLine($"Login successful. User: {loggedInUser.Id}. {loggedInUser.Fullname}");
                        List<string> mainMenuOptions = new List<string>
                        {
                            "See favorite artists",
                            "See favorite songs",
                            "Add a new favorite artist",
                            "Search for an Artist",
                            "Log out"
                        };
                        while (loggedInUser != null)
                        {
                            Menu menu = new Menu();
                            int choice = menu.ShowMenu(mainMenuOptions);
                            bool loggedIn = await ExecuteMainMenuOption(choice, loggedInUser);
                            if (!loggedIn)
                            {
                                Console.WriteLine("Logging out...");
                                loggedInUser = null;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to login");
                    }
                }
            }
        }

        public async Task<bool> ExecuteMainMenuOption(int optionIndex, User loggedInUser)
        {
            UserHandler userHandler = new UserHandler(loggedInUser);
            switch (optionIndex)
            {
                case 0:
                    await userHandler.ArtistById();
                    break;
                case 1:
                    await userHandler.SongById();
                    break;
                case 2:
                    await userHandler.ConnectUserToArtist();
                    break;
                case 3:
                    await userHandler.GetInfoFromArist();
                    break; 
                case 4:
                    return false;
            }
            return true;
        }
    }
}
