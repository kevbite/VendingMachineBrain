using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    public abstract class VendingMachineTests
    {
        protected TestKeypad Keypad;
        protected TestCoinSlot CoinSlot;
        protected Mock<IProductDispenser> ProductDispenser;
        protected Mock<ICoinIdentifier> CoinIdentifier;
        protected TestDisplay Display;
        protected Dictionary<ProductSlot, Product> State;
        protected VendingMachine VendingMachine;

        public void GivenAVedingMachine()
        {
            Keypad = new TestKeypad();
            ProductDispenser = new Mock<IProductDispenser>();
            CoinSlot = new TestCoinSlot();
            CoinIdentifier = new Mock<ICoinIdentifier>();
            Display = new TestDisplay();

            State = new Dictionary<ProductSlot, Product>()
            {
                {ProductSlot.One, new Product() {Name = "Cola", Price = 1.00m}},
                {ProductSlot.Two, new Product() {Name = "Chips", Price = 0.50m}},
                {ProductSlot.Three, new Product() {Name = "Candy", Price = 0.65m}}
            };

            VendingMachine = new VendingMachine(Keypad, ProductDispenser.Object, CoinSlot, CoinIdentifier.Object, Display);
            VendingMachine.SetState(State);
        }
    }
}
