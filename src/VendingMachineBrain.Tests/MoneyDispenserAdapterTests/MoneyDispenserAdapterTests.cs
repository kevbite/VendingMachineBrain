using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.MoneyDispenserAdapterTests
{
    [TestFixture]
    public class MoneyDispenserAdapterTests
    {
        private MoneyDispenserAdapter _adapter;
        private Mock<ICoinDispenser> _coinDispenser;

        [SetUp]
        public void GivenAMoneyDispenserAdapter()
        {
            _coinDispenser = new Mock<ICoinDispenser>();
            _adapter = new MoneyDispenserAdapter(_coinDispenser.Object);
        }

        [Test]
        public void WhenDispensingTheePoundAndEightyEightPenceThenTheCorrectCoinsAreDispensed()
        {
            _adapter.Dispense(3.88m);

            var expectedCoins = new[]
           {
                Coin.OnePence,
                Coin.TwoPence,
                Coin.FivePence,
                Coin.TenPence,
                Coin.TwentyPence,
                Coin.FiftyPence,
                Coin.OnePound,
                Coin.TwoPound
            };

            _coinDispenser.Verify(x => x.Dispense(It.IsAny<Coin>()), Times.Exactly(expectedCoins.Length));

            foreach (var expectedCoin in expectedCoins)
            {
                _coinDispenser.Verify(x => x.Dispense(It.Is<Coin>(coin => coin.Amount == expectedCoin.Amount)), Times.Once);
            }
        }

        [Test]
        public void WhenDispensingFourPenceThenTheCorrectCoinsAreDispensed()
        {
            _adapter.Dispense(0.04m);

            _coinDispenser.Verify(x => x.Dispense(It.IsAny<Coin>()), Times.Exactly(2));

            _coinDispenser.Verify(x => x.Dispense(It.Is<Coin>(coin => coin.Amount == Coin.TwoPence.Amount)), Times.Exactly(2));
        }

        [Test]
        public void WhenDispensingAnActualCoinThenTheCoinIsPassedToTheCoinDispenser()
        {
            var onePence = Coin.OnePence;

            _adapter.Dispense(onePence);

            _coinDispenser.Verify(x => x.Dispense(onePence), Times.Once);
        }
    }
}
