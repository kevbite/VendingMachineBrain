using FluentAssertions;
using NUnit.Framework;

namespace VendingMachineBrain.FunctionalTests.Scenarios
{
    [TestFixture]
    public class TryingToBuyingAProductWithNoMoney : VendingMachineTests
    {
        [OneTimeSetUp]
        public void WhenPressingKeyTwo()
        {
            GivenAVedingMachine();

            Keypad.Press(Key.Two);
        }

        [Test]
        public void ThenTheProductPriceIsDisplayedThenInsertCoinIsDisplayed()
        {
            var expectedProduct = State[ProductSlot.Two];

            Display.Read().Should().Be($"PRICE £{expectedProduct.Price:#0.00}");
            Display.Read().Should().Be("INSERT COIN");
            Display.Read().Should().Be("INSERT COIN");
            Display.Read().Should().Be("INSERT COIN");
        }
    }
}
