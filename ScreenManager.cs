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
        static Account loggedAccount = null;
        static List<Vehicle> data = null;
        static List<Account> dataAccount = null;

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

        static public void LoadAccounts(List<Account> accounts)
        {
            dataAccount = accounts;
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
                    case Commands.POKAZKONTA:
                        ShowAccounts();
                        break;
                    case Commands.DODAJADMINA:
                        AdminPromotion();
                        break;
                    case Commands.ZMIENSTATUS:
                        VehicleStatusChange();
                        break;
                    case Commands.USUNPOJAZD:
                        DeleteVehicle();
                        break;
                    case Commands.WYPOZYCZ:
                        BorrowVehicle();
                        break;
                    case Commands.ZWROC:
                        ReturnVehicle();
                        break;
                    case Commands.WIADOMOSC:
                        SendMessage();
                        break;
                    case Commands.ZAREZERWUJ:
                        OrderVehicle();
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Brak komendy {commandProvided}. Sprobuj POMOC aby uzsykac wskazowki");
            }
            PrintScreen();
        }

        
        static void Login()
        {
            Console.WriteLine("Podaj nazwe");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj haslo");
            string password = Console.ReadLine();

          

            foreach (Account account in dataAccount)
            {
                if(account.name == name)
                {
                    if(account.password == password)
                    {
                        loggedAccount = account;
                        Console.Clear();
                        Console.WriteLine("Zalogowano");
                        if (loggedAccount.message != "")
                        {
                            Console.WriteLine($"Oczekujaca wiadomosc od admina: \n {loggedAccount.message}");
                            loggedAccount.message = "";
                        }
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
            AccountTypes type = AccountTypes.Standard;
            if (loggedAccount != null && loggedAccount.type == AccountTypes.Admin)
            {
                Console.WriteLine("Czy chcesz aby konto bylo adminem? (TAK/NIE)");
                string isAdmin = Console.ReadLine();

                
                if (isAdmin.ToUpper() == "TAK")
                {
                    type = AccountTypes.Admin;
                }
            }
          
            Account newAccount = new Account(name, password, type);
            dataAccount.Add(newAccount);
           
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
            Console.Clear();
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

        static void ShowAccounts()
        {
            if (loggedAccount != null && loggedAccount.type == AccountTypes.Admin)
            {
                if (data != null)
                {
                    foreach (Account veh in dataAccount)
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine(veh);

                    }
                }
            }
            else
            {
                Console.WriteLine("Zaloguj sie na konto administratora");
            }
        }

        static void AdminPromotion()
        {
            if (loggedAccount != null && loggedAccount.type == AccountTypes.Admin)
            {
                Console.WriteLine("Podaj nazwe konta");
                string name = Console.ReadLine();
                foreach (Account account in dataAccount)
                {
                    if(account.name == name)
                    {
                        account.type = AccountTypes.Admin;
                    }

                }
            }
            else
            {
                Console.WriteLine("Zaloguj sie na konto administratora");
            }
        }

        static Vehicle GetVehViaId(int id)
        {
            foreach(Vehicle veh in data)
            {
                if(veh.Id == id)
                {
                    return veh;

                }
            }
            return null;
        }

        static Account GetAccountViaId(int id)
        {
            foreach (Account account in dataAccount)
            {
                if (account.id == id)
                {
                    return account;

                }
            }
            return null;
        }

        public static string GetAccNameViaId(int id)
        {
           return GetAccountViaId(id).name;
                
        }

        static void VehicleStatusChange()
        {
            if (loggedAccount != null && loggedAccount.type == AccountTypes.Admin)
            {
                Console.WriteLine("Podaj numer id pojazdu");
                string id = Console.ReadLine();
                bool success = int.TryParse(id, out int successed);
                
                if (success)
                {
                    Vehicle vehSelected = null;
                    foreach (Vehicle veh in data)
                    {
                        if (veh.Id == successed)
                        {
                            vehSelected = veh ;
                            Console.WriteLine($"Wybrales {vehSelected.Name}");
                        }

                    }
                    if(vehSelected == null)
                    {
                        Console.WriteLine("Nie ma takiego pojazdu");
                        return;
                    }
                    Console.WriteLine("Dostepne typy statusow: ");
                    foreach (Statuses types in Enum.GetValues(typeof(Statuses)))
                    {
                        if (types != Statuses.NULL)
                        {
                            Console.Write(types.ToString() + ", ");
                            Console.WriteLine("");
                        }
                    }
                    Console.WriteLine("Podaj nowy stan pojazdu:");
                    string status = Console.ReadLine();

                    bool typeExist = false;
                    Statuses typeToUse = Statuses.NULL;
                    foreach (Statuses types in Enum.GetValues(typeof(Statuses)))
                    {
                        if (types.ToString().ToUpper() == status.ToString().ToUpper())
                        {
                            typeExist = true;
                            typeToUse = types;
                            break;
                        }
                    }
                    if (typeExist)
                    {
                        Console.WriteLine("Pojazd po zmiaanie statusu:");
                        Console.WriteLine(typeToUse);
                        GetVehViaId(successed).Status = typeToUse;
                        Console.WriteLine(GetVehViaId(successed));
                        vehSelected.Status = typeToUse;
                        
                    }
                    else
                    {
                        Console.WriteLine("Brak takie typu, sprobuj dodac pojazd ponownie");
                    }
                }

            }
            else
            {
                Console.WriteLine("Zaloguj sie na konto administratora");
            }
        }

        static void DeleteVehicle()
        {
            if (loggedAccount != null && loggedAccount.type == AccountTypes.Admin)
            {
                Console.WriteLine("Podaj numer id pojazdu");
                string id = Console.ReadLine();
                bool success = int.TryParse(id, out int successed);

                if (success)
                {
                    Vehicle vehSelected = null;
                    foreach (Vehicle veh in data)
                    {
                        if (veh.Id == successed)
                        {
                            vehSelected = veh;
                            Console.WriteLine($"Usunales {vehSelected.Name}");
                            data.Remove(veh);
                            return;
                        }

                    }
                    if (vehSelected == null)
                    {
                        Console.WriteLine("Nie ma takiego pojazdu");
                        return;
                    }
                  
                }

            }
            else
            {
                Console.WriteLine("Zaloguj sie na konto administratora");
            }
        }

        static void BorrowVehicle()
        {
            if (loggedAccount != null)
            {
                Console.WriteLine("Podaj numer id pojazdu");
                string id = Console.ReadLine();
                bool success = int.TryParse(id, out int successed);

                if (success)
                {
                    Vehicle vehSelected = null;
                    foreach (Vehicle veh in data)
                    {
                        if (veh.Id == successed)
                        {
                            if (veh.Status == Statuses.AVAILABLE)
                            {

                                Console.WriteLine($"Wypozyczyles {veh.Name}");
                                GetVehViaId(successed).BorrowedToWho = loggedAccount.id;
                                GetVehViaId(successed).Status = Statuses.BORROWED;
                                return;
                            }
                            else
                            {
                                Console.WriteLine($"Ten pojazd jest wypozyczony przez kogos innego");
                                return;
                            }
                        }

                    }
                    if (vehSelected == null)
                    {
                        Console.WriteLine("Nie ma takiego pojazdu");
                        return;
                    }

                }

            }
            else
            {
                Console.WriteLine("Zaloguj sie na konto");
            }
        }

        static void ReturnVehicle()
        {
            if (loggedAccount != null)
            {
                Console.WriteLine("Podaj numer id pojazdu");
                string id = Console.ReadLine();
                bool success = int.TryParse(id, out int successed);

                if (success)
                {
                    Vehicle vehSelected = null;
                    foreach (Vehicle veh in data)
                    {
                        if (veh.Id == successed)
                        {
                            if (veh.Status == Statuses.BORROWED || veh.Status == Statuses.ORDERED)
                            {
                                if (veh.BorrowedToWho == loggedAccount.id)
                                {
                                    Console.WriteLine($"Oddales {veh.Name}");
                                    GetVehViaId(successed).BorrowedToWho = -1;
                                    GetVehViaId(successed).Status = Statuses.AVAILABLE;
                                    
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine($"Ten pojazd nie jest wypozyczony przez Ciebie");
                                    return;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Ten pojazd nie jest wypozyczony przez Ciebie");
                                return;
                            }
                        }

                    }
                    if (vehSelected == null)
                    {
                        Console.WriteLine("Nie ma takiego pojazdu");
                        return;
                    }

                }

            }
            else
            {
                Console.WriteLine("Zaloguj sie na konto");
            }
        }

        static void SendMessage()
        {
            if (loggedAccount != null && loggedAccount.type == AccountTypes.Admin)
            {
                Console.WriteLine("Podaj numer id konta");
                string id = Console.ReadLine();
                bool success = int.TryParse(id, out int successed);

                if (success)
                {
                    
                    foreach (Account account in dataAccount)
                    {
                        if (account.id == successed)
                        {
                            Console.WriteLine("Podaj wiadomosc");
                            string message = Console.ReadLine();
                            GetAccountViaId(successed).message = message;
                            Console.WriteLine($"Wyslales wiadomosc: {message} do uzytkownika {account.name}");
                            return;
                        }

                    }
                    Console.WriteLine("Nie ma takiego konta");
                }
                else
                {
                    Console.WriteLine("Zaloguj sie na konto administratora");
                }
            }
        }
        static DateTime FromStringToDate(string dateString)
        {
            string[] datesDetails = dateString.Split("-");
            bool convertionY = int.TryParse(datesDetails[0], out int year);
            bool convertionM = int.TryParse(datesDetails[1], out int month);
            bool convertionD = int.TryParse(datesDetails[2], out int day);
            if (convertionD && convertionM && convertionY)
            {
                try
                {
                    return new DateTime(year, month, day);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Podano zla date, automatycznie ustawiono dzien dzisiejszy");
                    return DateTime.Today;
                }
            }
            else
            {
                Console.WriteLine("Podano zla date, automatycznie ustawiono dzien dzisiejszy");
                return DateTime.Today;
            }
            
        }
        static void OrderVehicle()
        {
            if (loggedAccount != null)
            {
                Console.WriteLine("Podaj numer id pojazdu");
                string id = Console.ReadLine();
                bool success = int.TryParse(id, out int successed);

                if (success)
                {
                    Vehicle vehSelected = null;
                    foreach (Vehicle veh in data)
                    {
                        if (veh.Id == successed)
                        {
                            if (veh.Status == Statuses.AVAILABLE)
                            {

                                Console.WriteLine($"Chcesz zamowic {veh.Name}");
                                Console.WriteLine($"Podaj date rozpoczecia wypozyczenia w formacie yyyy-mm-dd np. 2023-12-1");
                                string dateString = Console.ReadLine();
                                DateTime dateStart = FromStringToDate(dateString);
                                Console.WriteLine($"Podaj date zakonczenia wypozyczenia w formacie yyyy-mm-dd np. 2023-12-1");
                                dateString = Console.ReadLine();
                                DateTime dateEnd =  FromStringToDate(dateString);
                                if (dateStart < dateEnd)
                                {
                                    GetVehViaId(successed).BorrowedToWho = loggedAccount.id;
                                    GetVehViaId(successed).Status = Statuses.ORDERED;
                                    GetVehViaId(successed).OrderedFrom = dateStart;
                                    GetVehViaId(successed).OrderedTo = dateEnd;
                                    return;
                                }
                                Console.WriteLine("Bledne daty. Sprobuj jeszcze raz");
                                return;
                            }
                            else
                            {
                                Console.WriteLine($"Ten pojazd jest wypozyczony przez kogos innego");
                                return;
                            }
                        }

                    }
                    if (vehSelected == null)
                    {
                        Console.WriteLine("Nie ma takiego pojazdu");
                        return;
                    }

                }

            }
            else
            {
                Console.WriteLine("Zaloguj sie na konto");
            }
        }
    }
}
