using System;
using System.Collections.Generic;
using System.Text;

namespace Wypozyczalnia
{
    [Serializable]
    class Account
    {
        public Account(string name, string password, AccountTypes type)
        {
            this.name = name;
            this.password = password;
            this.type = type;
        }

        public string name { get; set; }
        public string password { get; set; }
        public  AccountTypes type { get; set; }

        public override string ToString()
        {
            return $"{name} | {type}";
        }

    }
}
