using FluentAssertions;
using NUnit.Framework;

namespace VendingMachineBrain.FunctionalTests.Scenarios
{
    [TestFixture]
    public class TryingToBuyingAProductWithNotEnoughMoney : VendingMachineTests
    {
        private Product _expectedProduct;
        private decimal _expectedBalance;

        [OneTimeSetUp]
        public void WhenPressingKeyTwo()
        {
            GivenAVedingMachine();
            _expectedProduct = State[ProductSlot.Two];
            _expectedBalance = 0.10m;

            CoinSlot.Insert(RawCoins.TenPence);

            Keypad.Press(Key.Two);
        }

        [Test]
        public void ThenTheProductPriceIsDisplayedThenTheCurrentBalanceIsDisplayed()
        {
            Display.Read().Should().Be($"PRICE £{_expectedProduct.Price:#0.00}");
            Display.Read().Should().Be($"£{_expectedBalance:#0.00}");
            Display.Read().Should().Be($"£{_expectedBalance:#0.00}");
        }
    }
}
