using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BradleyErickson_Assignment6
{
    public partial class ViewExistingCustomers : Form
    {
        private Bank_Account aBankAccount;
        List<string> anAccountNumber;
        private List<Customer> searchResult;
        private TextBox[] editTextbBoxes;


        public ViewExistingCustomers()
        {
            InitializeComponent();
            anAccountNumber = new List<string>();
            PopulateCustomerNames();
            PopulateViewCustomerNames();
            editTextbBoxes = new TextBox[] { txtEditAddress, txtEditCity, txtEditEmail, txtEditName, txtEditPhoneNo, txtEditSSN, txtEditState, txtEditZip};
        }

        public void PopulateCustomerNames()
        {
            lstNewCustomers.Items.Clear();
            if (NewCustomer.GetCustomers().Count != 0)
            {
                foreach (Customer customer in NewCustomer.GetCustomers())
                {
                    lstNewCustomers.Items.Add(customer.GetCustomerName());
                }
            }
            else
            {
                lstNewCustomers.Items.Add("There are currently No Customers!");
            }
        }



        public void PopulateAccountNumbers(Customer aCustomer)
        {
            lstNewAccounts.Items.Clear();
            txtAccountNumber.Clear();
            txtStartingBalance.Clear();

            if (aCustomer.GetAccount().Count != 0)
            {
                foreach (Bank_Account account in aCustomer.GetAccount())
                {
                    lstNewAccounts.Items.Add(account.GetAccountNumber());
                }
            }
            else
            {
                lstNewAccounts.Items.Add("No Current Accounts Open!");
            }
        }

        private void lstNewCustomers_Click(object sender, EventArgs e)
        {
            if (lstNewCustomers.SelectedIndex != -1)
            {
                lblNewAddress.Text = NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetCustomerAddress();
                lblNewCity.Text = NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetCustomerCity();
                lblNewState.Text = NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetCustomerState();
                lblNewZip.Text = NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetCustomerZIP();
                lblNewPhoneNo.Text = NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetCustomerPhoneNumber();
                lblNewEmail.Text = NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetCustomerEmail();

                PopulateAccountNumbers(NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex]);
            }
        }

        private bool AccountNumberIsDuplicate(string aNumber)
        {
            bool isDuplicate = false;
            string inputNumber = txtAccountNumber.Text;

            if (anAccountNumber.Contains(aNumber, StringComparer.OrdinalIgnoreCase))
            {
                isDuplicate = true;
            }
            return isDuplicate;
        }

        private void AddAcountNumberToCheck(string aNumber)
        {
            if (!AccountNumberIsDuplicate(aNumber))
            {
                anAccountNumber.Add(aNumber);
            }
            else
            {
                MessageBox.Show("There is already an account with that number!", "Duplicate Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            double startingBalance = 100;
            double balanceDefault = 100;
            int numberOfChecks = 200;
            double interestEarnings = 0;

            if (lstNewCustomers.SelectedIndex != -1)
            {
                if (!string.IsNullOrWhiteSpace(txtAccountNumber.Text))
                {
                    if (double.TryParse(txtStartingBalance.Text, out startingBalance))
                    {
                        AddAcountNumberToCheck(txtAccountNumber.Text);
                        aBankAccount = new Bank_Account(txtAccountNumber.Text, double.Parse(txtStartingBalance.Text), DateTime.Today, numberOfChecks, interestEarnings,
                            NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex]);
                        ResetNewAccountAdded();
                    }
                    else
                    {
                        AddAcountNumberToCheck(txtAccountNumber.Text);
                        aBankAccount = new Bank_Account(txtAccountNumber.Text, balanceDefault, DateTime.Today, numberOfChecks, interestEarnings,
                           NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex]);
                        ResetNewAccountAdded();
                    }
                }
                else
                {
                    MessageBox.Show("You must enter information into the account number", "Incomplete Input error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccountNumber.Focus();
                }
            }
            else
            {
                MessageBox.Show("You must first select a customer.");
                TabControl1.SelectedIndex = 0;
            }
        }


        private void lstNewAccounts_Click(object sender, EventArgs e)
        {
            if (lstNewCustomers.SelectedIndex != -1)
            {
                if (NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetAccount().Count > 0)
                {
                    txtAccountNumber.Text = NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetAccount()[lstNewAccounts.SelectedIndex].GetAccountNumber();
                    txtStartingBalance.Text = NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetAccount()[lstNewAccounts.SelectedIndex].GetAccountBalance().ToString("c");
                }
            }
        }

        private void ResetNewAccountAdded()
        {
            foreach (TextBox textBox in editTextbBoxes)
            {
                textBox.Clear();
            }
            PopulateAccountNumbers(NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex]);
        }

        public void PopulateViewCustomerNames()
        {
            lstViewCustomers.Items.Clear();
            if (NewCustomer.GetCustomers().Count != 0)
            {
                foreach (Customer customer in NewCustomer.GetCustomers())
                {
                    lstViewCustomers.Items.Add(customer.GetCustomerName());
                }
            }
            else
            {
                lstViewCustomers.Items.Add("There are currently no customers here...");
            }
        }

        public void PopulateViewAccountNumbers(Customer aCustomer)
        {
            lstViewAccountNo.Items.Clear();
            lblViewAccountNumber.Text = "";
            lblViewCurrentBalance.Text = "";
            lblViewDateOpened.Text = "";

            if (aCustomer.GetAccount().Count != 0)
            {
                foreach (Bank_Account account in aCustomer.GetAccount())
                {
                    lstViewAccountNo.Items.Add(account.GetAccountNumber());
                }
            }
            else
            {
                lstViewAccountNo.Items.Add("There are no accounts here...");
            }
        }

        private void lstViewCustomers_Click(object sender, EventArgs e)
        {
            if (lstViewCustomers.SelectedIndex != -1)
            {
                lblViewAddress.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetCustomerAddress();
                lblViewCity.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetCustomerCity();
                lblViewState.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetCustomerState();
                lblViewZip.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetCustomerZIP();
                lblViewPhoneNo.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetCustomerPhoneNumber();
                lblViewEmail.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetCustomerEmail();

                PopulateViewAccountNumbers(NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex]);
            }
        }

        private void lstViewAccountNo_Click(object sender, EventArgs e)
        {
            if (lstViewAccountNo.SelectedIndex != -1)
            {
                if (NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetAccount().Count > 0)
                {
                    lblViewCurrentBalance.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetAccount()[lstViewAccountNo.SelectedIndex].GetAccountBalance().ToString("c");
                    lblViewAccountNumber.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetAccount()[lstViewAccountNo.SelectedIndex].GetAccountNumber();
                    lblViewDateOpened.Text = NewCustomer.GetCustomers()[lstViewCustomers.SelectedIndex].GetAccount()[lstViewAccountNo.SelectedIndex].GetDateAccountOpened().ToString("MM/dd/yyyy");
                }
            }
        }

        public void PopulateSearchBox(List<Customer> aList, ListBox aListBox)
        {
            lstSearchResults.Items.Clear();
            foreach (Customer customer in aList)
            {
                aListBox.Items.Add(customer.GetCustomerName());
            }
        }

        public void PopulateSearchAccountBox(Customer aCustomer)
        {
            lstSearchCustomerAccounts.Items.Clear();
            lblSearchCurrentBalance.Text = "";

            foreach (Bank_Account account in aCustomer.GetAccount())
            {
                lstSearchCustomerAccounts.Items.Add(account.GetAccountNumber());
            }
        }

        private List<Customer> SearchByZip(List<Customer> aList, string searchvalue)
        {
            searchResult = new List<Customer>();

            foreach (Customer customer in aList)
            {
                if (customer.GetCustomerZIP().ToString().ToUpper() == searchvalue.ToUpper())
                {
                    searchResult.Add(customer);
                }
            }
            return searchResult;
        }

        private List<Customer> SearchByCity(List<Customer> aList, string searchvalue)
        {
            searchResult = new List<Customer>();

            foreach (Customer customer in aList)
            {
                if (customer.GetCustomerCity().ToString().ToUpper() == searchvalue.ToUpper())
                {
                    searchResult.Add(customer);
                }
            }
            return searchResult;
        }

        private List<Customer> SearchByState(List<Customer> aList, string searchvalue)
        {
            searchResult = new List<Customer>();

            foreach (Customer customer in aList)
            {
                if (customer.GetCustomerState().ToString().ToUpper() == searchvalue.ToUpper())
                {
                    searchResult.Add(customer);
                }
            }
            return searchResult;
        }

        private void cboSearchCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            const int ZIP_CODE_INDEX = 0;
            const int CITY_INDEX = 1;
            const int STATE_INDEX = 2;

            if (cboSearchCriteria.SelectedIndex != -1)
            {
                if (!string.IsNullOrWhiteSpace(txtSearchValue.Text))
                {
                    switch (cboSearchCriteria.SelectedIndex)
                    {
                        case ZIP_CODE_INDEX:
                            if (SearchByZip(NewCustomer.GetCustomers(), txtSearchValue.Text).Count > 0)
                            {
                                PopulateSearchBox(SearchByZip(NewCustomer.GetCustomers(), txtSearchValue.Text), lstSearchResults);
                            }
                            else
                            {
                                MessageBox.Show("No customer with the zip code " + txtSearchValue.Text + " was found.",
                                    "Zip Code Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case CITY_INDEX:
                            if (SearchByCity(NewCustomer.GetCustomers(), txtSearchValue.Text).Count > 0)
                            {
                                PopulateSearchBox(SearchByCity(NewCustomer.GetCustomers(), txtSearchValue.Text), lstSearchResults);
                            }
                            else
                            {
                                MessageBox.Show("No customer with the City of " + txtSearchValue.Text + " was found.",
                                    "City Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                        case STATE_INDEX:
                            if (SearchByState(NewCustomer.GetCustomers(), txtSearchValue.Text).Count > 0)
                            {
                                PopulateSearchBox(SearchByState(NewCustomer.GetCustomers(), txtSearchValue.Text), lstSearchResults);
                            }
                            else
                            {
                                MessageBox.Show("No customer with the State " + txtSearchValue.Text + " was found.",
                                    "State Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                    }
                   
                }
                else
                {
                    MessageBox.Show("You must enter a search value in the text box!", "Missing Search Value",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NewSearch()
        {
            cboSearchCriteria.SelectedIndex = -1;
            txtSearchValue.Clear();
            txtSearchValue.Focus();
        }

        private Customer GetSearchCustomer(int index)
        {
            return searchResult[index];
        }

        private void lstSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSearchResults.SelectedIndex != -1)
            {
                lblSeacrhCustomerName.Text = GetSearchCustomer(lstSearchResults.SelectedIndex).GetCustomerName();
                PopulateSearchAccountBox(NewCustomer.GetCustomers()[lstSearchResults.SelectedIndex]);
            }
            lstSearchCustomerAccounts.SelectedIndex = 0;
            
        }
        

        private void lstSearchCustomerAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSearchCustomerAccounts.SelectedIndex != -1)
            {
                if (NewCustomer.GetCustomers()[lstNewCustomers.SelectedIndex].GetAccount().Count > 0)
                {
                    lblSearchCurrentBalance.Text =GetSearchCustomer(lstSearchResults.SelectedIndex).GetAccount()[lstSearchCustomerAccounts.SelectedIndex].GetAccountBalance().ToString();
                }
            }
        }

        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            lblSeacrhCustomerName.Text = "";
            lblSearchCurrentBalance.Text = "";
            lblAverageAccountBalance.Text = "";

            lstSearchCustomerAccounts.Items.Clear();
            lstSearchResults.Items.Clear();
            cboSearchCriteria.SelectedIndex = -1;
            txtSearchValue.Clear();
            txtSearchValue.Focus();
        }


        public void PopulateEditingCustomerNames()
        {
            lstEditCustomerInfo.Items.Clear();
            if (NewCustomer.GetCustomers().Count != 0)
            {
                foreach (Customer customer in NewCustomer.GetCustomers())
                {
                    lstEditCustomerInfo.Items.Add(customer.GetCustomerName());
                }
            }
            else
            {
                lstEditCustomerInfo.Items.Add("No current accounts open!");
            }
        }

        private void tbpgEditCustomerInfo_Enter(object sender, EventArgs e)
        {
            PopulateEditingCustomerNames();
        }

        private void lstEditCustomerInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstEditCustomerInfo.SelectedIndex != -1)
            {
                txtEditName.Text = NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].GetCustomerName();
                txtEditSSN.Text = NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].GetCustomerSSN();
                txtEditAddress.Text = NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].GetCustomerAddress();
                txtEditCity.Text = NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].GetCustomerCity();
                txtEditState.Text = NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].GetCustomerState();
                txtEditZip.Text = NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].GetCustomerZIP();
                txtEditPhoneNo.Text = NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].GetCustomerPhoneNumber();
                txtEditEmail.Text = NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].GetCustomerEmail();
            }
        }

        private bool InputEditInfo(TextBox[] editTextBoxes)
        {
            bool InputEntered = true;

            foreach (TextBox textbox in editTextbBoxes)
            {
                if (string.IsNullOrWhiteSpace(textbox.Text))
                {
                    InputEntered = false;
                }
            }

            return InputEntered;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (InputEditInfo(editTextbBoxes))
            {
                NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].SetCustomerAddress(txtEditAddress.Text);
                NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].SetCustomerCity(txtEditCity.Text);
                NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].SetCustomerEmail(txtEditEmail.Text);
                NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].SetCustomerName(txtEditName.Text);
                NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].SetCustomerPhoneNumber(txtEditPhoneNo.Text);
                NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].SetCustomerSSN(txtEditSSN.Text);
                NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].SetCustomerState(txtEditState.Text);
                NewCustomer.GetCustomers()[lstEditCustomerInfo.SelectedIndex].SetCustomerZIP(txtEditZip.Text);

                foreach(TextBox textbox in editTextbBoxes)
                {
                    textbox.Clear();
                }
            }
            else
            {
                MessageBox.Show("You must fill out all of the information.", "Missing Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        public void PopulateCustomerTransactionNames()
        {
            lstTransactionCustomers.Items.Clear();
            if (NewCustomer.GetCustomers().Count != 0)
            {
                foreach (Customer customer in NewCustomer.GetCustomers())
                {
                    lstTransactionCustomers.Items.Add(customer.GetCustomerName());
                }
            }
            else
            {
                lstTransactionCustomers.Items.Add("There are currently no customers");
            }
        }

        public void PopulateAccountTransactionNumbers(Customer aCustomer)
        {
            lstTransactionAccountNumber.Items.Clear();
            lblTransactionAccountNumber.Text = "";
            lblTransactionCurrentBalance.Text = "";

            if (aCustomer.GetAccount().Count != 0)
            {
                foreach (Bank_Account account in aCustomer.GetAccount())
                {
                    lstTransactionAccountNumber.Items.Add(account.GetAccountNumber());
                }
            }
            else
            {
                lstTransactionAccountNumber.Items.Add("No current accounts open");
            }
        }
        
        private void tbpgAccountTransactions_Enter(object sender, EventArgs e)
        {
            PopulateCustomerTransactionNames();
        }

        private double GetInterestRate()
        {
            double balance = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance();
            double interestRate = 0;

            if (balance >= 20000)
            {
                interestRate = .24;
            }
            else if (balance >= 10000)
            {
                interestRate = .17;
            }
            else if (balance >= 1000)
            {
                interestRate = .08;
            }
            else if (balance >= 1)
            {
                interestRate = .05;
            }

            return interestRate;
        }

        private void lstTransactionCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTransactionCustomers.SelectedIndex != -1)
            {
                PopulateAccountTransactionNumbers(NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex]);
                lstTransactionAccountNumber.SelectedIndex = 0;
            }
        }

        private void lstTransactionAccountNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTransactionAccountNumber.SelectedIndex != -1)
            {
                if (NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount().Count > 0)
                {
                    double interestEarnings;
                    double interestRate = GetInterestRate();
                    double balance = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance();

                    interestEarnings = interestRate * balance;

                    lblTransactionAccountNumber.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountNumber();
                    lblTransactionCurrentBalance.Text = balance.ToString("c");
                    lblAmountOfChecks.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetNumberOfChecks().ToString();
                    lblInterestRate.Text = interestRate.ToString();
                    lblInterestPayment.Text = interestEarnings.ToString();
                }
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            double amount = 0;
            double interestEarnings;
            double interestRate = GetInterestRate();
            double balance = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance();

            interestEarnings = interestRate * balance;

            if (double.TryParse(txtDeposit.Text, out amount))
            {
                NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].Deposit(amount);
                lblTransactionCurrentBalance.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance().ToString("c");
                txtDeposit.Clear();
                lblInterestRate.Text = interestRate.ToString();
                lblInterestPayment.Text = interestEarnings.ToString();
            }
            else
            {
                MessageBox.Show("You mst enter a numeric value in this textbox", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWithdrawl_Click(object sender, EventArgs e)
        {
            double amount = 0;

            if (double.TryParse(txtWithdrawl.Text, out amount))
            {
                NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].Withdrawl(amount);

                lblTransactionCurrentBalance.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance().ToString("c");

                txtWithdrawl.Clear();
                if (NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance() < 0)
                {
                    NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].Withdrawl(50);
                    lblTransactionCurrentBalance.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance().ToString("c");
                }
            }
            else
            {
                MessageBox.Show("You must enter a numeric value in this textbox.", "Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnProcessCheck_Click(object sender, EventArgs e)
        {
            double amount = 0;
            int aCheck = 1;

            if (double.TryParse(txtDebitCheck.Text, out amount))
            {
                NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].DecreaseChecks(aCheck, amount);

                lblTransactionCurrentBalance.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance().ToString("c");
                lblAmountOfChecks.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetNumberOfChecks().ToString();

                if (NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance() < 0)
                {
                    NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].Withdrawl(50);
                    lblTransactionCurrentBalance.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance().ToString("C");
                }

                txtDebitCheck.Clear();
            }       
        }

        private void btnAddInterest_Click(object sender, EventArgs e)
        {
            double interestEarnings;
            double interestRate = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetInterestEarnings();
            double balance = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance();

            interestEarnings = interestRate * balance;

            NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].Deposit(interestEarnings);
            lblTransactionCurrentBalance.Text = NewCustomer.GetCustomers()[lstTransactionCustomers.SelectedIndex].GetAccount()[lstTransactionAccountNumber.SelectedIndex].GetAccountBalance().ToString("c");
            lblInterestPayment.Text = interestEarnings.ToString();
        }

        private void tbpgNewAccount_Enter(object sender, EventArgs e)
        {
            PopulateCustomerNames();
        }

        private void tbpgViewAccounts_Enter(object sender, EventArgs e)
        {

            PopulateViewCustomerNames();
        }

       

       


    }
}