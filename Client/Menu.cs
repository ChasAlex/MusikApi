using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Menu
    {
        public int ShowMenu(List<string> menuOptions, string title = "Menu")
        {
            int selectedIndex = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(title);
                for (int i = 0; i < menuOptions.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write("-> ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    Console.WriteLine(menuOptions[i]);
                }
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + menuOptions.Count) % menuOptions.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % menuOptions.Count;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return selectedIndex;
                }
            }
        }
        private bool ExecuteMenuOption(int optionIndex)
        {
            switch (optionIndex)
            {
                case 0:
                    Console.ReadLine();
                    break;
                case 1:
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Logging out...");
                    //clear LoggedInUser
                    break;
            }
            return true;
        }
    }
}