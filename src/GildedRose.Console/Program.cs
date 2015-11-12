using System.Collections.Generic;
using static System.Console;

namespace GildedRose.Console
{
    class Program
    {
        private readonly IUpdateStrategyFactory _strategyFactory = new UpdateStrategyFactory();
        IList<Item> Items;
        static void Main(string[] args)
        {
            WriteLine("OMGHAI!");

            var app = new Program
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            };

            app.UpdateQuality();

            ReadKey();

        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                var updateStrategy = _strategyFactory.Create(item.Name);
                updateStrategy.UpdateQuality(item);
            }
        }
    }
}
