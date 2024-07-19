namespace Bookify.Domain.Apartments
{
    public record Currency
    {
        internal static readonly Currency None = new("");
        public static readonly Currency Usd = new Currency("USD");
        public static readonly Currency Eur = new Currency("EUR");
        public static readonly Currency Cny = new Currency("CNY");

        public string Code { get; init; }

        private Currency(string code)
        {
            Code = code;
        }

        public static Currency FromCode(string code)
        {
            return All.FirstOrDefault(x => x.Code == code)
                ?? throw new ApplicationException("The currency code is invalid");
        }

        public static readonly IReadOnlyCollection<Currency> All = new List<Currency>
        { 
            Usd,
            Eur,
            Cny,
        };
    }
}
