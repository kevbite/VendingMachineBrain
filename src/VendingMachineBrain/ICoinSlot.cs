namespace VendingMachineBrain
{
    public interface ICoinSlot
    {
        void Connect(ICoinSlotObserver vendingMachine);
    }
}