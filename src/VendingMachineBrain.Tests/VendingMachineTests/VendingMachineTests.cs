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
        protected Dictionary<ProductSlot, Queue<Product>> State;
        protected VendingMachine VendingMachine;

        public void GivenAVedingMachine()
        {
            Keypad = new TestKeypad();
            ProductDispenser = new Mock<IProductDispenser>();
            CoinSlot = new TestCoinSlot();
            CoinIdentifier = new Mock<ICoinIdentifier>();
            Display = new TestDisplay();

            State = new Dictionary<ProductSlot, Queue<Product>>()
            {
                {ProductSlot.One, new Queue<Product>(new []{ new Product() {Name = "Cola", Price = 1.00m}})},
                {ProductSlot.Two, new Queue<Product>(new []{ new Product() {Name = "Chips", Price = 0.50m}})},
                {ProductSlot.Three, new Queue<Product>(new []{ new Product() {Name = "Candy", Price = 0.65m}})}
            };

            VendingMachine = new VendingMachine(Keypad, ProductDispenser.Object, CoinSlot, CoinIdentifier.Object, Display);
            VendingMachine.SetState(State);
        }
    }
}
