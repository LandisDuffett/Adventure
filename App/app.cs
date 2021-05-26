using System;
using Adventure.Models;

namespace Adventure
{
  class App
  {
    Game game;

    Boolean active;

    public void Run()
    {
      game = new Game();
      active = true;
      while (active)
      {
        Console.Clear();
        Console.WriteLine(@"
Welcome to Adventure!

What would you like to do?
C = see all commands
S = start your quest
Q = quit the game");
        string choice = Console.ReadLine();
        switch (choice.ToLower())
        {
          case "c":
            Console.Clear();
            Console.WriteLine(@"
You may enter any of these commands at any time during the game:
show commands = list all possible commands
go <direction> = move your character to the next room 
                 in that direction
take <item> = take any item available in a room and  
              add it to your inventory
use <item>  = use any item for its intended and 
              obvious purpose
see inventory = see all items currently in your 
                possession
quit game   = quit the game");
            Console.WriteLine(@"
Enter any key to continue.");
            Console.ReadLine();
            break;
          case "s":
            game.Start();
            break;
          case "q":
            active = false;
            break;
          default:
            Console.WriteLine("Invalid Selection. Enter any key to go back.");
            Console.ReadLine();
            Run();
            break;
        }
      }
    }
  }
}