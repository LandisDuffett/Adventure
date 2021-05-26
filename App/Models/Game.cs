using System;
using System.Collections.Generic;

namespace Adventure.Models
{
  class Game
  {
    List<Item> Match { get; set; }
    List<Room> AllRooms { get; set; }
    public Room current { get; set; }
    public Room nextRoom { get; set; }
    public bool running { get; set; }
    public Item key { get; set; }
    public Item sword { get; set; }
    public Player currPlayer { get; set; }
    public Room home { get; set; }
    public Room southRoom { get; set; }
    public Room southwestRoom { get; set; }
    public Room northRoom { get; set; }
    public Room northnorthRoom { get; set; }
    public Room northeastRoom { get; set; }

    public void Start()
    {
      Console.Clear();
      Console.WriteLine(@"
Welcome to Adventure!
Enter your player name:"
);
      string name = Console.ReadLine();
      currPlayer.Name = name;
      current = AllRooms.Find(r => r.Name == "home");
      Console.WriteLine(current.Description);
      Console.WriteLine($"What would you like to do, {name}?");
      Console.WriteLine(@"
You may enter any of these commands at any time:
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
      string move = Console.ReadLine();
      if (move == "quit game")
      {
        Console.WriteLine($"You shall forever be known as {name}, the coward.");
        Environment.Exit(0);
        return;
      }
      else
      {
        Next(move.ToLower());
      }
    }
    public void Next(string move)
    {
      string verb = move.Split(" ")[0];
      switch (verb)
      {
        case "show":
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
          move = Console.ReadLine();
          Next(move.ToLower());
          break;
        case "take":
          string item = move.Split(" ")[1].ToLower();
          Match = current.Items.FindAll(i => i.Name == item);
          if (Match.Count > 0)
          {
            currPlayer.Inventory.Add(Match[0]);
            current.Items.Remove(Match[0]);
            Console.WriteLine($"You now have a(n) {item}.");
            move = Console.ReadLine();
            Next(move.ToLower());
          }
          else
          {
            Console.WriteLine($"There is no {item} here.");
            move = Console.ReadLine();
            Next(move.ToLower());
          }
          break;
        case "use":
          item = move.Split(" ")[1];
          Match = currPlayer.Inventory.FindAll(i => i.Name == item);
          if (item.ToLower() == "key" && current.Name == "northnorthRoom" && Match.Count > 0)
          {
            AllRooms[5].Locked = false;
            string nextMove = Console.ReadLine();
            Next(nextMove);
          }
          else if (Match.Count == 0)
          {
            Console.WriteLine($"You do not have a(n) {item}");
            move = Console.ReadLine();
            Next(move.ToLower());
          }
          else
          {
            Console.WriteLine($"That has no use here.");
            move = Console.ReadLine();
            Next(move.ToLower());
          }
          break;
        case "go":
          string direction = move.Split(" ")[1].ToLower();
          string nxtRm = "";
          for (int i = 0; i < current.Passageways.Count; i++)
          {
            if (current.Passageways[i] == direction)
            {
              nxtRm = current.Passageways[i + 1];
            }
            else
            {
              continue;
            }
          }
          if (nxtRm != "")
          {
            for (int y = 0; y < AllRooms.Count; y++)
            {
              if (AllRooms[y].Name == nxtRm)
              {
                nextRoom = AllRooms[y];
              }
              else
              {
                continue;
              }
            }
            if (!nextRoom.Locked)
            {
              current = nextRoom;
              if (current.Name == "northeastRoom" || current.Name == "southwestRoom")
              {
                Console.WriteLine(current.Description);
                End();
              }
              else
              {
                Console.WriteLine(current.Description);
                if (current.Items.Count > 0)
                {
                  Console.WriteLine($"Items in this room: {current.Items[0].Name}");
                  move = Console.ReadLine();
                  Next(move);
                }
                else
                {
                  Console.WriteLine("No items in this room.");
                  move = Console.ReadLine();
                  Next(move);
                }
              }
            }
            else if (nextRoom.Locked)
            {
              Console.WriteLine("The door is locked.");
              move = Console.ReadLine();
              Next(move);
            }
          }
          else if (direction == "north" || direction == "south" || direction == "east" || direction == "west")
          {
            Console.WriteLine("How does that wall taste?");
            move = Console.ReadLine();
            Next(move);
          }
          else
          {
            Console.WriteLine("You must enter 'go' + a direction ('north', 'east', 'south', or 'west').");
            move = Console.ReadLine();
            Next(move);
          }
          break;
        case "see":
          for (int i = 0; i < currPlayer.Inventory.Count; i++)
          {
            Console.WriteLine(currPlayer.Inventory[i].Name);
          }
          move = Console.ReadLine();
          Next(move);
          break;
        case "quit":
          Environment.Exit(0);
          return;
        default:
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
          move = Console.ReadLine();
          Next(move);
          break;
      }
    }
    public void End()
    {
      Console.WriteLine(@"
What would you like to do? 
- quit (q)
- play again (p)");
      string move = Console.ReadLine();
      move = move.ToLower();
      switch (move)
      {
        case "q":
          Environment.Exit(0);
          return;
        case "p":
          Start();
          break;
        default:
          Console.WriteLine("Invalid selection.");
          do
          {
            Console.WriteLine(@"
What would you like to do? 
- quit (q)
- play again (p)");
            move = Console.ReadLine();
            move = move.ToLower();
          }
          while (move != "q" && move != "p");
          break;
      }
    }
    public Game()
    {
      currPlayer = new Player("");
      key = new Item("key");
      sword = new Item("sword");
      AllRooms = new List<Room>() { new Room("home", new List<Item> { }, new List<string> { "south", "southRoom", "north", "northRoom" }, "You stand alone in a dark cave lit only by a single candle. From the room to the South you detect a putrefying and acrid odor. Another passage leads to a somewhat brighter room to the north. Enter your command: ", false), new Room("southRoom", new List<Item> { sword }, new List<string> { "north", "home", "west", "southwestRoom" }, "The rancid odor has grown stronger. The fumes are overwhelming. The odor seems to originate from the room to the west. Remember that where there is danger, there is often great opportunity.", false), new Room("southwestRoom", new List<Item> { }, new List<string> { "east", "southRoom" }, "You enter the dark room with the rancid smell. You immediately fall into a vat of boiling sulfur and die.", false), new Room("northRoom", new List<Item> { key }, new List<string> { "south", "home", "north", "northnorthRoom" }, "There may be something on the table in the center of this room, guarded by a tiny but fierce kitten named 'Muffinberry'. There is a dark cave leading north.", false), new Room("northnorthRoom", new List<Item> { }, new List<string> { "south", "northRoom", "east", "northeastRoom" }, "The room is completely dark. There are no other exits besides the passage through which you entered and a room to the east, but the door is locked.", false), new Room("northeastRoom", new List<Item> { key }, new List<string> { "west", "northnorthRoom" }, "You unlock the door and enter the room to find an enormous horde of treasure and a dead, decaying dragon beside it. You win!", true) };
    }
  }
}

