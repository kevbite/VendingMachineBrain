using System.Collections.Generic;
using Moq;
using VendingMachineBrain.TestHelpers;

namespace VendingMachineBrain.FunctionalTests.Scenarios
{
    public abstract class VendingMachineTests
    {
        protected TestKeypad Keypad;
        protected TestCoinSlot CoinSlot;
        protected Mock<IProductDispenser> ProductDispenser;
        protected TestDisplay Display;
        protected Dictionary<ProductSlot, Product> State;
        protected VendingMachine VendingMachine;

        public void GivenAVedingMachine()
        {
            Keypad = new TestKeypad();
            ProductDispenser = new Mock<IProductDispenser>();
            CoinSlot = new TestCoinSlot();
            Display = new TestDisplay();

            State = new Dictionary<ProductSlot, Product>()
            {
                {ProductSlot.One, new Product("Cola", 1.00m)},
                {ProductSlot.Two, new Product("Chips", 0.50m)},
                {ProductSlot.Three, new Product("Candy", 0.65m)}
            };

            VendingMachine = new VendingMachine(Keypad, ProductDispenser.Object, CoinSlot, Display);
            VendingMachine.SetState(State);
        }
    }
}