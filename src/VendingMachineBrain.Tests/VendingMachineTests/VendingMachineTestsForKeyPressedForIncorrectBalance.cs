using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    [TestFixture]
    public class VendingMachineTestsForKeyPressedForIncorrectBalance : VendingMachineTests
    {
        [SetUp]
        public void WhenPressingKey()
        {
            GivenAVedingMachine();

            var state = new Mock<IDictionary<ProductSlot, Product>>();
            state.Setup(x => x[ProductSlot.Two])
                .Returns(new Product("Test Product", 0.51m));

            VendingMachine.SetState(state.Object);

            var rawCoin = new RawCoin(0,0);
            CoinIdentifier.Setup(x => x.Identifier(rawCoin))
                .Returns(Coin.FiftyPence);

            (VendingMachine as ICoinSlotObserver).CoinInserted(rawCoin);
            (VendingMachine as IKeypadObserver).KeyPressed(Key.Two);
        }

        [Test]
        public void ThenNothingIsDispensed()
        {
            ProductDispenser.Verify(x => x.Dispense(It.IsAny<ProductSlot>()), Times.Never);
        }

        [Test]
        public void ThenTheProductPriceIsDisplayed()
        {
            Display.Verify(x => x.Write("PRICE £0.51"));
        }

        [Test]
        public void ThenTheBalanceIsWrittenToTheDisplay()
        {
            Display.Verify(x => x.Write("£0.50"));
        }
    }
}