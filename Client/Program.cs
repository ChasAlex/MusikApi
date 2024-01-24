
namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if(await Login.UserLogin())
            {
                Console.Write("Search for an Artist: ");
                string artist = Console.ReadLine();
                LoggedInUser loggedInUser = new LoggedInUser();
                UserHandler userHandler = new UserHandler(loggedInUser);
                await UserHandler.GetInfoFromArist(artist);
                Console.ReadLine();
            }
        }
    }
}