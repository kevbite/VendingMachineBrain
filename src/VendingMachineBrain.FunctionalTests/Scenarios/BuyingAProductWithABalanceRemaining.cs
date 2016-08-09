using System.Linq;
using Castle.Core.Internal;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.FunctionalTests.Scenarios
{
    [TestFixture]
    public class BuyingAProductWithABalanceRemaining : VendingMachineTests
    {
        [OneTimeSetUp]
        public void WhenInsertingMoneyAndPressingKeyTwo()
        {
            GivenAVedingMachine();

            var coins = new[]
            {
                RawCoins.FiftyPence,
                RawCoins.OnePence,
                RawCoins.TwoPence,
                RawCoins.FivePence,
                RawCoins.TenPence,
                RawCoins.TwentyPence,
                RawCoins.FiftyPence,
                RawCoins.OnePound,
                RawCoins.TwoPound
            };

            coins.ForEach(x => CoinSlot.Insert(x));

            Keypad.Press(Key.Two);
        }

        [Test]
        public void ThenTheCorrectProductIsDispensed()
        {
            ProductDispenser.Verify(x => x.Dispense(ProductSlot.Two), Times.Once);
        }

        [Test]
        public void ThenDisplayShowsThankYouThenShowsInsertCoin()
        {
            Display.Read().Should().Be("THANK YOU");
            Display.Read().Should().Be("INSERT COIN");
            Display.Read().Should().Be("INSERT COIN");
            Display.Read().Should().Be("INSERT COIN");
        }

        [Test]
        public void ThenTheBlanceShouldBeZero()
        {
            VendingMachine.Balance.Should().Be(decimal.Zero);
        }

        [Test]
        public void ThenTheCorrectCoinsAreDispensed()
        {
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

            CoinDispenser.Verify(x => x.Dispense(It.IsAny<Coin>()), Times.Exactly(expectedCoins.Length));

            foreach (var expectedCoin in expectedCoins)
            {
                CoinDispenser.Verify(x => x.Dispense(It.Is<Coin>(coin => coin.Amount == expectedCoin.Amount)), Times.Once);
            }
        }
    }
}
