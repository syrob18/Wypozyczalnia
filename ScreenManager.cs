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
        static Account loggedAccount = null;
        static List<Vehicle> data = null;

        public static void PrintScreen()
        {
            string name ="Nieznajomy";
            if (loggedAccount != null)
            {
                name = loggedAccount.name;
            }
            Console.WriteLine("-----------------------------");
            Console.WriteLine($"Witaj, {name}");
            
            Console.WriteLine("Podaj komende:");

            string command = Console.ReadLine();

            CommandPicker(command);
        }

        static public void LoadList(List<Vehicle> listVehicle)
        {
            data = listVehicle;
        }

        static void CommandPicker(string commandProvided)
        {
            bool commandExist = false;
            Commands commandToUse = Commands.NULL;
            foreach (Commands command in Enum.GetValues(typeof(Commands)))
            {
                if (commandProvided.ToString().ToUpper() == command.ToString().ToUpper())
                {
                    commandExist = true;
                    commandToUse = command;
                    break;
                }
            }
            if (commandExist)
            {
                switch (commandToUse)
                {
                    case Commands.ZAREJESTRUJ:
                        Register();
                        break;
                    case Commands.ZALOGUJ:
                        Login();
                        break;
                    case Commands.POMOC:
                        ShowHelp();
                        break;
                    case Commands.WYLOGUJ:
                        SignOut();
                        break;
                    case Commands.DODAJPOJAZD:
                        AddVehicle();
                        break;
                    case Commands.POKAZPOJAZDY:
                        ShowStatus();
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
            Console.WriteLine("Podaj nazwe");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj haslo");
            string password = Console.ReadLine();

            ArrayList list = new ArrayList();
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
                Console.WriteLine("Brak takiego konta");
                return;
            }
            ArrayList listAccounts = new ArrayList();
            foreach(JsonElement element in list)
            {
               
                Account account = new Account(element.GetProperty("name").GetString(), element.GetProperty("password").GetString(), (AccountTypes) element.GetProperty("type").GetInt32());
                listAccounts.Add(account);
            }



            foreach(Account account in listAccounts)
            {
                if(account.name == name)
                {
                    if(account.password == password)
                    {
                        loggedAccount = account;
                        Console.Clear();
                        Console.WriteLine("Zalogowano");
                        return;
                    }
                }
            }
            Console.WriteLine("Brak takiego konta");
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
            if (isAdmin.ToUpper() == "TAK")
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

        static void ShowHelp() {
            Console.WriteLine("Dostepne komendy: ");
            foreach (Commands command in Enum.GetValues(typeof(Commands)))
            {
                if (command != Commands.NULL)
                {
                    Console.Write(command.ToString() + ", ");
                }
            }
            Console.WriteLine();
        }

        static void SignOut()
        {
            
            Console.WriteLine($"Zegnaj {loggedAccount.name}");
            loggedAccount = null;
            
        }

        static void AddVehicle()
        {
            if (loggedAccount != null && loggedAccount.type == AccountTypes.Admin)
            {
                Console.WriteLine("Podaj nazwe pojazdu");
                string name = Console.ReadLine();
                Console.WriteLine("Dostepne komendy typy pojazdow: ");
                foreach (VehicleType types in Enum.GetValues(typeof(VehicleType)))
                {
                    if (types != VehicleType.NULL)
                    {
                        Console.Write(types.ToString() + ", ");
                        Console.WriteLine("");
                    }
                }
                Console.WriteLine("Podaj typ pojazdu:");
                string type = Console.ReadLine();

                bool typeExist = false;
                VehicleType typeToUse = VehicleType.NULL;
                foreach (VehicleType types in Enum.GetValues(typeof(VehicleType)))
                {
                    if (type.ToString().ToUpper() == types.ToString().ToUpper())
                    {
                        typeExist = true;
                        typeToUse = types;
                        break;
                    }
                }
                if (typeExist)
                {
                    data.Add(new Vehicle(name, typeToUse));
                }
                else
                {
                    Console.WriteLine("Brak takie typu, sprobuj dodac pojazd ponownie");
                }
            }
            else
            {
                Console.WriteLine("Zaloguj sie na konto administratora");
            }
        }

        static void ShowStatus()
        {
            if (data != null)
            {
                foreach(Vehicle veh in data)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine(veh);
                    
                }
            }
        } 
    }
}
