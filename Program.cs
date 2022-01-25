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
            ScreenManager.LoadList(vehicleList);
            ScreenManager.PrintScreen();
            Console.ReadKey();
        }
    }
}
