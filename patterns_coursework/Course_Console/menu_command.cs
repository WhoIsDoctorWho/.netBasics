using System;

namespace Course_Console
{
    public abstract class MenuCommand
    {
        public Receiver receiver;
        public bool isLeaf = false; // если команда - лист, мы не добавляем её в историю возможного возврата
        public abstract IMenuState Execute();
        public static void ReadKey()
        {
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
        }
        public string GetDescription()
        {
            return receiver.description;
        }
    }
    public abstract class Receiver
    {
        public string description;
        public static int ReadUInt(string request)
        {
            int gNum;
            do
            {
                Console.Clear();
                Console.Write(request);
            } while (!int.TryParse(Console.ReadLine(), out gNum) || gNum < 0);
            return gNum;
        }
        public virtual void Exec() { }
    }

    public class Start : Receiver
    {
        public Start()
        {
            description = "Some description of the start command";
        }
    }
    public class StartCommand : MenuCommand
    {
        public StartCommand()
        {
            receiver = new Start();
        }
        public override IMenuState Execute()
        {
            return new StartMenu();
        }
    }
    public class CreateEvent : Receiver
    {
        private DateTime CreateDate()
        {
            try
            {
                int year = ReadUInt("Enter year: ");
                int month = ReadUInt("Enter month: ");
                int day = ReadUInt("Enter day: ");
                return new DateTime(year, month, day);
            }
            catch (ArgumentOutOfRangeException)
            {
                return CreateDate();
            }
        }
        private EventBuilder chooseEvent(string name, int gNum, DateTime date)
        {
            while (true)
            {
                string mess = "1) Conference\n2) Party\n3) Corporate party\nSelect event type: ";
                int select = ReadUInt(mess);
                switch (select)
                {
                    case 1:
                        {
                            return new Conference(name, gNum, date);
                        }
                    case 2:
                        {
                            return new Party(name, gNum, date);
                        }
                    case 3:
                        {
                            return new CorporateParty(name, gNum, date);
                        }
                }
            }
        }

