namespace VendingMachineBrain
{
    public interface ICoinSlotObserver
    {
        void CoinInserted(RawCoin rawCoin);
    }
}