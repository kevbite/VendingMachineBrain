namespace VendingMachineBrain.FunctionalTests
{
    public static class RawCoins
    {
        public static RawCoin TenPence { get; } = new RawCoin(24.5m, 6.5m);

        public static RawCoin TwentyPence { get; } = new RawCoin(21.4m, 5m);
    }
}
