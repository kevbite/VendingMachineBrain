using System.Collections.Generic;
using System.Linq;
using VendingMachineBrain.CoinIdentifiers;

namespace VendingMachineBrain
{
    public class VendingMachine : IKeypadObserver, ICoinSlotObserver
    {
        private IDictionary<ProductSlot, Product> _state;
        private readonly IProductDispenser _productDispenser;
        private readonly ICoinIdentifier _coinIdentifier;
        private readonly IDisplay _display;

        public decimal Balance { get; private set; }

        public VendingMachine(IKeypad keypad, IProductDispenser productDispenser, ICoinSlot coinSlot, IDisplay display)
            : this(keypad, productDispenser, coinSlot, new RoyalMintCoinIdentifier(), display)
        {
            
        }

        public VendingMachine(IKeypad keypad, IProductDispenser productDispenser, ICoinSlot coinSlot, ICoinIdentifier coinIdentifier, IDisplay display)
        {
            keypad.Connect(this);
            coinSlot.Connect(this);
            
            _productDispenser = productDispenser;
            _coinIdentifier = coinIdentifier;
            _display = display;
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
                Balance -= product.Price;
                _display.Write("THANK YOU");
                _display.Write("INSERT COIN");
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

            Balance += coin.Amount;
        }

        private readonly IReadOnlyDictionary<Key, ProductSlot> _keyProductSlotMap = new Dictionary<Key, ProductSlot>()
        {
            {Key.One, ProductSlot.One},
            {Key.Two, ProductSlot.Two},
            {Key.Three, ProductSlot.Three}
        };
    }
}
