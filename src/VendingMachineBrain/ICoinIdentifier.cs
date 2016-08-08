namespace VendingMachineBrain
{
    public interface ICoinIdentifier
    {
        Coin Identifier(RawCoin rawCoin);
    }
}