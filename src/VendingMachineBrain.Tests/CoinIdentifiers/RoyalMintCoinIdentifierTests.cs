using FluentAssertions;
using NUnit.Framework;
using VendingMachineBrain.CoinIdentifiers;

namespace VendingMachineBrain.Tests.CoinIdentifiers
{
    [TestFixture]
    public class RoyalMintCoinIdentifierTests
    {
        private RoyalMintCoinIdentifier _royalMintCoinIdentifier;
        
        [SetUp]
        public void GivenARoyalMintCoinIdentifier()
        {
            _royalMintCoinIdentifier = new RoyalMintCoinIdentifier();
        }
        
        [TestCase("20.3", "3.56", "0.01", TestName = "One Pence Coin")]
        [TestCase("25.9", "7.12", "0.02", TestName = "Two Pence Coin")]
        [TestCase("18.0", "3.25", "0.05", TestName = "Five Pence Coin")]
        [TestCase("24.5", "6.5", "0.10", TestName = "Ten Pence Coin")]
        [TestCase("21.4", "5", "0.20", TestName = "Twenty Pence Coin")]
        [TestCase("27.3", "8", "0.50", TestName = "Fifty  Pence Coin")]
        [TestCase("22.5", "9.5", "1.00", TestName = "One Pound Coin")]
        [TestCase("28.4", "12.0", "2.00", TestName = "Two Pound Coin")]
        [TestCase("38.61", "28.28", "5.00", TestName = "Five Pound Coin")]
        [TestCase("2.8", "8.75", "1.00", TestName = "2017 One Pound Coin")]
        public void WhenIdentifyingARawCoinThenTheCorrectValueWillBeReturned(decimal diameter, decimal weight, decimal expectedAmount)
        {
            var coin = _royalMintCoinIdentifier.Identifier(new RawCoin(diameter, weight));

            coin.Amount.Should().Be(expectedAmount);
        }
        
    }
}
