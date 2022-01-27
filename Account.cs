using System;
using System.Collections.Generic;
using System.Text;

namespace Wypozyczalnia
{
    class Account
    {

        static int lastId=0;
        public Account(string name, string password, AccountTypes type)
        {
            this.name = name;
            this.password = password;
            this.type = type;
            this.id = lastId++;

        }
        public string message { get; set; } = "";
        public int id { get;}
        public string name { get; set; }
        public string password { get; set; }
        public  AccountTypes type { get; set; }


        public override string ToString()
        {
            return $"{name} | {type}";
        }

        

    }
}
