using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VendingMachineBrain.Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IKeypad, IProductDispenser, ICoinSlot, IDisplay, ICoinDispenser, IReturnButton
    {
        private readonly VendingMachine _vendingMachine;
        private IKeypadObserver _keypadObserver;
        private ICoinSlotObserver _coinSlotObserver;
        private IReturnButtonObserver _returnButtonObserver;

        private readonly ConcurrentQueue<string> _displayText = new ConcurrentQueue<string>();
        private readonly DispatcherTimer _dispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();

            _vendingMachine = new VendingMachine(this, this, this, this, this, this); // These would be replaced with real device wrappers in real IoT.
            _vendingMachine.SetState(new Dictionary<ProductSlot, Product>()
            {
                {ProductSlot.One, new Product("Cola", 1.00m)},
                {ProductSlot.Two, new Product("Chips", 0.50m)},
                {ProductSlot.Three, new Product("Candy", 0.65m)}
            });

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            _dispatcherTimer.Start();
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            string text = "";
            _displayText.TryPeek(out text);

            Display.Text = text;

            if (_displayText.Count > 1)
            {
                _displayText.TryDequeue(out text);
            }

        }

        public void Connect(IKeypadObserver keypadObserver)
        {
            _keypadObserver = keypadObserver;
        }

        public void Dispense(ProductSlot productSlot)
        {
            ProductDispenser.Text = productSlot.ToString();
        }

        public void Connect(ICoinSlotObserver coinSlotObserver)
        {
            _coinSlotObserver = coinSlotObserver;
        }

        public async void Write(string text)
        {
            _displayText.Enqueue(text);
        }

        public void Dispense(Coin coins)
        {
            CoinDispenser.Text += coins.Amount + ", ";
        } 

        public void Connect(IReturnButtonObserver returnButtonObserver)
        {
            _returnButtonObserver = returnButtonObserver;
        }

        private void Key1_Click(object sender, RoutedEventArgs e)
        {
            _keypadObserver.KeyPressed(Key.One);
        }

        private void Key2_Click(object sender, RoutedEventArgs e)
        {
            _keypadObserver.KeyPressed(Key.Two);
        }

        private void Key3_Click(object sender, RoutedEventArgs e)
        {
            _keypadObserver.KeyPressed(Key.Three);
        }

        private void InsertOnePence_Click(object sender, RoutedEventArgs e)
        {
            _coinSlotObserver.CoinInserted(new RawCoin(20.3m, 3.56m));
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            _returnButtonObserver.ButtonPressed();
        }

        private void InsertPound_Click(object sender, RoutedEventArgs e)
        {
            _coinSlotObserver.CoinInserted(new RawCoin(22.5m, 9.5m));
        }
    }
}
