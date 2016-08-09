using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace VendingMachineBrain.Tests.VendingMachineTests
{
    [TestFixture]
    public class VendingMachineTestsForReturnButton : VendingMachineTests
    {
        private Coin _coin1;
        private Coin _coin2;

        [SetUp]
        public void WhenCoinInsertedAndReturningCoins()
        {
            GivenAVedingMachine();

            var rawCoin1 = new RawCoin(0, 0);
            var rawCoin2 = new RawCoin(0, 0);

            _coin1 = Coin.OnePence;
            CoinIdentifier.Setup(x => x.Identifier(rawCoin1))
                .Returns(_coin1);

            _coin2 = Coin.TwoPence;
            CoinIdentifier.Setup(x => x.Identifier(rawCoin2))
                .Returns(_coin2);

            (VendingMachine as ICoinSlotObserver).CoinInserted(rawCoin1);
            (VendingMachine as ICoinSlotObserver).CoinInserted(rawCoin2);

            (VendingMachine as IReturnButtonObserver).ButtonPressed();
        }

        [Test]
        public void ThenTheSameCoinsAreDispensed()
        {
            MoneyDispenser.Verify(x => x.Dispense(_coin1), Times.Once);
            MoneyDispenser.Verify(x => x.Dispense(_coin2), Times.Once);
        }

        [Test]
        public void ThenTheBalanceIsZero()
        {
            VendingMachine.Balance.Should().Be(decimal.Zero);
        }
    }
}
