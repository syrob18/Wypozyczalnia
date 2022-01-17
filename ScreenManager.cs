using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

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
                    case Commands.ZAREJESTRUJ:
                        Register();
                        break;
                    case Commands.POMOC:
                        //ShowHelp();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Brak komendy {commandProvided}. Sprobuj POMOC aby uzsykac wskazowki");
            }
            PrintScreen();
        }

        static string accountJSON;

        static void JsonSerialization()
        {

        }
        static void Login()
        {

        }

        static void Register()
        {
            Console.WriteLine("Podaj nazwe");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj haslo");
            string password = Console.ReadLine();
            Console.WriteLine("Czy chcesz aby konto bylo adminem? (TAK/NIE)");
            string isAdmin = Console.ReadLine();
            AccountTypes type = AccountTypes.Standard;
            if(isAdmin.ToUpper() == "TAK")
            {
                type = AccountTypes.Admin;
            }

            ArrayList list = new ArrayList();
            Account newAccount = new Account(name, password, type);
            string fileName = "./Accounts.json";
            string jsonString = "";
            if (File.Exists(fileName))
            {
                jsonString = File.ReadAllText(fileName);
                if (jsonString != "")
                {
                    list = JsonSerializer.Deserialize<ArrayList>(jsonString);
                }
            }
            else
            {
                File.Create(fileName).Close();
                
            }
            /*string jsonAccount = JsonSerializer.Serialize(newAccount);
            Console.WriteLine(jsonAccount);*/
            
            
            list.Add(newAccount);
            string jsonAccounts = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, jsonAccounts);
        }
    }
}
