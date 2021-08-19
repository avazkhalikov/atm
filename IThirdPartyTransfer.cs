namespace NBUbankATMSystem
{
    public interface IThirdPartyTransfer
    {
        void PerformThirdPartyTransferConsole(BankAccount bankAccount, VMThirdPartyTransfer vmThirdPartyTransfer);
    }
}