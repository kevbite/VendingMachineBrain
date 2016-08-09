using System;
using System.Collections.Generic;

namespace VendingMachineBrain.CoinIdentifiers
{
    public class RoyalMintCoinIdentifier : ICoinIdentifier
    {
        public Coin Identifier(RawCoin rawCoin)
        {
            Coin coin;
            _coinSpecificationMap.TryGetValue(Tuple.Create(rawCoin.Diameter, rawCoin.Weight), out coin);

            return coin;
        }

        private readonly Dictionary<Tuple<decimal, decimal>, Coin> _coinSpecificationMap = new Dictionary
            <Tuple<decimal, decimal>, Coin>()
        {
            {Tuple.Create(20.3m, 3.56m), Coin.OnePence},
            {Tuple.Create(25.9m, 7.12m), Coin.TwoPence},
            {Tuple.Create(18.0m, 3.25m), Coin.FivePence},
            {Tuple.Create(24.5m, 6.5m), Coin.TenPence},
            {Tuple.Create(21.4m, 5m), Coin.TwentyPence},
            {Tuple.Create(27.3m, 8m), Coin.FiftyPence},
            {Tuple.Create(22.5m, 9.5m), Coin.OnePound},
            {Tuple.Create(28.4m, 12.0m), Coin.TwoPound},
            {Tuple.Create(38.61m, 28.28m), Coin.FivePound},
            {Tuple.Create(2.8m, 8.75m), Coin.OnePound}
        };
    }
}
