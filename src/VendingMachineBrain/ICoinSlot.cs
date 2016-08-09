namespace VendingMachineBrain
{
    public interface ICoinSlot
    {
        void Connect(ICoinSlotObserver coinSlotObserver);
    }
}