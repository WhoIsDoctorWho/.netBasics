using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Console
{
    public interface IMenuState
    {
        void CreateMenu(List<MenuCommand> commands); // создаём меню в зависимости от состояния
    }
    // наши состояния
    // конструкторы состояний создают доступные методы(команды) для вызова 
    // в каждом методе - изменение состояния/вызов функции(у листов)
    // у менеджера команды есть метод run(), который принимает ввод
    public class StartMenu : IMenuState
    {
        // we can 1) Buy event 2) Get Info 3) help 4) Exit
        public void CreateMenu(List<MenuCommand> commands)
        {
            commands.Clear();
            commands.Add(new PayForTheEventsCommand());
            commands.Add(new ManageEventCommand());
            commands.Add(new HelpCommand());            
        }

    }    
    public class ManageEventMenu : IMenuState
    {
        // we can 1) Сhoose type  2) Choose cost 3) help 4) Exit        
        public void CreateMenu(List<MenuCommand> commands)
        {
            commands.Clear();
            commands.Add(new CreateEventCommand());
            commands.Add(new PrintEventsCommand());
            commands.Add(new DeleteEventCommand());
            commands.Add(new UpdateEventCommand());
        }

    }
    public class EventMenu : IMenuState
    {
        // we can 1) Сhoose type  2) Choose cost 3) help 4) Exit        
        public void CreateMenu(List<MenuCommand> commands)
        {
            commands.Clear();
            commands.Add(new CreateEventCommand());
        }

    }
}
