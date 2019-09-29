using System;
using System.Collections.Generic;
using System.Linq;

namespace Course_Console
{
    // Command Manager & StateObject
    public class Menu
    {
        IMenuState MenuState { get; set; }

        List<MenuCommand> commands;
        Stack<MenuCommand> commandsHistory;
        public Menu()
        {
            commands = new List<MenuCommand>();
            commandsHistory = new Stack<MenuCommand>();
            commands.Add(new StartCommand());
            Execute(0);
            MenuState.CreateMenu(commands);

            Run();
        }
        public void Run()
        {
            bool run = true;
            do
            {
                MenuState.CreateMenu(commands); // изменяем меню в зависимости от состояния
                WriteMenu();
                string input = Console.ReadLine();
                if (input == "b")
                {
                    GoBack();
                    continue;
                }
                else if (input == "e")
                {
                    return;
                }
                int key;
                if (int.TryParse(input, out key) && commands.ElementAtOrDefault(key - 1) != null)
                {
                    Execute(key - 1); // first element has zero index                                        
                }
            } while (run);
        }
        public void Execute(MenuCommand command)
        {
            MenuState = command.Execute();
            if (!command.isLeaf)
                commandsHistory.Push(command); // добавляем выполненную команду в историю команд (если не листовая)
        }
        public void Execute(int number)
        {
            Execute(commands[number]);
        }
        public void WriteMenu()
        {
            Console.Clear();
            int i = 1;
            foreach (MenuCommand com in commands)
            {
                Console.WriteLine(i++ + ") " + com.GetDescription());
            }
            Console.WriteLine("b) Go Back");
            Console.WriteLine("e) Exit");
        }
        public void GoBack()
        {
            if (commandsHistory.Count > 1) // у нас есть более двух элементов на стеке (так как стартовое меню - корень)
            {
                commandsHistory.Pop(); // remove current command
                Execute(commandsHistory.Pop()); // take prev command and execute it                
            }
        }
    }
}


