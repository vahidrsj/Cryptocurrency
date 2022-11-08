using Cryptocurrency.Core.Entities;
using Cryptocurrency.Core.ValueObject;

namespace Cryptocurrency.Core.Tests.Entities
{
    public class CryptoTests
    {
        private readonly string cryptoName = "Bitcoin";
        private readonly string cryptoSymbol = "BTC";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Crypto_Create_CryptoName_Done_Successfully()
        {
            var crypto = new Crypto(new CryptoName(cryptoName, cryptoSymbol));

            Assert.IsNotNull(crypto);
            Assert.That(crypto.CryptoName.Name, Is.EqualTo(cryptoName));
            Assert.That(crypto.CryptoName.Symbol, Is.EqualTo(cryptoSymbol));
            Assert.That(crypto.Prices, Is.Empty);
        }

        [Test]
        public void Crypto_Create_Done_Successfully()
        {
            var prices = new Dictionary<string, decimal>();
            prices.Add("USD", (decimal)123.456);
            prices.Add("EUR", (decimal)123.456);

            var crypto = new Crypto(new CryptoName(cryptoName, cryptoSymbol));
            crypto.SetPrice(prices);

            Assert.IsNotNull(crypto);
            Assert.That(crypto.CryptoName.Name, Is.EqualTo(cryptoName));
            Assert.That(crypto.CryptoName.Symbol, Is.EqualTo(cryptoSymbol));
            Assert.IsNotEmpty(crypto.Prices);
        }

        [Test]
        public void Crypto_CryptoName_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => { Crypto crypto = new Crypto(new CryptoName(string.Empty, string.Empty)); });
            Assert.Throws<ArgumentNullException>(() => { Crypto crypto = new Crypto(new CryptoName(cryptoName, string.Empty)); });
            Assert.Throws<ArgumentNullException>(() => { Crypto crypto = new Crypto(new CryptoName(string.Empty, cryptoSymbol)); });
        }

        [Test]
        public void Crypto_Prices_Empty()
        {
            var crypto = new Crypto(new CryptoName(cryptoName, cryptoSymbol));

            Assert.Throws<ArgumentNullException>(() => { Price price = new Price().Create(string.Empty, 0); });
            Assert.Throws<InvalidDataException>(() => { Price price = new Price().Create("USD", 0); });
        }

        [Test]
        public void Crypto_Prices_SetPrice_InvalidArgument()
        {
            var crypto = new Crypto(new CryptoName(cryptoName, cryptoSymbol));

            Assert.Throws<InvalidDataException>(() => crypto.SetPrice(new List<Price>()));
            Assert.Throws<InvalidDataException>(() => crypto.SetPrice(new Dictionary<string, decimal>()));
            Assert.Throws<ArgumentNullException>(() => crypto.SetPrice(new Dictionary<string, decimal>() { { string.Empty, 0 } }));
            Assert.Throws<InvalidDataException>(() => crypto.SetPrice(new Dictionary<string, decimal>() { { "USD", 0 } }));
        }

        [Test]
        public void Crypto_Prices_AddPrice_InvalidArgument()
        {
            var crypto = new Crypto(new CryptoName(cryptoName, cryptoSymbol));

            Assert.Throws<ArgumentNullException>(() => crypto.AddPrice(new Price()));
            Assert.Throws<ArgumentNullException>(() => crypto.AddPrice(new Price().Create(string.Empty, 0)));
            Assert.Throws<InvalidDataException>(() => crypto.AddPrice(new Price().Create("USD", 0)));
        }
    }
}
