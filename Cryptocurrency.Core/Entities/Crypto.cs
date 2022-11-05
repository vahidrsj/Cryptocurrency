using Cryptocurrency.Core.ValueObject;

namespace Cryptocurrency.Core.Entities
{
    public class Crypto
    {
        public CryptoName CryptoName { get;protected set; }
        public List<Price> Prices { get; protected set; }

        public Crypto(CryptoName CryptoName)
        {
            this.CryptoName = CryptoName;
        }

        public void SetPrice(List<Price> prices)
        {
            Prices = prices;
        }

        public void AddPrice(Price price)
        {
            //check exist items
            Prices.Add(price);
        }
    }
}
