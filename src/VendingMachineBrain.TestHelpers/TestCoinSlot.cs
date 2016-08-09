using System.Collections.Generic;

namespace VendingMachineBrain.TestHelpers
{
    public class TestCoinSlot : ICoinSlot
    {
        private ICoinSlotObserver _coinSlotObserver;

        public void Connect(ICoinSlotObserver coinSlotObserver)
        {
            _coinSlotObserver = coinSlotObserver;
        }

        public void Insert(RawCoin rawCoin)
        {
            _coinSlotObserver.CoinInserted(rawCoin);
        }
    }
}