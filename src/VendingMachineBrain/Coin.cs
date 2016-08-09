namespace VendingMachineBrain
{
    public class Coin
    {
        private Coin(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }

        public static Coin OnePence = new Coin(0.01m);
        public static Coin TwoPence = new Coin(0.02m);
        public static Coin FivePence = new Coin(0.05m);
        public static Coin TenPence = new Coin(0.10m);
        public static Coin TwentyPence = new Coin(0.20m);
        public static Coin FiftyPence = new Coin(0.50m);
        public static Coin OnePound = new Coin(1.00m);
        public static Coin TwoPound = new Coin(2.00m);
        public static Coin FivePound = new Coin(5.00m);
    }
}