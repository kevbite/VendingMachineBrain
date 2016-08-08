using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.Tests
{
    [TestFixture]
    public class VendingMachineTests
    {
        private TestKeypad _keypad;
        private Mock<IProductDispenser> _productDispenser;
        private TestCoinSlot _coinSlot;
        private Mock<ICoinIdentifier> _coinIdentifier;
        private Dictionary<ProductSlot, Queue<Product>> _state;
        private Product _expectedProduct;
        private Mock<IDisplay> _display;
        private VendingMachine _vendingMachine;

        public void GivenAVedingMachine()
        {
            _keypad = new TestKeypad();
            _productDispenser = new Mock<IProductDispenser>();
            _coinSlot = new TestCoinSlot();
            _coinIdentifier = new Mock<ICoinIdentifier>();
            _display = new Mock<IDisplay>();

            _state = new Dictionary<ProductSlot, Queue<Product>>()
            {
                {ProductSlot.One, new Queue<Product>(new []{ new Product() {Name = "Cola", Price = 1.00m}})},
                {ProductSlot.Two, new Queue<Product>(new []{ new Product() {Name = "Chips", Price = 0.50m}})},
                {ProductSlot.Three, new Queue<Product>(new []{ new Product() {Name = "Candy", Price = 0.65m}})}
            };

            _vendingMachine = new VendingMachine(_keypad, _productDispenser.Object, _coinSlot, _coinIdentifier.Object, _display.Object);
            _vendingMachine.SetState(_state);
        }

        [OneTimeSetUp]
        public void WhenInsertingEnoughMoneyAndPressingKeyTwo()
        {
            GivenAVedingMachine();

            _expectedProduct = _state[ProductSlot.Two].Peek();

            var rawCoin = new RawCoin();
            _coinIdentifier.Setup(x => x.Identifier(rawCoin))
                .Returns(new Coin(_expectedProduct.Price));

            _coinSlot.Insert(rawCoin);

            _keypad.Press(Key.Two);
        }

        [Test]
        public void ThenTheCorrectProductIsDispensed()
        {
            _productDispenser.Verify(x => x.Dispense(_expectedProduct), Times.Once);
        }

        [Test]
        public void ThenDisplayShowsThankYou()
        {
            _display.Verify(x => x.Write("THANK YOU"), Times.Once);
        }

        [Test]
        public void ThenDisplayShowsInsertCoin()
        {
            _display.Verify(x => x.Write("INSERT COIN"), Times.Once);
        }

        [Test]
        public void ThenTheBlanceShouldBeZero()
        {
            _vendingMachine.Balance.Should().Be(decimal.Zero);
        }
    }

 
}
