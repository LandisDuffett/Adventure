using System;
using System.Collections.Generic;

namespace Adventure.Models
{
  class Game
  {
    List<Item> Match { get; set; }
    public Room current { get; set; }
    public bool way { get; set; }
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
      current = home;
      Console.WriteLine(home.Description);
      string move = Console.ReadLine();
      Next(move.ToLower());
    }

    public void Next(string move)
    {
      string verb = move.Split(" ")[0];
      switch (verb)
      {
        case "take":
          string item = move.Split(" ")[1].ToLower();
          Match = current.Items.FindAll(i => i.Name == item);
          if (Match.Count > 0)
          {
            currPlayer.Inventory.Add(Match[0]);
            current.Items.Remove(Match[0]);
          }
          else
          {
            Console.WriteLine($"There is no {item} here.")
          }
          break;
        case "use":
          item = move.Split(" ")[1];
          Match = currPlayer.Inventory.FindAll(i => i.Name == item);
          if (item.ToLower() == "key" && current == northRoom && Match.Count > 0)
          {
            current = northeastRoom;
            Console.WriteLine(northeastRoom.Description);
            string nextMove = Console.ReadLine();
            Next(nextMove);
          }
          else if (Match.Count == 0)
          {
            Console.WriteLine($"You do not have a(n) {item}");
          }
          else
          {
            Console.WriteLine($"That has no use here.")
          }
          break;
        case "go":
          item = move.Split(" ")[1].ToLower();
          way = current.Passageways.ContainsKey(item);
          if (way && !current.Passageways[item].Locked)
          {
            current = current.Passageways[item]
            Console.WriteLine(current.Description)
            move = Console.ReadLine()
            Next(move);
          }
          else if (way && current.Passageways[item].Locked)
          {
            Console.WriteLine("The door is locked.")
            move = Console.ReadLine();
            Next(move);
          }
          else
          {
            Console.WriteLine("How does that wall taste?");
            move = Console.ReadLine();
            Next(move);
          }
          break;
        case "see":
        default:
          Console.WriteLine(@"
Your commands are:
go <direction> = move your character to the next room 
                 in that direction
take <item> = take any item available in a room and  
              add it to your inventory
use <item>  = use any item for its intended and 
              obvious purpose
see inventory = see all items currently in your
                possession");
          break;
      }
      string direction = move.Split(" ")[1];
      for (int i = 0; i < current.Doors.Count; i++)
      {
        if (direction == current.Doors[i] && !current.Locked)
        {
          if
        }
      }
      if (direction == )
    }

    public Game()
    {
      key = new Item("key");
      sword = new Item("sword");
      home = new Room("home", new List<Item> { }, new Dictionary<string, Room> { { "south", southRoom }, { "north", northRoom } }, "You stand alone in a dark cave lit only by a single candle. From the room to the South you detect a putrefying and acrid odor. Another passage leads to a somewhat brighter room to the north. Enter your command: ", false);
      southRoom = new Room("south", new List<Item> { sword }, new Dictionary<string, Room> { { "north", home }, { "west", southwestRoom } }, "The rancid odor has grown stronger. The fumes are overwhelming. The odor seems to originate from the room to the west. Remember that where there is danger, there is often great opportunity.", false);
      southwestRoom = new Room("southwest", new List<Item> { }, new Dictionary<string, Room> { { "east", southRoom } }, "You enter the dark room with the rancid smell. You immediately fall into a vat of boiling sulfur and die.", false);
      northRoom = new Room("north", new List<Item> { key }, new Dictionary<string, Room> { { "south", home }, { north, northnorthRoom } }, "On a table lies a golden key guarded by a tiny but fierce kitten named 'Muffinberry'. There is a dark cave leading north and a door to a room to the east, but the door is locked.", false);
      northnorthRoom = new Room("northnorth", new List<Item> { key }, new Dictionary<string, Room> { { "south", northRoom }, { "east", northeastRoom } }, "The room is completely dark. There are no other exits besides the passage through which you entered.", false);
      northeastRoom = new Room("northeast", new List<Item> { key }, new Dictionary<string, Room> { { "west", northnorthRoom } }, "You unlock the door and enter the room to find an enormous horde of treasure and a dead, decaying dragon beside it. You win!", true);
    }
  }

}