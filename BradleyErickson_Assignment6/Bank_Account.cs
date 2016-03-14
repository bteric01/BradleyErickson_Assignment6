using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyErickson_Assignment6
{
    public class Bank_Account
    {
        //Attributes
        private string accountNumber;
        private double accountBalance;
        private DateTime dateAcctOpened;
        private int numberOfChecks;
        private double interestEarnings;

        private Customer myCustomer;
        
        //Constructor
        public Bank_Account(string anAccountNumber, double anAccountBalance, DateTime aDayAccountOpened,
            int aNumberOfChecks, double anInterestEarning, Customer aCustomer)
        {
            SetAccountNumber(anAccountNumber);
            SetAccountBalance(anAccountBalance);
            SetDateAccountOpened(aDayAccountOpened);
            SetNumberOfChecks(aNumberOfChecks);
            SetInterestEarning(anInterestEarning);

            AssignAccountToCustomer(aCustomer);
        }

        //Set Accessors
        public void SetAccountNumber(string anAccountNumber)
        {
            accountNumber = anAccountNumber;
        }

        public void SetAccountBalance(double anAccountBalance)
        {
            accountBalance = anAccountBalance;
        }

        public void SetDateAccountOpened(DateTime aDayAccountOpened)
        {
            dateAcctOpened = aDayAccountOpened;
        }

        public void SetNumberOfChecks(int aNumberOfChecks)
        {
            numberOfChecks = aNumberOfChecks;
        }

        public void SetInterestEarning(double anInterestEarning)
        {
            interestEarnings = anInterestEarning;
        }

        public void AssignAccountToCustomer(Customer aCustomer)
        {
            myCustomer = aCustomer;
            myCustomer.AddBank_Acount(this);
        }

        //GetAccessors
        public string GetAccountNumber()
        {
            return accountNumber;
        }

        public double GetAccountBalance()
        {
            return accountBalance;
        }

        public DateTime GetDateAccountOpened()
        {
            return dateAcctOpened;
        }

        public int GetNumberOfChecks()
        {
            return numberOfChecks;
        }

        public double GetInterestEarnings()
        {
            return interestEarnings;
        }

        //Method

        public void Deposit(double amount)
        {
            accountBalance += amount;
        }

        public void Withdrawl(double amount)
        {
            accountBalance -= amount;
        }

        public void DecreaseChecks(int aCheck, double amount)
        {
            numberOfChecks -= aCheck;
            accountBalance -= amount;
        }
    }
}
