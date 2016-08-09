namespace VendingMachineBrain
{
    public interface IMoneyDispenser : ICoinDispenser
    {
        void Dispense(decimal amount);
    }
}