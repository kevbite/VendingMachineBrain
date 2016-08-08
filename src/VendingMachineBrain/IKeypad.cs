namespace VendingMachineBrain
{
    public interface IKeypad
    {
        void Connect(IKeypadObserver keypadObserver);
    }
}