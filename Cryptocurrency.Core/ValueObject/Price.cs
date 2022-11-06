namespace Cryptocurrency.Core.ValueObject
{
    public class Price : ValueObject<Price>
    {
        public string Currency { get; protected set; }
        public decimal Value { get; protected set; }

        public Price Create(string currency, decimal Value)
        {
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentNullException(nameof(currency));

            if (Value <= 0)
                throw new ArgumentNullException(nameof(Value));

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
