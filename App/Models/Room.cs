using System;
using System.Collections.Generic;

namespace Adventure.Models
{
  class Room
  {
    public List<Item> Items { get; set; }
    public List<string> Passageways { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Locked { get; set; }

    public Room(string name, List<Item> items, List<string> passageways, string description, bool locked)
    {
      Name = name;
      Items = items;
      Passageways = passageways;
      Description = description;
      Locked = locked;
    }
  }
}