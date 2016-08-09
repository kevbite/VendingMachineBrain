using System.Collections.Generic;
using System.Linq;

namespace VendingMachineBrain
{
    public class MoneyDispenserAdapter : IMoneyDispenser
    {
        private readonly ICoinDispenser _coinDispenser;

        public MoneyDispenserAdapter(ICoinDispenser coinDispenser)
        {
            _coinDispenser = coinDispenser;
        }

        public void Dispense(decimal amount)
        {
            foreach (var availableCoin in _availableCoins.OrderByDescending(x => x.Amount))
            {
                var coinsRequired = (int)(amount/availableCoin.Amount);

                foreach (var coin in Enumerable.Repeat(availableCoin, coinsRequired))
                {
                    _coinDispenser.Dispense(coin);
                }

                amount = amount%availableCoin.Amount;
            }
        }

        private readonly IEnumerable<Coin> _availableCoins = new[]
        {
            Coin.OnePence,
            Coin.TwoPence,
            Coin.FivePence,
            Coin.TenPence,
            Coin.TwentyPence,
            Coin.FiftyPence,
            Coin.OnePound,
            Coin.TwoPound,
        };

        public void Dispense(Coin coins)
        {
            _coinDispenser.Dispense(coins);
        }
    }
}