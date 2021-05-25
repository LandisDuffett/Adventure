using System.Collections.Generic;

namespace Adventure.Models
{
  class Player
  {
    public List<Item> Inventory { get; set; }
    public string Name { get; set; }
    public int Health { get; set; }

    public bool Dead { get => Health <= 0; }

    public Player(string name)
    {
      Inventory = new List<Item>();
      Name = name;
      Health = 10;
    }
  }
}