using System;
using System.Collections.Generic;
using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        [Fact]
        public void TestUpdateOnZeroZeroItem()
        {
            var resultAndStrategyPair = new List<Tuple<Item, IUpdateStrategy>>
            {
                Tuple.Create(new Item {Quality = 0, SellIn = 0}, (IUpdateStrategy)new DoNothingUpdate()),
                Tuple.Create(new Item {Quality = 1, SellIn = -1}, (IUpdateStrategy)new AgedBrieUpdateStrategy()),
                Tuple.Create(new Item {Quality = 0, SellIn = -1}, (IUpdateStrategy)new BackstagePassesUpdateStrategy()),
                Tuple.Create(new Item {Quality = 0, SellIn = -1}, (IUpdateStrategy)new StandardUpdateStrategy(2)),
                Tuple.Create(new Item {Quality = 0, SellIn = -1}, (IUpdateStrategy)new StandardUpdateStrategy()),
            };

            foreach (var itemPair in resultAndStrategyPair)
            {
                var testCase = new Item { Quality = 0, SellIn = 0 };
                itemPair.Item2.UpdateQuality(testCase);
                Assert.Equal(itemPair.Item1.Quality, testCase.Quality);
                Assert.Equal(itemPair.Item1.SellIn, testCase.SellIn);
            }
        }

        [Fact]
        public void TestUpdateOnTenTenItem()
        {
            var resultAndStrategyPair = new List<Tuple<Item, IUpdateStrategy>>
            {
                Tuple.Create(new Item {Quality = 10, SellIn = 10}, (IUpdateStrategy)new DoNothingUpdate()),
                Tuple.Create(new Item {Quality = 11, SellIn = 9}, (IUpdateStrategy)new AgedBrieUpdateStrategy()),
                Tuple.Create(new Item {Quality = 12, SellIn = 9}, (IUpdateStrategy)new BackstagePassesUpdateStrategy()),
                Tuple.Create(new Item {Quality = 8, SellIn = 9}, (IUpdateStrategy)new StandardUpdateStrategy(2)),
                Tuple.Create(new Item {Quality = 9, SellIn = 9}, (IUpdateStrategy)new StandardUpdateStrategy()),
            };

            foreach (var itemPair in resultAndStrategyPair)
            {
                var testCase = new Item { Quality = 10, SellIn = 10 };
                itemPair.Item2.UpdateQuality(testCase);
                Assert.Equal(itemPair.Item1.Quality, testCase.Quality);
                Assert.Equal(itemPair.Item1.SellIn, testCase.SellIn);
            }
        }

        [Fact]
        public void TestUpdateOnProvidedValues()
        {
            var items = new List<Tuple<Item, Item, IUpdateStrategy>>
            {
                Tuple.Create(new Item {Quality = 19, SellIn = 9}, new Item {Name = "+5 Dexterity Vest", Quality = 20, SellIn = 10}, (IUpdateStrategy) new StandardUpdateStrategy()),
                Tuple.Create(new Item {Quality = 1, SellIn = 1}, new Item {Name = "Aged Brie", SellIn = 2, Quality = 0}, (IUpdateStrategy) new AgedBrieUpdateStrategy()),
                Tuple.Create(new Item {Quality = 6, SellIn = 4}, new Item {Name = "Elixir of the Mongoose", Quality = 7, SellIn = 5}, (IUpdateStrategy) new StandardUpdateStrategy()),
                Tuple.Create(new Item {Quality = 80, SellIn = 0}, new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80}, (IUpdateStrategy) new DoNothingUpdate()),
                Tuple.Create(new Item {Quality = 21, SellIn = 14}, new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 20, SellIn = 15}, (IUpdateStrategy) new BackstagePassesUpdateStrategy()),
                Tuple.Create(new Item {Quality = 4, SellIn = 2}, new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}, (IUpdateStrategy) new StandardUpdateStrategy(2))
            };

            foreach (var itemPair in items)
            {
                itemPair.Item3.UpdateQuality(itemPair.Item2);
                Assert.Equal(itemPair.Item1.Quality, itemPair.Item2.Quality);
                Assert.Equal(itemPair.Item1.SellIn, itemPair.Item2.SellIn);
            }
        }
    }
}