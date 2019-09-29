using System;

namespace Course_Console
{
    public class UpdateEventChain
    {
        public Event ev;
        AbstractHandler handler;
        public UpdateEventChain(Event ev)
        {
            this.ev = ev;

            handler = new RockBandHandler();            
            handler.SetNext(new CoffeeBreakHandler()).SetNext(new FakirActionHandler()); // creating of the chain            
        }
        public Event ClientCode()
        {
            return handler.Handle(ev);
        }
    }
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        Event Handle(Event request);
    }
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;
        public string GetAnswer()
        {
            string answer;
            do
            {
                Console.Write("Do you want it?(y/n) ");
                answer = Console.ReadLine();
            } while (answer != "y" && answer != "n");
            return answer;
        }

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual Event Handle(Event request)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(request);
            }
            else
            {
                return request;
            }
        }
    }

    public class RockBandHandler : AbstractHandler
    {
        public override Event Handle(Event request)
        {            
            Console.Clear();
            Console.WriteLine(RockBand.DescribeActivity());
            string answer = GetAnswer();
            if (answer == "y")           
                request = new RockBand(request); // create decorator, return            
            return base.Handle(request);
        }
    }
    public class CoffeeBreakHandler : AbstractHandler
    {
        public override Event Handle(Event request)
        {            
            Console.Clear();
            Console.WriteLine(CoffeeBreak.DescribeActivity());
            string answer = GetAnswer();
            if (answer == "y")
                request = new CoffeeBreak(request);            
            return base.Handle(request);
        }
    }
    public class FakirActionHandler : AbstractHandler
    {
        public override Event Handle(Event request)
        {
            Console.Clear();
            Console.WriteLine(FakirAction.DescribeActivity());
            string answer = GetAnswer();
            if (answer == "y")
                request = new FakirAction(request);            
            return base.Handle(request);
        }
    }
}
