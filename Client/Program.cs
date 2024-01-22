
namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await Login.UserLogin();
        }
    }
}