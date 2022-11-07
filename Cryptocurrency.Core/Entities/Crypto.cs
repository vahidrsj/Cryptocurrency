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
            if (prices == null)
                throw new ArgumentNullException(nameof(prices));
            else if (prices.Count == 0)
                throw new InvalidDataException(nameof(prices));

            Prices = prices;
        }

        public void SetPrice(Dictionary<string, decimal> prices)
        {
            if (prices == null)
                throw new ArgumentNullException(nameof(prices));
            else if (prices.Count == 0)
                throw new InvalidDataException(nameof(prices));

            foreach (var price in prices)
            {
                AddPrice(new Price().Create(price.Key, price.Value));
            }
        }

        public void AddPrice(Price price)
        {
            if (string.IsNullOrEmpty(price.Currency))
                throw new ArgumentNullException(nameof(price));
            else if (price.Value <= 0)
                throw new InvalidDataException(nameof(price));
            
            Prices.Add(price);
        }
    }
}
