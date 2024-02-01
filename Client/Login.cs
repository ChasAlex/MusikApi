using Client.Models.Viewmodels;
using Client.PasswordHandler;
using System.Net.Http.Json;

namespace Client
{
    public class Login
    {

        private const string _loginApiUrl = "http://localhost:5158/login";
        private const string _signupApiUrl = "http://localhost:5158/signup";


        public async Task UserLoginAsync()
        {
            while (true)
            {
                //Checks if user wants to login or signup
                var loginOptions = new List<string> { "Login", "Signup", };


                int indexLoginOption = Menu.ShowMenu(loginOptions, "Welcome");
                if (indexLoginOption == 0) //menu choice: login
                {
                    Console.WriteLine("Enter username: ");
                    string userNameInput = Console.ReadLine().ToLower();

                    Console.WriteLine("Enter password: ");
                    string passwordInput = PasswordManager.HideAndReadPassword();


                    var credentials = new
                    {
                        Username = userNameInput,
                        Password = passwordInput
                    };

                    Console.Clear();
                    using HttpClient client = new HttpClient();

                    HttpResponseMessage response = await client.PostAsJsonAsync(_loginApiUrl, credentials);

                    if (response.IsSuccessStatusCode)
                    {
                        var loggedInUser = await response.Content.ReadFromJsonAsync<User>(); //gets and stores info about who is logged in

                        if (loggedInUser != null)
                        {
                            await MainMenuAsync(loggedInUser); //starts main menu for logged in user
                        }
                        else
                        {
                            Console.WriteLine("Failed to login");
                        }
                    }
                }
                else if (indexLoginOption == 1)//menu choice: signup
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

        //Checks if username already is taken, and if not creates user
        public async Task SignupAsync()
        {
            Console.WriteLine("Enter fullname: ");
            string fullNameInput = Console.ReadLine();

            Console.WriteLine("Enter username: ");
            string userNameInput = Console.ReadLine().ToLower();

            Console.WriteLine("Enter password: ");
            string passwordInput = Console.ReadLine();

            var signupInfo = new
            {
                fullname = fullNameInput,
                Username = userNameInput,
                Password = passwordInput
            };

            Console.Clear();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(_signupApiUrl, signupInfo);

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

        //loops main menu
        public async Task MainMenuAsync(User loggedInUser)
        {
            Console.WriteLine($"Login successful. User: {loggedInUser.Id}. {loggedInUser.Fullname}");
            List<string> mainMenuOptions = new List<string>
                        {
                            "See favorite artists",
                            "See favorite songs",
                            "See favorite genres",
                            "Add a new favorite artist",
                            "Add a new favorite song",
                            "Add a new favorite genre",
                            "Search for an Artist",
                            "Search for an Genre",
                            "Search toptracks for an Artist",
                            "Log out"
                        };
            while (loggedInUser != null)
            {

                int choice = Menu.ShowMenu(mainMenuOptions);
                bool loggedIn = await MainMenuOptionsAsync(choice, loggedInUser);

                if (!loggedIn)
                {
                    Console.WriteLine("Logging out...");
                    loggedInUser = null;
                }
            }
        }

        //options for main menu
        public async Task<bool> MainMenuOptionsAsync(int optionIndex, User loggedInUser)
        {
            UserHandler userHandler = new UserHandler(loggedInUser);

            switch (optionIndex)
            {
                case 0:
                    await userHandler.ArtistByIdAsync();
                    break;
                case 1:
                    await userHandler.SongByIdAsync();
                    break;
                case 2:
                    await userHandler.GenresByIdAsync();
                    break;
                case 3:
                    await userHandler.ConnectUserToArtistAsync();
                    break;
                case 4:
                    await userHandler.ConnectUserToSongAsync();
                    break;
                case 5:
                    await userHandler.ConnectUserToGenreAsync();
                    break;
                case 6:
                    await userHandler.GetInfoFromAristAsync();
                    break;
                case 7:
                    await userHandler.GetTopSongsGenreAsync();
                    break;
                case 8:
                    await userHandler.GetTopSongsArtistAsync();
                    break;
                case 9:
                    return false; //returns false for loggedIn, which makes main menu delete info about logged in user, which stops main menu-loop
            }

            return true;
        }
    }
}
