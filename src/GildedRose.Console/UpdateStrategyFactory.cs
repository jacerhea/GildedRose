namespace GildedRose.Console
{
    public class UpdateStrategyFactory : IUpdateStrategyFactory
    {
        public IUpdateStrategy Create(string name)
        {
            if (name == "Sulfuras, Hand of Ragnaros")
            {
                return new DoNothingUpdate();
            }
            else if (name == "Aged Brie")
            {
                return new AgedBrieUpdateStrategy();
            }
            else if (name == "Backstage passes to a TAFKAL80ETC concert")
            {
                return new BackstagePassesUpdateStrategy();
            }
            else if (name.Contains("Conjured"))
            {
                return new StandardUpdateStrategy(2);
            }
            else
            {
                return new StandardUpdateStrategy();
            }
        }
    }

    public class StandardUpdateStrategy : IUpdateStrategy
    {
        private readonly int _factor;

        public StandardUpdateStrategy(int factor = 1)
        {
            _factor = factor;
        }

        public void UpdateQuality(Item item)
        {
            item.SellIn--;
            if (item.Quality > 0)
            {
                item.Quality -= _factor * (item.SellIn < 0 ? 2 : 1);
            }
            if (item.Quality < 0)
            {
                item.Quality = 0;
            }
        }
    }

    public class BackstagePassesUpdateStrategy : IUpdateStrategy
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn--;
            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
            else if (item.SellIn <= 5)
            {
                item.Quality = item.Quality + 3;
            }
            else if (item.SellIn <= 10)
            {
                item.Quality = item.Quality + 2;
            }
            else if (item.Quality < 50)
            {
                item.Quality++;
            }
        }
    }

    public class AgedBrieUpdateStrategy : IUpdateStrategy
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn--;
            if (item.Quality < 50)
            {
                item.Quality++;
            }
        }
    }


    public class DoNothingUpdate : IUpdateStrategy
    {
        public void UpdateQuality(Item item) { }
    }

    public interface IUpdateStrategyFactory
    {
        IUpdateStrategy Create(string name);
    }

    public interface IUpdateStrategy
    {
        void UpdateQuality(Item item);
    }
}