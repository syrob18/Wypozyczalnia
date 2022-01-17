using System;
using System.Collections.Generic;
using System.Text;

namespace Wypozyczalnia
{
   
    static class ScreenManager
    {
        static string screensDetailsURL = "ScreensPropeties.json";

        public static void PrintScreen()
        {
            Console.WriteLine("TITLE");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("SOME TEXT");

            string command = Console.ReadLine();
           
            CommandPicker(command);
        }

        static void CommandPicker(string commandProvided)
        {
            bool commandExist = false;
            Commands commandToUse = Commands.NULL;
            foreach(Commands command in Enum.GetValues(typeof(Commands)))
            {
                if(commandProvided.ToString().ToUpper() == command.ToString().ToUpper())
                {
                    commandExist = true;
                    commandToUse = command;
                    break;
                }
            }
            if (commandExist)
            {
                Console.WriteLine("DZIALA");
                switch (commandToUse)
                {
                    case Commands.NULL;
                        InvalidCommand();
                        break;
                    case Commands.POMOC:
                        ShowHelp();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Brak komendy {commandProvided}. Sprobuj POMOC aby uzsykac wskazowki");
            }
        }
    }
}
