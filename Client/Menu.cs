
namespace Client
{
    public class Menu
    {
        public int ShowMenu(List<string> menuOptions, string title = "Menu")
        {
            int selectedIndex = 0;
            int maxAlternativesPerSide = 10;
            List<string> partialMenuOptions;
            int partsOfMenu = (int)(Math.Ceiling((double)menuOptions.Count / maxAlternativesPerSide));

            int amountAlternativesOnThisSide = default;
            int part = 1;
            while (true)
            {
                amountAlternativesOnThisSide = partsOfMenu > part ? maxAlternativesPerSide : ((int)(double)menuOptions.Count - 1) % maxAlternativesPerSide + 1;
                partialMenuOptions = menuOptions.
                    GetRange(
                        (part - 1) * maxAlternativesPerSide,
                        amountAlternativesOnThisSide);
                Console.Clear();
                Console.WriteLine(title);
                for (int i = 0; i < amountAlternativesOnThisSide; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write("-> ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    Console.WriteLine(partialMenuOptions[i]);
                }
                if (partsOfMenu != 1)
                {
                    Console.WriteLine($"Page {part} of {partsOfMenu}. Press right or left to see more");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + partialMenuOptions.Count) % partialMenuOptions.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % partialMenuOptions.Count;
                        break;
                    case ConsoleKey.LeftArrow:
                        part = (part + partsOfMenu) % partsOfMenu + 1;
                        selectedIndex = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        part = part % partsOfMenu + 1;
                        selectedIndex = 0;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return selectedIndex + (part - 1) * maxAlternativesPerSide;

                }
            }
        }
    }
}