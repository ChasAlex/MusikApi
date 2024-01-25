
namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Login login = new Login();
            await login.UserLogin();
        }
    }
}