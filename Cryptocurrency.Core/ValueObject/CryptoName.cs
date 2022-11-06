namespace Cryptocurrency.Core.ValueObject
{
    public class CryptoName : ValueObject<CryptoName>
    {
        public string Name { get; protected set; }
        public string Symbol { get; protected set; }

        public CryptoName(string name, string symbol)
        {
            this.Name = name;
            this.Symbol = symbol;
        }

        protected override bool EqualsCore(CryptoName other)
        {
            return other.Name == Name && other.Symbol == Symbol;
        }

        protected override int GetHashCodeCore()
        {
            return Name.GetHashCode() ^ Symbol.GetHashCode();
        }
    }
}
