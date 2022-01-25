using System;
using System.Collections.Generic;
using System.Text;

namespace Wypozyczalnia
{
    class Vehicle
    {
       static int lastId = 0;
       int id;
       string name;
       VehicleType type;
       DateTime orderedFrom;
       DateTime orderedTo;
       

        public Vehicle(string name, VehicleType type)
        {
            id = lastId++;
            this.name = name;
            this.Type = type;
        }

        public DateTime OrderedFrom { get => orderedFrom; set => orderedFrom = value; }
        public DateTime OrderedTo { get => orderedTo; set => orderedTo = value; }
        public int Id { get => id;}
        internal VehicleType Type { get => type; set => type = value; }

        override public string ToString()
        {
            return $"{id} | {name} | {type}";
        }
    }
}