        public CreateEvent()
        {
            description = "Create new event";
        }
        public override void Exec()
        {
            Console.Clear();
            Console.Write("Enter name of event: ");
            string name = Console.ReadLine();
            int gNum = ReadUInt("Enter number of guests: ");
            DateTime date = CreateDate();
            EventConstructor ec = new EventConstructor();
            Event ev = ec.GenerateEvent(chooseEvent(name, gNum, date));
            EventAgency agency = EventAgency.Instance();
            agency.events.Add(ev);
        }
    }
    public class CreateEventCommand : MenuCommand
    {
        public CreateEventCommand()
        {
            receiver = new CreateEvent();
            isLeaf = true;
        }
        public override IMenuState Execute()
        {
            receiver.Exec();
            return new ManageEventMenu();
        }
    }
    public class Help : Receiver
    {
        public Help()
        {
            description = "Help me!";
        }
        public override void Exec()
        {
            Console.Clear();
            Console.WriteLine("This program is needed to create and pay for events for your company. " +
                "\nYou can generate an event to your taste and our professional organizer will hold" +
                "\nit at the highest level. The program has easy navigation: to create an event, " +
                "\ngo to the event management part and click 'create a new event'. " +
                "\nThere are also buttons to delete or change" +
                "\nIn start menu you can pay for your events.");
            MenuCommand.ReadKey();
        }
    }
    public class HelpCommand : MenuCommand 
    {
        public HelpCommand()
        {
            isLeaf = true;
            receiver = new Help();
        }
        public override IMenuState Execute()
        {
            receiver.Exec();
            return new StartMenu();
        }
    }
    public class ManageEvent : Receiver
    {
        public ManageEvent()
        {
            description = "Manage events";
        }
    }
    public class ManageEventCommand : MenuCommand
    {
        public ManageEventCommand()
        {
            receiver = new ManageEvent();
        }
        public override IMenuState Execute()
        {
            return new ManageEventMenu();
        }
    }
    public class PrintEvents : Receiver
    {
        public PrintEvents()
        {
            description = "Print all available events";
        }
        public override void Exec()
        {
            Console.Clear();
            EventAgency agency = EventAgency.Instance();
            agency.events.Print();
            if (agency.events.Count() == 0)
                Console.WriteLine("There're no available events");
            MenuCommand.ReadKey();
        }
    }
    public class PrintEventsCommand : MenuCommand
    {
        public PrintEventsCommand()
        {
            receiver = new PrintEvents();
            isLeaf = true;
        }
        public override IMenuState Execute()
        {
            receiver.Exec();
            return new ManageEventMenu();
        }
    }
    public class DeleteEvent : Receiver
    {
        public DeleteEvent()
        {
            description = "Delete event";
        }
        public override void Exec()
        {
            Console.Clear();
            EventAgency agency = EventAgency.Instance();
            if (agency.events.Count() != 0)
            {
                agency.events.Print();
                int id;
                string input;
                do
                {
                    Console.Write("Deleted id (b for go back): ");
                    if ((input = Console.ReadLine()) == "b")
                        break;
                } while (!int.TryParse(input, out id) || !agency.events.DeleteById(id));
            }
            else
            {
                Console.WriteLine("There're no available events.");
                MenuCommand.ReadKey();
            }

        }
    }
    public class DeleteEventCommand : MenuCommand
    {
        public DeleteEventCommand()
        {
            receiver = new DeleteEvent();
            isLeaf = true;
        }
        public override IMenuState Execute()
        {
            receiver.Exec();
            return new ManageEventMenu();
        }
    }
    public class UpdateEvent : Receiver
    {
        public UpdateEvent()
        {
            description = "Update event";
        }
        public override void Exec()
        {
            Console.Clear();
            EventAgency agency = EventAgency.Instance();
            if (agency.events.Count() != 0)
            {
                agency.events.Print();
                int id;
                string input;
                do
                {
                    Console.Write("Updated id (b for go back): ");
                    if ((input = Console.ReadLine()) == "b")
                        return;
                } while (!int.TryParse(input, out id) || !agency.events.IsEventById(id));
                UpdateEventChain chain = new UpdateEventChain(agency.events.GetEventById(id));
                Event e = chain.ClientCode();
                agency.events.SetEventById(id, e);
            }
            else
            {
                Console.WriteLine("There're no available events");
                MenuCommand.ReadKey();
            }

        }
    }
    public class UpdateEventCommand : MenuCommand
    {
        public UpdateEventCommand()
        {
            receiver = new UpdateEvent();
            isLeaf = true;
        }
        public override IMenuState Execute()
        {
            receiver.Exec();
            return new ManageEventMenu();
        }
    }
    public class PayForTheEvents : Receiver
    {
        public PayForTheEvents()
        {
            description = "Pay for the selected events";
        }
        public override void Exec()
        {
            Console.Clear();
            EventAgency agency = EventAgency.Instance();
            agency.SetClient();
            if (agency.events.cost != 0)
            {
                agency.events.Print();
                string answer;
                do
                {
                    Console.Write("Pay for the {0} events {1}$ (y/n): ", agency.events.Count(), agency.events.cost);
                    answer = Console.ReadLine();
                } while (answer != "y" && answer != "n");
                if (answer == "y")
                {
                    if (agency.client.PayForEvents())
                    {
                        agency.events.cost = 0;
                        Console.WriteLine("Success!!");                        
                    }
                    else
                    {
                        Console.WriteLine("Not enough money! Delete some events");
                    }
                }
            }
            else
            {
                Console.WriteLine("You already paid this events");                
            }
            MenuCommand.ReadKey();
        }
    }
    public class PayForTheEventsCommand : MenuCommand
    {
        public PayForTheEventsCommand()
        {
            receiver = new PayForTheEvents();
            isLeaf = true;
        }
        public override IMenuState Execute()
        {
            receiver.Exec();
            return new StartMenu();
        }
    }
}
