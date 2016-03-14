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
    public partial class NewCustomer : Form
    {
        private static List<Customer> customers;
        private Customer aCustomer;
        private TextBox[] textBoxes;

        public NewCustomer()
        {
            InitializeComponent();
            customers = new List<Customer>();
            textBoxes = new TextBox[] { txtAddress, txtCity, txtEmail, txtName, txtPhoneNo, txtSSN, txtState, txtZip };
        }

        public void AddNewCustomer()
        {
            aCustomer = new Customer(txtSSN.Text,txtName.Text, txtAddress.Text, txtCity.Text, txtState.Text,
                txtZip.Text, txtPhoneNo.Text, txtEmail.Text);
            customers.Add(aCustomer);
        }

        public void ResetGUI()
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Clear();
            }
            txtName.Focus();
        }

         private bool AllInputEntered(TextBox[] textboxArray)
        {
            bool allInputEntered = true;

            foreach (TextBox textbox in textboxArray)
            {
                if (string.IsNullOrWhiteSpace(textbox.Text))
                {
                    allInputEntered = false;
                }
            }

            return allInputEntered;
         }

        

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if(AllInputEntered(textBoxes))
            {
                 if (!string.IsNullOrWhiteSpace(textBoxes.ToString()))
                 {
                      txtName.Focus();
                      AddNewCustomer();
                      ResetGUI();
                 }
            }
            else
            {
                 MessageBox.Show("You must enter information into all of the textboxes.", "Incomplete Input",
                    MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
                                                                                           
        }

        private void btnViewCustomer_Click(object sender, EventArgs e)
        {
            ViewExistingCustomers viewExistingCustomers = new ViewExistingCustomers();
            this.Hide();
            viewExistingCustomers.ShowDialog();
            this.Show();
        }

        public static List<Customer> GetCustomers()
        {
            return customers;
        }


    }
}
