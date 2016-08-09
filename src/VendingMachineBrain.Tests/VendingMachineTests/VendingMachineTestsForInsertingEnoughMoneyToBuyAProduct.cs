using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    [TestFixture]
    public class VendingMachineTestsForInsertingEnoughMoneyToBuyAProduct : VendingMachineTests
    {
        private Product _expectedProduct;

        [OneTimeSetUp]
        public void WhenInsertingEnoughMoneyAndPressingKeyTwo()
        {
            GivenAVedingMachine();

            _expectedProduct = State[ProductSlot.Two].Peek();

            var rawCoin = new RawCoin();
            CoinIdentifier.Setup(x => x.Identifier(rawCoin))
                .Returns(new Coin(_expectedProduct.Price));

            CoinSlot.Insert(rawCoin);

            Keypad.Press(Key.Two);
        }

        [Test]
        public void ThenTheCorrectProductIsDispensed()
        {
            ProductDispenser.Verify(x => x.Dispense(_expectedProduct), Times.Once);
        }

        [Test]
        public void ThenDisplayShowsThankYouThenShowsInsertCoin()
        {
            Display.Read().Should().Be("THANK YOU");
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
