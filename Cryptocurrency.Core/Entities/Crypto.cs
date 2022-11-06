using Cryptocurrency.Core.ValueObject;
using System.Diagnostics;

namespace Cryptocurrency.Core.Entities
{
    public class Crypto
    {
        public CryptoName CryptoName { get; protected set; }
        public List<Price> Prices { get; protected set; }

        public Crypto(CryptoName CryptoName)
        {
            this.CryptoName = CryptoName;
            Prices = new List<Price>();
        }

        public void SetPrice(List<Price> prices)
        {
            if (prices == null || prices.Count == 0)
                throw new ArgumentNullException(nameof(prices));

            Prices = prices;
        }

        public void SetPrice(Dictionary<string, decimal> prices)
        {
            if (prices == null || prices.Count == 0)
                throw new ArgumentNullException(nameof(prices));

            foreach (var price in prices)
            {
                AddPrice(new Price().Create(price.Key, price.Value));
            }
        }

        public void AddPrice(Price price)
        {
            if (string.IsNullOrEmpty(price.Currency) || price.Value <= 0)
                throw new ArgumentNullException(nameof(price));

            Prices.Add(price);
        }

        public void AddPriceRage(List<Price> price)
        {
            if (price == null || price.Count == 0)
                throw new ArgumentNullException(nameof(price));

            Prices.AddRange(price);
        }

    }
}
