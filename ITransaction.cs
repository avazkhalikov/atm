namespace NBUbankATMSystem
{
    interface ITransaction
    {
        void InsertTransaction(BankAccount bankAccount, Transaction transaction);

        void ViewTransaction(BankAccount bankAccount);
    }
}