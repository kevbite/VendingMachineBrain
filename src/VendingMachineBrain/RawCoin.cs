namespace VendingMachineBrain
{
    public class RawCoin
    {
        public decimal Diameter { get; }
        public decimal Weight { get; }

        public RawCoin(decimal diameter, decimal weight)
        {
            Diameter = diameter;
            Weight = weight;
        }
    }
}