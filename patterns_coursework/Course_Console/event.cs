using System;
using System.Collections.Generic;
using System.Linq;

namespace Course_Console
{
    public abstract class IEvent
    {
        public IEvent() { }
        public virtual int Count()
        {
            return 0;
        }
        public virtual void Add(IEvent ev) { }
        public virtual void SetEventById(int id, IEvent ev) { }
        public virtual bool DeleteById(int id)
        {
            return false;
        }
        public virtual void Print() { }
        public virtual bool IsEventById(int id)
        {
            return false;
        }
        public virtual Event GetEventById(int id)
        {
            return null;
        }
        public abstract string description { get; set; }
        public abstract int cost { get; set; }

    }
    public class EventList : IEvent
    {
        private List<Event> events;

        public EventList()
        {
            events = new List<Event>();
        }
        public override string description
        {
            get
            {
                string description = "";
                foreach (Event ev in events)
                {
                    description += ev.description + " ";
                }
                return description;
            }
            set { description = value; }
        }
        public override int cost
        {
            get
            {
                int cost = 0;
                foreach (Event ev in events)
                {
                    cost += ev.cost;
                }
                return cost;
            }
            set
            {        // we set only to the zero        
                foreach (Event ev in events)
                {
                    ev.cost = value;
                }
            }
        }
        public override void Print()
        {
            Console.Clear();
            foreach (Event ev in events)
            {
                ev.Print();
            }
            Console.WriteLine("===============================================");
        }
        public override int Count()
        {
            return events.Count;
        }
        public override void Add(IEvent ev)
        {
            events.Add((Event)ev);
        }
        private int GetIndexById(int id)
        {
            return events.FindIndex(x => x.id == id);
        }
        private bool Remove(int index)
        {
            try
            {
                events.RemoveAt(index);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }

        }
        public override bool DeleteById(int id)
        {
            bool isEvent = IsEventById(id);
            return isEvent ? Remove(GetIndexById(id)) : false;
        }
        public override bool IsEventById(int id)
        {
            return GetIndexById(id) != -1;
        }
        public override Event GetEventById(int id)

        {
            return IsEventById(id) ? events.ElementAt(GetIndexById(id)) : null;
        }
        public override void SetEventById(int id, IEvent ev)
        {
            int index = GetIndexById(id);
            events[index] = (Event)ev;
        }


    }
    public class Event : IEvent
    {
        public virtual string eventName { get; set; }
        public virtual string type { get; set; }
        public override string description { get; set; }
        public virtual DateTime date { get; set; }
        public virtual string mainOrg { get; set; }
        public virtual int numGuests { get; set; }
        public override int cost { get; set; }
        public virtual string place { get; set; }
        public virtual int id { get; set; }

        public static int nextId = 0;

        public Event() { }
        public override void Print()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("===============================================");
            Console.WriteLine(type + " " + eventName + "(" + date.ToShortDateString() + ")" + " id: " + id);
            Console.WriteLine("Main organizer: " + mainOrg);
            Console.WriteLine("For " + numGuests + " in " + place);
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("About event: " + description + "\nCost " + cost);
        }

    }

    public abstract class EventBuilder
    {
        private Event ev = new Event();
        protected string name;
        protected DateTime date;
        protected int nGuests;
        public EventBuilder(string name, int nGuests, DateTime date)
        {
            this.name = name;
            this.nGuests = nGuests;
            this.date = date;
        }
        public Event GetEvent()
        {
            return ev;
        }

        public void SetEventName()
        {
            GetEvent().eventName = name;
        }
        public void SetDate()
        {
            GetEvent().date = date;
        }
        public void SetGuestsNum()
        {
            GetEvent().numGuests = nGuests;
        }
        public void SetId()
        {
            GetEvent().id = Event.nextId++;
        }
        public abstract void SetMainOrg();
        public abstract void SetType();
        public abstract void SetDescription();
        public abstract void SetCost();
        public abstract void SetPlace();
    }

    public class EventConstructor
    {
        public Event GenerateEvent(EventBuilder eBuilder)
        {
            eBuilder.SetId();
            eBuilder.SetDate();
            eBuilder.SetDescription();
            eBuilder.SetCost();
            eBuilder.SetType();
            eBuilder.SetEventName();
            eBuilder.SetMainOrg();
            eBuilder.SetGuestsNum();
            eBuilder.SetPlace();
            return eBuilder.GetEvent();
        }
    }
    //concrete builders    
    public class Conference : EventBuilder
    {
        public Conference(string name, int nGuests, DateTime date) : base(name, nGuests, date) { }
        public override void SetCost()
        {
            GetEvent().cost = 8000;
        }
        public override void SetDescription()
        {
            GetEvent().description = "Conference for your company. Table, drinks...";
        }
        public override void SetMainOrg()
        {
            GetEvent().mainOrg = "Tanya";
        }
        public override void SetPlace()
        {
            GetEvent().place = "Event hall";
        }
        public override void SetType()
        {
            GetEvent().type = GetType().Name;
        }
    }
    public class Party : EventBuilder
    {
        public Party(string name, int nGuests, DateTime date) : base(name, nGuests, date) { }

        public override void SetCost()
        {
            GetEvent().cost = 15000;
        }
        public override void SetDescription()
        {
            GetEvent().description = "Cool party at the beach";
        }
        public override void SetMainOrg()
        {
            GetEvent().mainOrg = "Andrey";
        }
        public override void SetPlace()
        {
            GetEvent().place = "Olmeca beach";
        }
        public override void SetType()
        {
            GetEvent().type = GetType().Name;
        }
    }
    public class CorporateParty : EventBuilder
    {
        public CorporateParty(string name, int nGuests, DateTime date) : base(name, nGuests, date) { }

        public override void SetCost()
        {
            GetEvent().cost = 10000;
        }
        public override void SetDescription()
        {
            GetEvent().description = "A lot of eat and alcohol";
        }
        public override void SetMainOrg()
        {
            GetEvent().mainOrg = "Ivan";
        }
        public override void SetPlace()
        {
            GetEvent().place = "Bambook party-place";
        }
        public override void SetType()
        {
            GetEvent().type = GetType().Name;
        }
    }
}
