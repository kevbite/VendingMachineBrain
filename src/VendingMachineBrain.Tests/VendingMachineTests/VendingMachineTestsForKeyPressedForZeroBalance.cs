using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    [TestFixture]
    public class VendingMachineTestsForKeyPressedForZeroBalance : VendingMachineTests
    {
        [SetUp]
        public void WhenPressingKey()
        {
            GivenAVedingMachine();

            var state = new Mock<IDictionary<ProductSlot, Product>>();
            state.Setup(x => x[ProductSlot.Two])
                .Returns(new Product("Test Product", 10.0m));

            VendingMachine.SetState(state.Object);

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
            Display.Verify(x => x.Write("PRICE £10.00"));
        }

        [Test]
        public void ThenInsertCoinIsWrittenToTheDisplay()
        {
            Display.Verify(x => x.Write("INSERT COIN"));
        }
    }
}