using Castle.DynamicProxy;

namespace VendingMachineBrain.FunctionalTests
{
    public static class RawCoins
    {
        public static RawCoin OnePence { get; } = new RawCoin(20.3m, 3.56m);

        public static RawCoin TwoPence { get; } = new RawCoin(25.9m, 7.12m);

        public static RawCoin FivePence { get; } = new RawCoin(18.0m, 3.25m);

        public static RawCoin TenPence { get; } = new RawCoin(24.5m, 6.5m);

        public static RawCoin TwentyPence { get; } = new RawCoin(21.4m, 5m);

        public static RawCoin FiftyPence { get; } = new RawCoin(27.3m, 8m);

        public static RawCoin OnePound { get; } = new RawCoin(22.5m, 9.5m);

        public static RawCoin TwoPound { get; } = new RawCoin(28.4m, 12.0m);

    }
}
