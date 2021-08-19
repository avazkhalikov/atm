using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBUbankATMSystem;
using System;
using System.Collections.Generic;

namespace ATMTest
{
    [TestClass]
    public class UnitTest1
    {
        private static decimal transaction_amt;
        private static List<BankAccount> _accountList;
        private static List<Transaction> _listOfTransactions;
        private static BankAccount selectedAccount;
        private static BankAccount inputAccount;
        private static NBUbankATM atm; 
       
        /*   Reference Info of accounts to test. 
           { FullName = "Azim", AccountNumber=333111, CardNumber = 123, PinCode = 111111, Balance = 2000.00m, isLocked = false },
           { FullName = "Abdumalik", AccountNumber=111222, CardNumber = 456, PinCode = 111111, Balance = 1500.30m, isLocked = true },
           { FullName = "Toshmat", AccountNumber=888555, CardNumber = 789, PinCode = 111111, Balance = 2900.12m, isLocked = false }
          
         */      
     
        [TestInitialize]
        public void TestInitialize()
        {
            transaction_amt = 0;
            
            atm = new NBUbankATM();
            atm.InitializeListofTransactions();
            atm.Initialization();   //initialize all accounts
        }

        //Test Case 0: 
        [TestMethod]
        public void GetAccount_WithValidAccount_ReturnsAccount()
        {
            // Act
            selectedAccount = atm.getAllAccounts()[0];

            // Assert
            Assert.IsNotNull(selectedAccount);
          
        }


        //the first sample test will be here, CheckBalanceOne!
        //Test Case 1: 
        [TestMethod]
        public void TestCheckBalance()
        {
           selectedAccount = atm.getAllAccounts()[0]; //get the first account and make it selected.
           decimal myBalance= atm.GetBalance(selectedAccount);
            Assert.AreEqual(2000, myBalance); 

        }


        //Test Case 2: 
        [TestMethod]
        public void TestCheckWithdrawalCorrectlyUpdatedBalanceWithValidInput()
        {
            selectedAccount = atm.getAllAccounts()[0]; //gets the first account.
            atm.MakeActualWithdrawal(selectedAccount, 10);
            atm.MakeActualWithdrawal(selectedAccount, 20);
            decimal myBalance = atm.GetBalance(selectedAccount);

            Assert.AreEqual(1970, myBalance);
        }

        /* Cash Withdrawal’s purpose is to get a cash from ATM.  Suppose only amounts can be withdrawn is a multiply of 10. Ex: 10, 20, 30, 100, 200 UZS 
           Amount needs to be more than zero 
           Only positive amounts accepted
           Enough funds must exist
           Minimum Kept amount should be more that amount left. In our case it is 20 UZS. 
         */

        //Test Case 3:  Place Deposit, Positive Test, check if the amount deposited results with correct balance
        [TestMethod]
        public void TestBalanceAfterCashDepositAmount()
        {
            selectedAccount = atm.getAllAccounts()[0]; //gets the first account.
            
            decimal initialBalance = atm.GetBalance(selectedAccount);
            
            Assert.IsTrue(atm.PlaceDeposit(selectedAccount, 10));
            decimal myBalance = atm.GetBalance(selectedAccount);
            
            //check if the balance is updated.
            Assert.AreEqual(initialBalance+10, myBalance);
        }

        //Test Case 4: 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),  "A userId of null was inappropriately allowed.")]
        public void TestCheckWithdrawalAmountAcceptedPositive()
        {
            selectedAccount = atm.getAllAccounts()[0];
            atm.MakeActualWithdrawal(selectedAccount, -10);

            //error must be thrown here.
            // Assert.ThrowsException()
            //act
            Assert.ThrowsException<ArgumentNullException>(() => atm.MakeActualWithdrawal(selectedAccount, -10));

            decimal myBalance = atm.GetBalance(selectedAccount);
            //balance must remain unchanged.
            Assert.AreEqual(2000, myBalance);

        }

        //Test Case 5: 
        [TestMethod]
        public void TestCheckIfBalanceOnWithdrawalNotNegative()
        {
            selectedAccount = atm.getAllAccounts()[0];
            decimal InitialBalance = atm.GetBalance(selectedAccount);
            atm.MakeActualWithdrawal(selectedAccount, InitialBalance+10);

            if (atm.GetBalance(selectedAccount) < 0)
            {
                Assert.Fail("Balance become negative");
            }

        }

        //Test Case 6: 
        [TestMethod]
        public void TestCheckIfOnlyWithEnoughFundsWithdrawal()
        {
            selectedAccount = atm.getAllAccounts()[0];
            decimal InitialBalance = atm.GetBalance(selectedAccount);
            atm.MakeActualWithdrawal(selectedAccount, InitialBalance + 10);

            if (atm.GetBalance(selectedAccount) != InitialBalance)
            {
                Assert.Fail("ATm allows to widthrawal with unsufficient funds!");
            }

        }

        //Positive Test: Perform Third Party Money Transfer
        //Test Case 7: 
        [TestMethod]
        public void PerformThirdPartyTransfer()
        {
            BankAccount selectedAccount1 = atm.getAllAccounts()[0];

            BankAccount selectedAccount2 = atm.getAllAccounts()[1];
            decimal InitialBalance = atm.GetBalance(selectedAccount2);


            //"Abdumalik", AccountNumber=111222,
            var vMThirdPartyTransfer = new VMThirdPartyTransfer();
            vMThirdPartyTransfer.TransferAmount = 10;
            vMThirdPartyTransfer.RecipientBankAccountNumber = 111222;
            vMThirdPartyTransfer.RecipientBankAccountName = "Abdumalik";
            //make transfer.
            var output = atm.PerformThirdPartyTransfer(selectedAccount1, vMThirdPartyTransfer);
            decimal AfterTransferBalance = atm.GetBalance(selectedAccount2);

            Assert.AreEqual(InitialBalance + 10, AfterTransferBalance);           


        }

        //Test Case 8:
        [TestMethod]
        public void PerformThirdPartyTransferNegativeTest()
        {

        }


        //Test Case 9:  Check if Transfer Success
        [TestMethod]
        public void PerformThirdPartySuccessTransfer()
        {
            BankAccount selectedAccount1 = atm.getAllAccounts()[0];

            BankAccount selectedAccount2 = atm.getAllAccounts()[1];
            decimal InitialBalance = atm.GetBalance(selectedAccount2);


            //"Abdumalik", AccountNumber=111222,
            var vMThirdPartyTransfer = new VMThirdPartyTransfer();
            vMThirdPartyTransfer.TransferAmount = 10;
            vMThirdPartyTransfer.RecipientBankAccountNumber = 111222;
            vMThirdPartyTransfer.RecipientBankAccountName = "Abdumalik";
            //make transfer.
            var output = atm.PerformThirdPartyTransfer(selectedAccount1, vMThirdPartyTransfer);

            Assert.IsTrue(output); 


        }

        //Test Case 10 - Negative Test: 
        [TestMethod]
        public void TestCheckDeposit()
        {

        }

       

        }
}
