namespace Cryptocurrency.Core.ValueObject
{
    public class Price : ValueObject<Price>
    {
        public string Currency { get; protected set; }
        public decimal Value { get; protected set; }

        public Price Create(string currency, decimal Value)
        {
            //check parameters
            //check valid currency

            this.Currency = currency;
            this.Value = Value;

            return this;
        }

        protected override bool EqualsCore(Price other)
        {
            return other.Currency == Currency && other.Value == Value;
        }

        protected override int GetHashCodeCore()
        {
            return Currency.GetHashCode() ^ Value.GetHashCode();
        }
    }
}
