using System;

namespace BankSystem
{
    [Serializable]
    public class BankClient
    {
        public string Name { get; set; }
        public string Passport { get; set; }
        public double Balance { get; set; }

        public BankClient() { }

        public BankClient(string name, string passport, double balance)
        {
            Name = name;
            Passport = passport;
            Balance = balance;
        }
    }
}