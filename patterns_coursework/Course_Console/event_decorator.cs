using System;

namespace Course_Console
{
    public abstract class EventDecorator : Event
    {
        public Event ev;
        public override string eventName { get { return ev.eventName; } }
        public override string type { get { return ev.type; } }
        public override string description
        {
            get
            {
                return ev.description + "\n" + GetSpecificDescription();
            }
        }
        public override DateTime date { get { return ev.date; } }
        public override string mainOrg { get { return ev.mainOrg; } }
        public override int numGuests { get { return ev.numGuests; } }
        public override int cost { get { return ev.cost + GetSpecificCost(); } }
        public override string place { get { return ev.place; } }
        public override int id { get { return ev.id; } }
        public EventDecorator(Event ev)
        {
            this.ev = ev;
        }
        abstract public int GetSpecificCost();
        abstract public string GetSpecificDescription();
    }
    public class RockBand : EventDecorator
    {
        private static string specification = "Hey! Want to add a drive to the event?\n" +
                "Do you want to light the audience?\n" +
                "Then this is for you! The best rock artists will " +
                "have an unforgettable performance on stage!\n";
        private static int price = 3000;
        public RockBand(Event ev) : base(ev)
        {
        }
        public static string DescribeActivity()
        {
            return specification + "\nIts cost: " + price;
        }
        public override string GetSpecificDescription()
        {
            return specification;
        }
        public override int GetSpecificCost()
        {
            return price;
        }
    }
    public class CoffeeBreak : EventDecorator
    {
        private static string specification = "What can be better than small cup of coffee?\n" +
                "Only big cuo of coffee!\n" +
                "We have a lot of coffe for all guests!\n" +
                "Coffee Coffee Coffee!\n";
        private static int price = 1000;
        public CoffeeBreak(Event ev) : base(ev)
        {
        }
        public static string DescribeActivity()
        {
            return specification + "\nIts cost: " + price;
        }
        public override string GetSpecificDescription()
        {
            return specification;
        }
        public override int GetSpecificCost()
        {
            return price;
        }
    }
    public class FakirAction : EventDecorator
    {
        private static string specification = "Do you afraid of fire?\n" +
                "This man - lord of fire!\n" +
                "He really knows how make the guest's hearts tremble!\n" +
                "Don't be afraid..\n";
        private static int price = 1500;
        public FakirAction(Event ev) : base(ev)
        {
        }
        public static string DescribeActivity()
        {
            return specification + "\nIts cost: " + price;
        }
        public override string GetSpecificDescription()
        {
            return specification;
        }
        public override int GetSpecificCost()
        {
            return price;
        }
    }

}
