using System;
using System.Threading;

namespace Course_Console
{   
    public class ClientFactory
    {
        public static IClient FactoryMethod()
        {
            Console.Clear();
            Console.Write("How will you pay?\n1) By card\n2) By cash\n");
            int choice;
            do
            {
                Console.Write("Choice: ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > 2);
            if (choice == 1)
                return new CardClient();
            else
                return new CashClient();
        }
    }

    public interface IClient
    {
        bool PayForEvents();
    }
    public class AbstractClient
    {
        IClient client;
        public AbstractClient(IClient client)
        {
            this.client = client;
        }
        public bool PayForEvents()
        {
            return client.PayForEvents();
        }
    }
    public class CashClient : IClient
    {
        int cash;
        public CashClient()
        {
            Random rand = new Random();
            cash = rand.Next(30000, 35000);
        }
        public bool PayForEvents()
        {
            EventAgency agency = EventAgency.Instance();
            if (cash >= agency.events.cost)
            {
                cash -= agency.events.cost;               
                return true;
            }
            return false;
        }
    }
    public class CardClient : IClient
    {
        string card;
        int balance;
        public CardClient()
        {
            Console.Clear();
            do
            {
                Console.Write("Enter number of your card (16 digits): ");
                card = Console.ReadLine();
            } while (card.Length != 16 || int.TryParse(card, out int a));                            
            balance = GetCardBalance();            
        }
        private bool BankAccess()
        {
            Console.WriteLine("Access to the bank, please, wait...");
            Thread.Sleep(3000);
            return true;
        }
        private int GetCardBalance()
        {
            if (BankAccess())
            {
                Random random = new Random();
                return random.Next(40000, 50000);
            }
            return -1;
        }
        private bool TakeMoney(int sum)
        {
            if (BankAccess())
            {
                balance -= sum;
                return true;
            }
            return false;

        }
        public bool PayForEvents()
        {
            EventAgency agency = EventAgency.Instance();
            if (balance >= agency.events.cost)
            {
                return TakeMoney(agency.events.cost);
            }
            return false;
        }
    }
}

