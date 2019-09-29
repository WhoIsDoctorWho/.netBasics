using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Console
{
    public class EventAgency
    {
        private static EventAgency _instance;
        protected EventAgency(){}        
        public EventList events = new EventList();       
        public IClient client = null;
        
        public static EventAgency Instance()
        {
            if (_instance == null)
            {
                _instance = new EventAgency();
            }
            return _instance;
        }
        public void SetClient()
        {
            if (client == null)
            {
                client = ClientFactory.FactoryMethod();                
            }
        }
    }
}
