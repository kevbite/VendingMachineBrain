namespace VendingMachineBrain
{
    public class Coin
    {
        public Coin(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
    }
}