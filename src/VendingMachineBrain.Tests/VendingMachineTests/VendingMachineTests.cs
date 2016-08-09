﻿using Moq;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    public abstract class VendingMachineTests
    {
        protected Mock<IProductDispenser> ProductDispenser;
        protected Mock<ICoinIdentifier> CoinIdentifier;
        protected Mock<IDisplay> Display;
        public Mock<IMoneyDispenser> MoneyDispenser { get; set; }

        protected VendingMachine VendingMachine;
        
        public void GivenAVedingMachine()
        {
            ProductDispenser = new Mock<IProductDispenser>();
            CoinIdentifier = new Mock<ICoinIdentifier>();
            Display = new Mock<IDisplay>();
            MoneyDispenser = new Mock<IMoneyDispenser>();

            VendingMachine = new VendingMachine(Mock.Of<IKeypad>(), ProductDispenser.Object, Mock.Of<ICoinSlot>(), CoinIdentifier.Object, Display.Object, MoneyDispenser.Object, Mock.Of<IReturnButton>());
        }

    }
}
