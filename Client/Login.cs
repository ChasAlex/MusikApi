using Client.Models.Viewmodels;
using Client.PasswordHandler;
using System.Net.Http.Json;

namespace Client
{
    public class Login
    {
        public async Task UserLoginAsync()
        {
            while (true)
            {
                List<string> loginOptions = new List<string>
                        {
                            "Login",
                            "Signup",
                        };
                Menu menu = new Menu();
                int indexLoginOption = menu.ShowMenu(loginOptions, "Welcome");
                if (indexLoginOption == 0)
                {
                    Console.WriteLine("Enter username: ");
                    string usernameInput = Console.ReadLine().ToLower();
                    Console.WriteLine("Enter password: ");
                    string passwordInput = PasswordManager.HideAndReadPassword();

                    string loginApiUrl = "http://localhost:5158/login";

                    var credentials = new
                    {
                        Username = usernameInput,
                        Password = passwordInput
                    };

                    Console.Clear();
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.PostAsJsonAsync(loginApiUrl, credentials);
                        if (response.IsSuccessStatusCode)
                        {
                            var loggedInUser = await response.Content.ReadFromJsonAsync<User>();

                            if (loggedInUser != null)
                            {
                                await MainMenuAsync(loggedInUser);
                            }
                            else
                            {
                                Console.WriteLine("Failed to login");
                            }
                        }
                    }
                }
                else if (indexLoginOption == 1)
                {
                    await SignupAsync();
                }
                else
                {
                    Console.WriteLine("Something went wrong :(");
                }
                Console.Write("Press Enter to return to menu");
                Console.ReadKey();
            }
        }


        public async Task SignupAsync()
        {
            Console.WriteLine("Enter fullname: ");
            string fullnameInput = Console.ReadLine();
            Console.WriteLine("Enter username: ");
            string usernameInput = Console.ReadLine().ToLower();
            Console.WriteLine("Enter password: ");
            string passwordInput = Console.ReadLine();

            string signupApiUrl = "http://localhost:5158/signup";

            var signupInfo = new
            {
                fullname = fullnameInput,
                Username = usernameInput,
                Password = passwordInput
            };

            Console.Clear();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(signupApiUrl, signupInfo);
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync("Hurray! You've been registered!");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    Console.WriteLine("Username already exists");
                }
                else
                {
                    await Console.Out.WriteLineAsync("Something went wrong :(");
                }
            }
            Console.Write("Press Enter to return to menu");
            Console.ReadKey();
        }


        public async Task MainMenuAsync(User loggedInUser)
        {
            Console.WriteLine($"Login successful. User: {loggedInUser.Id}. {loggedInUser.Fullname}");
            List<string> mainMenuOptions = new List<string>
                        {
                            "See favorite artists",
                            "See favorite genres",
                            "See favorite songs",
                            "Add a new favorite artist",
                            "Search for an Artist",
                            "Log out"
                        };
            while (loggedInUser != null)
            {
                Menu loginMenu = new Menu();
                int choice = loginMenu.ShowMenu(mainMenuOptions);
                bool loggedIn = await MainMenuOptionsAsync(choice, loggedInUser);
                if (!loggedIn)
                {
                    Console.WriteLine("Logging out...");
                    loggedInUser = null;
                }
            }
        }


        public async Task<bool> MainMenuOptionsAsync(int optionIndex, User loggedInUser)
        {
            UserHandler userHandler = new UserHandler(loggedInUser);
            switch (optionIndex)
            {
                case 0:
                    await userHandler.ArtistById();
                    break;
                case 1:
                    await userHandler.GenresById();
                    break;
                case 2:
                    await userHandler.SongById();
                    break;
                case 3:
                    await userHandler.ConnectUserToArtist();
                    break;
                case 4:
                    await userHandler.GetInfoFromArist();
                    break;
                case 5:
                    return false;
            }
            return true;
        }
    }
}
