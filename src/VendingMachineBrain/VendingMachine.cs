using System.Collections.Generic;
using System.Linq;
using VendingMachineBrain.CoinIdentifiers;

namespace VendingMachineBrain
{
    public class VendingMachine : IKeypadObserver, ICoinSlotObserver, IReturnButtonObserver
    {
        private IDictionary<ProductSlot, Product> _state;
        private readonly IProductDispenser _productDispenser;
        private readonly ICoinIdentifier _coinIdentifier;
        private readonly IDisplay _display;
        private readonly IMoneyDispenser _moneyDispenser;

        private readonly List<Coin> _insertedCoins = new List<Coin>();

        public decimal Balance
        {
            get { return _insertedCoins.Sum(x => x.Amount); }
        }

        public VendingMachine(IKeypad keypad, IProductDispenser productDispenser, ICoinSlot coinSlot, IDisplay display, ICoinDispenser coinDispenser, IReturnButton returnButton)
            : this(keypad, productDispenser, coinSlot, new RoyalMintCoinIdentifier(), display, new MoneyDispenserAdapter(coinDispenser), returnButton)
        {
            
        }

        public VendingMachine(IKeypad keypad, IProductDispenser productDispenser, ICoinSlot coinSlot, ICoinIdentifier coinIdentifier, IDisplay display, IMoneyDispenser moneyDispenser, IReturnButton returnButton)
        {
            keypad.Connect(this);
            coinSlot.Connect(this);
            returnButton.Connect(this);

            _productDispenser = productDispenser;
            _coinIdentifier = coinIdentifier;
            _display = display;
            _moneyDispenser = moneyDispenser;
        }

        public void SetState(IDictionary<ProductSlot, Product> state)
        {
            _state = state;
        }

        void IKeypadObserver.KeyPressed(Key key)
        {
            var productSlot = _keyProductSlotMap[key];

            var product = _state[productSlot];

            if (product.Price <= Balance)
            {
                _productDispenser.Dispense(productSlot);
                _display.Write("THANK YOU");
                _display.Write("INSERT COIN");

                _moneyDispenser.Dispense(Balance - product.Price);
                _insertedCoins.Clear();
            }
            else
            {
                _display.Write($"PRICE {product.Price:C}");
                if (Balance == decimal.Zero)
                {
                    _display.Write("INSERT COIN");
                }
                else
                {
                    _display.Write($"{Balance:C}");
                }
            }
        }
        

        void ICoinSlotObserver.CoinInserted(RawCoin rawCoin)
        {
            var coin = _coinIdentifier.Identifier(rawCoin);

            _insertedCoins.Add(coin);
        }

        private readonly IReadOnlyDictionary<Key, ProductSlot> _keyProductSlotMap = new Dictionary<Key, ProductSlot>()
        {
            {Key.One, ProductSlot.One},
            {Key.Two, ProductSlot.Two},
            {Key.Three, ProductSlot.Three}
        };

        void IReturnButtonObserver.ButtonPressed()
        {
            _insertedCoins.ForEach(x => _moneyDispenser.Dispense(x));

            _insertedCoins.Clear();
        }
    }

    public interface IReturnButton
    {
        void Connect(IReturnButtonObserver returnButtonObserver);
    }

    public interface IReturnButtonObserver
    {
        void ButtonPressed();
    }
}
