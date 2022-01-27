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
        Statuses status;
        int borrowedToWho= -1;
       

        public Vehicle(string name, VehicleType type)
        {
            id = lastId++;
            this.name = name;
            this.Type = type;
            this.status = Statuses.AVAILABLE;
        }

        public DateTime OrderedFrom { get => orderedFrom; set => orderedFrom = value; }
        public DateTime OrderedTo { get => orderedTo; set => orderedTo = value; }
        public int Id { get => id;}
        public string Name { get => name; set => name = value; }
        internal VehicleType Type { get => type; set => type = value; }
        public Statuses Status { get => status; set => status = value; }
        public int BorrowedToWho { get => borrowedToWho; set => borrowedToWho = value; }

        override public string ToString()
        {
            string text = $"{id} | {name} | {type} | {Status}";
            if (BorrowedToWho != -1)
            {
                if (status == Statuses.BORROWED)
                {
                    text = $"{id} | {name} | {type} | {Status} | {ScreenManager.GetAccNameViaId(borrowedToWho)}";
                }
                else if(status == Statuses.ORDERED)
                {
                    text = $"{id} | {name} | {type} | {Status} | {ScreenManager.GetAccNameViaId(borrowedToWho)} | From: {orderedFrom.Date} To: {orderedTo.Date}";
                }
            }
            return text;
        }

    }
}
