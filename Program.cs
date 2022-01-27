using System;
using System.Collections.Generic;

namespace Wypozyczalnia
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vehicle> vehicleList = new List<Vehicle>
            {
                new Vehicle("Porshe 911", VehicleType.CAR),
                new Vehicle("Audi A4", VehicleType.CAR),
                new Vehicle("Aston Martin", VehicleType.CAR),
                new Vehicle("Wigry", VehicleType.BICECYCLE),
                new Vehicle("Walker Bay Venture 14", VehicleType.BOAT)
            };
            List<Account> accountList = new List<Account>
            {
                new Account("kacper","1234", AccountTypes.Admin),
                new Account("adam","73a73", AccountTypes.Standard),
                new Account("szymon","aabbcc", AccountTypes.Admin),
                new Account("kacper121","aers12", AccountTypes.Standard),
            };
            ScreenManager.LoadList(vehicleList);
            ScreenManager.LoadAccounts(accountList);
            ScreenManager.PrintScreen();
            Console.ReadKey();
        }
    }
}
