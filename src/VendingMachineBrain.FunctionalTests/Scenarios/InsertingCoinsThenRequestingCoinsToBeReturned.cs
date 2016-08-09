using Castle.Core.Internal;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.FunctionalTests.Scenarios
{
    [TestFixture]
    public class InsertingCoinsThenRequestingCoinsToBeReturned : VendingMachineTests
    {
        [OneTimeSetUp]
        public void WhenInsertingEnoughMoneyAndPressingKeyTwo()
        {
            GivenAVedingMachine();

            var coins = new[]
            {
                RawCoins.TenPence,
                RawCoins.TwentyPence,
                RawCoins.TwentyPence
            };

            coins.ForEach(x => CoinSlot.Insert(x));

            TestReturnButton.Press();
        }

        [Test]
        public void ThenTheSameCoinsAreReturned()
        {
            CoinDispenser.Verify(x => x.Dispense(It.IsAny<Coin>()), Times.Exactly(3));
            CoinDispenser.Verify(x => x.Dispense(It.Is<Coin>(coin => coin.Amount == 0.10m)), Times.Once);
            CoinDispenser.Verify(x => x.Dispense(It.Is<Coin>(coin => coin.Amount == 0.20m)), Times.Exactly(2));
        }
    }
}
