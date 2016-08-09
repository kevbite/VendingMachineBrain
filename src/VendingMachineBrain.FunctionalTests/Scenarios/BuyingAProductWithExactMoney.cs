using Castle.Core.Internal;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.FunctionalTests.Scenarios
{
    [TestFixture]
    public class BuyingAProductWithExactMoney : VendingMachineTests
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
    }
}
