namespace Client.PasswordHandler
{
    public static class PasswordManager
    {

        public static string HideAndReadPassword(char hideChar = '*')
        {
            const ConsoleKey EnterKey = ConsoleKey.Enter;
            const ConsoleKey BackspaceKey = ConsoleKey.Backspace;

            var password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key == BackspaceKey)
                {
                    RemoveLastInputChar(ref password);
                }
                else if (key.Key != EnterKey)
                {
                    HandleTheHideInput(key.KeyChar, hideChar);
                    password += key.KeyChar;
                }

            } while (key.Key != EnterKey);

            Console.WriteLine();

            return password;
        }


        private static void RemoveLastInputChar(ref string password)
        {
            if (password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                GoBackWithCursor();
            }
        }


        private static void HandleTheHideInput(char inputChar, char hideChar)
        {
            Console.Write(hideChar);
        }


        private static void GoBackWithCursor()
        {
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            Console.Write(" ");
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}
