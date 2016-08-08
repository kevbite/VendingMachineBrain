namespace VendingMachineBrain.Tests
{
    public class TestKeypad : IKeypad
    {
        private IKeypadObserver _keypadObserver;

        public void Connect(IKeypadObserver keypadObserver)
        {
            _keypadObserver = keypadObserver;
        }

        public void Press(Key key)
        {
            _keypadObserver.KeyPressed(key);
        }
    }
}