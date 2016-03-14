using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradleyErickson_Assignment6
{
   public class Customer
    {
        //Attributes
        private string customerSSN;
        private string customerName;
        private string customerAddress;
        private string customerCity;
        private string customerState;
        private string customerZIP;
        private string customerPhoneNumber;
        private string customerEmail;

        private List<Bank_Account> accounts;


        //Constructor
        public Customer(string aSSN, string aName, string anAddress, string aCity, string aState,
            string aZIP, string aPhoneNumber, string anEmail)
        {
            SetCustomerSSN(aSSN);
            SetCustomerName(aName);
            SetCustomerAddress(anAddress);
            SetCustomerCity(aCity);
            SetCustomerState(aState);
            SetCustomerZIP(aZIP);
            SetCustomerPhoneNumber(aPhoneNumber);
            SetCustomerEmail(anEmail);

            accounts = new List<Bank_Account>();
        }

        //Set Accessors
        public void SetCustomerSSN(string aSSN)
        {
            customerSSN = aSSN;
        }

        public void SetCustomerName(string aName)
        {
            customerName = aName;
        }

        public void SetCustomerAddress(string anAddress)
        {
            customerAddress = anAddress;
        }

        public void SetCustomerCity(string aCity)
        {
            customerCity = aCity;
        }

        public void SetCustomerState(string aState)
        {
            customerState = aState;
        }

        public void SetCustomerZIP(string aZIP)
        {
            customerZIP = aZIP;
        }

        public void SetCustomerPhoneNumber(string aPhoneNumber)
        {
            customerPhoneNumber = aPhoneNumber;
        }

        public void SetCustomerEmail(string anEmail)
        {
            customerEmail = anEmail;
        }

        public void AddBank_Acount(Bank_Account anAccount)
        {
            accounts.Add(anAccount);
        }

        //Get Accessors
        public string GetCustomerSSN()
        {
            return customerSSN;
        }

        public string GetCustomerName()
        {
            return customerName;
        }

        public string GetCustomerAddress()
        {
            return customerAddress;
        }

        public string GetCustomerCity()
        {
            return customerCity;
        }

        public string GetCustomerState()
        {
            return customerState;
        }

        public string GetCustomerZIP()
        {
            return customerZIP;
        }

        public string GetCustomerPhoneNumber()
        {
            return customerPhoneNumber;
        }

        public string GetCustomerEmail()
        {
            return customerEmail;
        }

        public List<Bank_Account> GetAccount()
        {
            return accounts;
        }
    }
}
