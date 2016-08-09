using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    [TestFixture]
    public class VendingMachineTestsForNotEnoughMoneyToBuyAProduct : VendingMachineTests
    {
        private Product _expectedProduct;
        private decimal _expectedBalance;

        [OneTimeSetUp]
        public void WhenPressingKeyTwo()
        {
            GivenAVedingMachine();
            _expectedProduct = State[ProductSlot.Two];
            var tenPence = Coin.TenPence;
            _expectedBalance = tenPence.Amount;
            var rawCoin = new RawCoin(0,0);
            CoinIdentifier.Setup(x => x.Identifier(rawCoin))
                .Returns(tenPence);

            CoinSlot.Insert(rawCoin);

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
