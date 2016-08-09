using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    [TestFixture]
    public class VendingMachineTestsForKeyPressed : VendingMachineTests
    { 
        [SetUp]
        public void WhenPressingKey()
        {
            GivenAVedingMachine();

            var state = new Mock<IDictionary<ProductSlot, Product>>();
            state.Setup(x => x[ProductSlot.Two])
                .Returns(new Product("Test Product", -10.0m));
            
            VendingMachine.SetState(state.Object);

            (VendingMachine as IKeypadObserver).KeyPressed(Key.Two);
        }

        [Test]
        public void ThenTheCorrectProductIsDispensed()
        {
            ProductDispenser.Verify(x => x.Dispense(ProductSlot.Two));
        }

        [Test]
        public void ThenThankYouIsWrittenToTheDisplay()
        {
            Display.Verify(x => x.Write("THANK YOU"), Times.Once);
        }

        [Test]
        public void ThenInsertCoinIsWrittenToTheDisplay()
        {
            Display.Verify(x => x.Write("INSERT COIN"));
        }

        [Test]
        public void ThenTheProductAmountIsDeducedFromBalance()
        {
            VendingMachine.Balance.Should().Be(10.0m);
        }
    }
}
