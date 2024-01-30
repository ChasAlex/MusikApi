namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var login = new Login();
                await login.UserLoginAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application is closing due to an error.. " +
                    $"{Environment.NewLine} Error: {ex.Message}");
            }
        }
    }
}