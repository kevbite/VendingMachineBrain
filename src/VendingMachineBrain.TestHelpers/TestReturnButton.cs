namespace VendingMachineBrain.TestHelpers
{
    public class TestReturnButton : IReturnButton
    {
        private IReturnButtonObserver _returnButtonObserver;

        public void Connect(IReturnButtonObserver returnButtonObserver)
        {
            _returnButtonObserver = returnButtonObserver;
        }

        public void Press() => _returnButtonObserver.ButtonPressed();
    }
}