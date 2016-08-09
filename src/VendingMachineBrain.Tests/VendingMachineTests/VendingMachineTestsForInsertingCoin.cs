using FluentAssertions;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    [TestFixture]
    public class VendingMachineTestsForInsertingCoin : VendingMachineTests
    {
        [SetUp]
        public void WhenCoinInserted()
        {
            GivenAVedingMachine();

            var rawCoin1 = new RawCoin(0, 0);
            var rawCoin2 = new RawCoin(0, 0);

            CoinIdentifier.Setup(x => x.Identifier(rawCoin1))
                .Returns(Coin.OnePence);

            CoinIdentifier.Setup(x => x.Identifier(rawCoin2))
                .Returns(Coin.TwoPence);

            (VendingMachine as ICoinSlotObserver).CoinInserted(rawCoin1);
            (VendingMachine as ICoinSlotObserver).CoinInserted(rawCoin2);
        }

        [Test]
        public void ThenTheBlanceIsCorrect()
        {
            VendingMachine.Balance.Should().Be(0.03m);
        }
    }
}
