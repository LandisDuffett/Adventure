namespace Adventure.Models
{
  class Item
  {
    public string Name { get; set; }

    public Item(string name)
    {
      Name = name;
    }

    Item key = new Item("key");
    Item sword = new Item("sword");

  }
}
